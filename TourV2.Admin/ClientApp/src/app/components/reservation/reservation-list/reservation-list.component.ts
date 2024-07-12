import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';
import { ReservationService } from '../reservation.service';
import { CommonDialogService } from '@core/common-dialog/common-dialog.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';

@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
})
export class ReservationListComponent extends BaseComponent implements OnInit {

  reservations:any;
  constructor(
    private reservationService:ReservationService,
    private commonDialogService: CommonDialogService,
    private toastrService: ToastrService,
    private translationService: TranslationService
  ){
    super()
  }

  ngOnInit(): void {
    this.getReservations()
  }

  getReservations(){
    this.reservationService.getAllReservations().subscribe((resp:any)=>{
      console.log(resp)
      this.reservations = resp;
    })
  }

  deleteReservation(reservation: any) {
    const areU= this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${areU} ${reservation.title}`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.reservationService.deleteReservation(reservation.id).subscribe(() => {
            this.toastrService.success(this.translationService.getValue('CATEGORY_DELETED_SUCCESSFULLY'));
            this.getReservations();
          });
        }
      });
  }
}
