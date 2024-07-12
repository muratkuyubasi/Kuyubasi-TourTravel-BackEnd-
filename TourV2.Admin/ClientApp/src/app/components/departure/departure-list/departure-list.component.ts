import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';
import { DepartureService } from '../departure.service';
import { CommonDialogService } from '@core/common-dialog/common-dialog.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';

@Component({
  selector: 'app-departure-list',
  templateUrl: './departure-list.component.html',
  styleUrls: ['./departure-list.component.css']
})
export class DepartureListComponent extends BaseComponent implements OnInit {

  departures:any;
  constructor(
    private departureService:DepartureService,
    private commonDialogService: CommonDialogService,
    private toastrService: ToastrService,
    private translationService: TranslationService
  ){
    super()
  }

  ngOnInit(): void {
    this.getDepartures()
  }

  getDepartures(){
    this.departureService.getAllDepartureByLang("tr").subscribe((resp:any)=>{
      console.log(resp)
      this.departures = resp;
    })
  }

  deleteDeparture(departure: any) {
    const areU= this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${areU} ${departure.title}`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.departureService.deleteDeparture(departure.id).subscribe(() => {
            this.toastrService.success(this.translationService.getValue('CATEGORY_DELETED_SUCCESSFULLY'));
            this.getDepartures();
          });
        }
      });
  }
}
