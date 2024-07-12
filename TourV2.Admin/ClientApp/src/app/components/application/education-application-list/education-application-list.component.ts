import { ApplicationService } from '../application.service';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from 'src/app/base.component';
import { Application } from '@core/domain-classes/application';
import { CommonError } from '@core/error-handler/common-error';
import * as XLSX from 'xlsx';
import { CommonDialogService } from '@core/common-dialog/common-dialog.service';
import { TranslationService } from '@core/services/translation.service';
import { Subject, debounceTime, distinctUntilChanged } from 'rxjs';
import { MatPaginator } from '@angular/material/paginator';
import { ActivatedRoute } from '@angular/router';
import { PaginatorService } from '@core/services/paginator.service';


@Component({
  selector: 'app-education-application-list',
  templateUrl: './education-application-list.component.html',
  styleUrls: ['./education-application-list.component.css']
})
export class EducationApplicationListComponent extends BaseComponent implements OnInit {
  candidate: any;
  displayedColumns: string[] = ['id', 'studentNameSurname', 'studentGender'];
  isLoadingResults = true;
  fileName:string = "Değerler Eğitimi Formu.xlsx";
  skip: number = 0;
  pageSize: number = 10;
  totalCount: number = 0;
  search: string = "";
  periodId: number;
  periodName: string;
  @ViewChild(MatPaginator) paginator: MatPaginator;
  private searchSubject = new Subject<string>();


  constructor(
    private applicationService: ApplicationService,
    private toastrService: ToastrService,
    private commonDialogService: CommonDialogService,
    private translationService: TranslationService,
    private activeRoute: ActivatedRoute
    , private paginatorService: PaginatorService) {
    super();
  }

  ngOnInit(): void {

    this.sub$.sink = this.activeRoute.params.subscribe((params: { id: number, name: string }) => {
      this.periodId = params.id;
      this.periodName = params.name.toUpperCase();
      if (this.periodId) {
        this.getCandidate();
      }
    });
    this.paginatorService.configurePaginator();
  }


  getCandidate(): void {
    this.isLoadingResults = true;
    this.applicationService.getHajjCandidates(this.periodId)
      .subscribe(
        (data: any) => {
          this.isLoadingResults = false;
          this.totalCount = data?.data?.totalCount
          this.candidate = data.data;
          console.log(data.data)
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

          this.sub$.sink = this.applicationService.deleteHajjCandidate
            (candidate.id)
            .subscribe(() => {

              this.toastrService.success(this.translationService.getValue('DELETED_SUCCESSFULLY'));
              if (candidate.isDeleted == false) {
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
        debounceTime(1000), // 1000 milisaniye gecikme
        distinctUntilChanged(), // Aynı değerlerde tekrar etme
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


