import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Application } from '@core/domain-classes/application';
import { CommonError } from '@core/error-handler/common-error';
import { ToastrService } from 'ngx-toastr';
import { ApplicationService } from '../application.service';
import { BaseComponent } from 'src/app/base.component';
import * as XLSX from 'xlsx';
import { CommonDialogService } from '@core/common-dialog/common-dialog.service';
import { TranslationService } from '@core/services/translation.service';
import { MatPaginator } from '@angular/material/paginator';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { PaginatorService } from '@core/services/paginator.service';

@Component({
  selector: 'app-umrah-application-list',
  templateUrl: './umrah-application-list.component.html',
  styleUrls: ['./umrah-application-list.component.css']
})
export class UmrahApplicationListComponent extends BaseComponent implements OnInit {
  public get paginatorService(): PaginatorService {
    return this._paginatorService;
  }
  public set paginatorService(value: PaginatorService) {
    this._paginatorService = value;
  }
  candidate: Application[] = [];
  displayedColumns: string[] = ['id', 'name', 'gender', 'swedenIdentificationNumber', 'phoneNumber', 'maritalStatus', 'dateOfBirth', 'nationality', 'closestAssociation', 'action'];
  isLoadingResults = true;
  fileName = 'UMRE_BASVURU_LISTESI.xlsx';
  skip: number = 0;
  pageSize: number = 10;
  totalCount: number = 0;
  search: string = "";
  periodId:number;
  periodName:string;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  private searchSubject = new Subject<string>();


  constructor(
    private activeRoute:ActivatedRoute,
    private toastrService: ToastrService,
    private applicationService: ApplicationService,
    private commonDialogService: CommonDialogService,
    private translationService: TranslationService
    ,private _paginatorService: PaginatorService) {
    super();
  }

  ngOnInit(): void {
    this.sub$.sink = this.activeRoute.params.subscribe((params: { id: number ,name:string}) => {
      this.periodId = params.id;
      this.periodName = params.name.toUpperCase();
      if (this.periodId) {
        this.getCandidate();
      }
    });

this._paginatorService.configurePaginator();
  }


  getCandidate(): void {
    this.isLoadingResults = true;
    this.applicationService.getUmrahCandidates(this.skip, this.pageSize, this.search,this.periodId)
      .subscribe(
        (data: any) => {
          this.isLoadingResults = false;
          this.totalCount = data?.data?.totalCount
          this.candidate = data.data?.data;
        },
        (err: CommonError) => {
          err.messages.forEach(msg => {
            this.toastrService.error(msg);
          });
        }
      );
  }

  deleteCandidate(candidate) {
    let name = candidate.name
    let surname = candidate.surname
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE')} ${name} ${surname}`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {

          this.sub$.sink = this.applicationService.deleteUmrahCandidate
            (candidate.id)
            .subscribe(() => {

              this.toastrService.success(this.translationService.getValue('DELETED_SUCCESSFULLY'));
              if (candidate.isDeleted == true) {
                this.getCandidate()
              }
            });
        }
      });
  }


  onSearch(e) {
    this.search = e.target.value ? e.target.value : "";
    this.searchSubject
      .pipe(
        debounceTime(1000), 
        distinctUntilChanged(),
      )
      .subscribe(() => {
        this.skip = 0;
        this.pageSize = 10;
        this.totalCount = 0;
        this.paginator.pageIndex = 0;
        this.getCandidate();
      });
    this.searchSubject.next(this.search);

  }

  exportExcel(): void {

    let element = document.getElementById('excel-table');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'ana liste');

    XLSX.writeFile(wb, this.fileName);
  }

  onPageChange(e) {
    this.skip = e.pageIndex;
    this.pageSize = e.pageSize;
    this.getCandidate()

  }
}
