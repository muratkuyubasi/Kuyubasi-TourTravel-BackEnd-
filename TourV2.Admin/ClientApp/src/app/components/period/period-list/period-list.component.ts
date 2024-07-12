import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';
import { PeriodService } from '../period.service';
import { CommonDialogService } from '@core/common-dialog/common-dialog.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';

@Component({
  selector: 'app-period-list',
  templateUrl: './period-list.component.html',
  styleUrls: ['./period-list.component.css']
})
export class PeriodListComponent extends BaseComponent implements OnInit {

  periods:any;
  constructor(
    private periodService:PeriodService,
    private commonDialogService: CommonDialogService,
    private toastrService: ToastrService,
    private translationService: TranslationService
  ){
    super()
  }

  ngOnInit(): void {
    this.getPeriods()
  }

  getPeriods(){
    this.periodService.getAllPeriodByLang("tr").subscribe((resp:any)=>{
      console.log(resp)
      this.periods = resp;
    })
  }

  deletePeriod(period: any) {
    const areU= this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${areU} ${period.title}`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.periodService.deletePeriod(period.id).subscribe(() => {
            this.toastrService.success(this.translationService.getValue('CATEGORY_DELETED_SUCCESSFULLY'));
            this.getPeriods();
          });
        }
      });
  }
}
