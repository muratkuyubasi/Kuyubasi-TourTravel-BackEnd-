import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';
import { TourService } from '../tour.service';
import { CommonDialogService } from '@core/common-dialog/common-dialog.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';

@Component({
  selector: 'app-tour-list',
  templateUrl: './tour-list.component.html',
  styleUrls: ['./tour-list.component.css']
})
export class TourListComponent extends BaseComponent implements OnInit {

  tours:any;
  constructor(
    private tourService:TourService,
    private commonDialogService: CommonDialogService,
    private toastrService: ToastrService,
    private translationService: TranslationService
  ){
    super()
  }

  ngOnInit(): void {
    this.getTours()
  }

  getTours(){
    this.tourService.getAllTourByLang("tr").subscribe((resp:any)=>{
      console.log(resp)
      this.tours = resp;
    })
  }

  deleteTour(tour: any) {
    const areU= this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${areU} ${tour.title}`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.tourService.deleteTour(tour.id).subscribe(() => {
            this.toastrService.success(this.translationService.getValue('CATEGORY_DELETED_SUCCESSFULLY'));
            this.getTours();
          });
        }
      });
  }
}
