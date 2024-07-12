import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';

import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { Validators } from 'ngx-editor';
import { ReservationService } from '../reservation.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';
import { ActiveTourService } from '../../activetour/activetour.service';
import * as moment from 'moment'
import { CommonDialogService } from '@core/common-dialog/common-dialog.service';
import { environment } from '@environments/environment';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
})
export class ManageComponent extends BaseComponent implements OnInit {

  reservation: any;
  isEditMode = false;
  reservationForm:UntypedFormGroup;
  reservationPersonForm:UntypedFormGroup;
  differentInvoice:boolean=false;
  activetours:any[]=[];
  tourPrices:any[]=[];
  departures:any[]=[];
  tourDepartures:any[]=[];
  extraPrice:Number=0;
  personEditMode = false;
  path = environment.serverUrl;

  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private fb:UntypedFormBuilder,
    private reservationService:ReservationService,
    private activeTourService:ActiveTourService,
    private toastrService:ToastrService,
    private translationService:TranslationService,
    private commonDialogService:CommonDialogService,
  ){
    super()
  }

  ngOnInit(): void {
    this.getActiveTours()
    this.createReservationForm();
    this.createReservationPersonForm();
    this.sub$.sink = this.activeRoute.data.subscribe(
      (data: { reservation: any }) => {
        if (data.reservation) {
          console.log("Reservation",data.reservation)
          this.reservation = data.reservation;
          this.isEditMode = true;
          this.reservationForm.patchValue(data.reservation);      
          this.getTourDepartures();
        }
      });
  }

  getActiveTours(){
    this.activeTourService.getAllActiveTourByLang("tr").subscribe((resp:any)=>{
      console.log(resp)
      this.activetours = resp;
      const activeTour:any = this.activetours.filter((item:any)=>item.id == this.reservation?.activeTourId)[0];
      this.tourPrices = activeTour.tourPrices
      // this.tourDepartures = activeTour.tourDepartures
    })
  }

  getTourDepartures(){
    this.activeTourService.getAllTourDepartures(this.reservation.activeTourId).subscribe((resp:any)=>{
      this.tourDepartures = resp;
      console.log("DEPS",resp)

    })
  }

  createReservationForm(){
      this.reservationForm = this.fb.group({
        id: [],
        code:[''],
        activeTourId:[''],
        contactFirstName:[''],
        contactLastName:[''],
        contactEmail:[''],
        contactPhone:[''],
        note:[''],
        contactAddress:[''],
        contactStreet:[''],
        contactCity:[''],
        contactState:[''],
        contactPostalCode:[''],
        contactDoorNumber:[''],
        contactCountry:[''],
        invoiceTitle:[''],
        taxNumber:[''],
        taxAdministration:[''],
        invoiceStreet:[''],
        invoicePostalCode:[''],
        invoiceDoorNumber:[''],
        invoiceAddress:[''],
        invoiceState:[''],
        invoiceCity:[''],
        uyruk:[''],
        invoiceCountry:[''],
        
        isDifferentInvoice:[false]
      });
  }

  createReservationPersonForm(){
    this.reservationPersonForm = this.fb.group({
      id:[0],
      tourReservationId:[this.reservation?.id],
      tourDepartureId:[''],
      identificationNumber:[''],
      firstName:[''],
      lastName:[''],
      email:[''],
      phone:[''],
      birthDay:[''],
      gender:[''],
      uyruk:[''],
      tourPriceId:['']
    })
  }

  createBuildObject(): any {
    const invoice = this.reservationForm.get('isDifferentInvoice').value;
    const id = this.reservationForm.get('id').value;
    const reservation: any = {
        id: id,
        activeTourId:this.reservationForm.get('activeTourId').value,
        contactFirstName:this.reservationForm.get('contactFirstName').value,
        contactLastName:this.reservationForm.get('contactLastName').value,
        contactEmail:this.reservationForm.get('contactEmail').value,
        contactPhone:this.reservationForm.get('contactPhone').value,
        note:this.reservationForm.get('note').value,
        contactAddress:this.reservationForm.get('contactAddress').value,
        contactStreet:this.reservationForm.get('contactStreet').value,
        contactCity:this.reservationForm.get('contactCity').value,
        contactState:this.reservationForm.get('contactState').value,
        contactPostalCode:this.reservationForm.get('contactPostalCode').value,
        contactDoorNumber:this.reservationForm.get('contactDoorNumber').value,
        contactCountry:this.reservationForm.get('contactCountry').value,
        invoiceTitle:invoice ? this.reservationForm.get('invoiceTitle').value : this.reservationForm.get('contactFirstName').value,
        taxNumber:invoice? this.reservationForm.get('taxNumber').value : '',
        taxAdministration:invoice ? this.reservationForm.get('invoiceTitle').value : this.reservationForm.get('contactLastName').value,
        invoiceStreet:invoice ? this.reservationForm.get('invoiceStreet').value : this.reservationForm.get('contactStreet').value,
        invoicePostalCode:invoice ? this.reservationForm.get('invoicePostalCode').value : this.reservationForm.get('contactPostalCode').value,
        invoiceDoorNumber:invoice ? this.reservationForm.get('invoiceDoorNumber').value : this.reservationForm.get('contactDoorNumber').value,
        invoiceAddress:invoice ? this.reservationForm.get('invoiceAddress').value : this.reservationForm.get('contactAddress').value,
        invoiceState:invoice ? this.reservationForm.get('invoiceState').value : this.reservationForm.get('contactState').value,
        invoiceCity:invoice ? this.reservationForm.get('invoiceCity').value : this.reservationForm.get('contactCity').value,
        invoiceCountry:invoice ? this.reservationForm.get('invoiceCountry').value : this.reservationForm.get('contactCountry').value,
        uyruk:invoice ? this.reservationForm.get('uyruk').value : this.reservationForm.get('uyruk').value,

        totalAmount:0,
        avaliableBalance:0,
        reservationPersons:[]
    }
    console.log(reservation)
    return reservation;
  }

  saveReservation(){
    if(this.reservationForm.valid){
      if(this.isEditMode){
        const reservation = this.createBuildObject();
        this.sub$.sink = this.reservationService.updateReservation(reservation).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('RESERVATION_UPDATED_SUCCESSFULLY'));
          this.router.navigate(['/reservation']);
        });
      }
      else{
        const reservation = this.createBuildObject();
        this.sub$.sink = this.reservationService.addReservation(reservation).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('RESERVATION_CREATED_SUCCESSFULLY'));
          this.router.navigate(['/reservation']);
        });
      }
  
    }
    else{
      this.reservationForm.markAllAsTouched();
    }
  }

  createBuildPersonObject(){
    const id = this.reservationPersonForm.get('id').value;
    const person: any = {
      id: id,
      tourPriceId:this.reservationPersonForm.get('tourPriceId').value,
      identificationNumber:this.reservationPersonForm.get('identificationNumber').value,
      firstName:this.reservationPersonForm.get('firstName').value,
      lastName:this.reservationPersonForm.get('lastName').value,
      email:this.reservationPersonForm.get('email').value,
      phone:this.reservationPersonForm.get('phone').value,
      birthDay:this.reservationPersonForm.get('birthDay').value,
      gender:this.reservationPersonForm.get('gender').value,
      uyruk:this.reservationPersonForm.get('uyruk').value,

      tourDepartureId:this.reservationPersonForm.get('tourDepartureId').value,
      tourReservationId:this.reservation?.id,
    }
    console.log(person)
    return person;
  }

  savePerson(){
    if(this.reservationPersonForm.valid){
      const person = this.createBuildPersonObject()
      if(this.personEditMode){
        this.sub$.sink = this.reservationService.updatePerson(person).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('PERSON_CREATED_SUCCESSFULLY'));
          
          // this.router.navigate(['/reservation/manage/'+this.reservation.code]);
        });
      }
      else{
        this.sub$.sink = this.reservationService.addPerson(person).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('PERSON_CREATED_SUCCESSFULLY'));
          this.reservation.reservationPersons.push(person)
        });
      }
    }
    else{

    }
  }

  setPrice(e){
    const extraPrice = this.tourPrices.filter(item=>item.id == e.target.value)[0];
    if(extraPrice.extraPrice>0){
      this.extraPrice = extraPrice.extraPrice
    }
    else{
      this.extraPrice = 0
    }
  }

  updatePerson(item){ 
    this.personEditMode = true
    item.birthDay = moment(item.birthDay).format("yyyy-MM-DD")
    this.reservationPersonForm.patchValue(item)

  }

  deletePerson(item){
   const areU= this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${areU} ${item.firstName}`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.reservationService.deletePerson(item.id).subscribe(() => {
            this.toastrService.success(this.translationService.getValue('DELETED_SUCCESSFULLY'));
            const index = this.reservation.reservationPersons.indexOf(item);
            this.reservation.reservationPersons.splice(index,1)
            // this.activetour.tourPrices.push(resp)
          });
        }
      });
  }

  getTourPersonPrice(id){
    const priceData:any = this.tourPrices.filter(item=>item.id ==id)[0]
    let total = priceData?.extraPrice>0 ? `${priceData?.price} (${priceData?.extraPrice} ek ücret)`:`${priceData?.price}`
    return priceData?.title+" "+total 
  }
  
}
