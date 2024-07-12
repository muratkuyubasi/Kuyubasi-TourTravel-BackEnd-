import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';

import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { Validators } from 'ngx-editor';
import { ActiveTourService } from '../activetour.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';
import * as moment from 'moment'
import { formatDate } from '@angular/common' 
import { environment } from '@environments/environment.prod';
import { UpdatePriceComponent } from '../update-price/update-price.component';
import { MatDialog } from '@angular/material/dialog';
import { CommonDialogService } from '@core/common-dialog/common-dialog.service';
import { UpdateDayComponent } from '../update-date/update-day.component';
import { UpdateSpecificationComponent } from '../update-specification/update-specification.component';
import { UpdateDepartureComponent } from '../update-departure/update-departure.component';
@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.css']
})
export class ManageComponent extends BaseComponent implements OnInit {

  tours:any[]=[]
  categories:any[]=[]
  periods:any[]=[]
  regions:any[]=[]
  priceses:any[]=[]
  departures:any[]=[]
  activetour: any;
  isEditMode = false;
  activetourForm:UntypedFormGroup;
  priceForm:UntypedFormGroup;
  dayForm:UntypedFormGroup;
  specForm:UntypedFormGroup;
  departureForm:UntypedFormGroup;

  fileSelected: File;
  imgURL: any;

  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private fb:UntypedFormBuilder,
    private activetourService:ActiveTourService,
    private toastrService:ToastrService,
    private translationService:TranslationService,
    private dialog: MatDialog,
    private commonDialogService: CommonDialogService,


  ){
    super()
    this.getTours();
    this.getCategories()
    this.getPeriods()
    this.getRegions()
    this.getDepartures()
    this.imgURL = environment.apiUrl;
  }

  ngOnInit(): void {
  
    this.createCategoryForm();
    this.sub$.sink = this.activeRoute.data.subscribe(
      (data: { activetour: any }) => {
        if (data.activetour) {

          console.log("KAYIT SONRASI DATA",data.activetour)
          data.activetour.tourCategoryId = data.activetour.tourCategories[0]?.categoryRecordId;
          data.activetour.transportationTitle = data.activetour.tourTransportation?.title;
          
           data.activetour.startDate = moment(data.activetour.startDate).format("yyyy-MM-DD")
           data.activetour.endDate = moment(data.activetour.endDate).format("yyyy-MM-DD")
           data.activetour.finishDate = moment(data.activetour.finishDate).format("yyyy-MM-DD")

          this.activetour = data.activetour;
          this.isEditMode = true;
          this.activetourForm.patchValue(data.activetour);

          this.createPriceForm();
          this.createDayForm();
          this.createSpecForm();
          this.createDepartureForm();


          
      console.log("pact",this.activetourForm.value)
        }
      });

  }

  getTours(){
    this.activetourService.getAllTourByLang("tr").subscribe((resp:any)=>{
      this.tours = resp;
    })
  }
  getCategories(){
    this.activetourService.getAllCategoryByLang("tr").subscribe((resp:any)=>{
      this.categories = resp;
    })
  }

  getPeriods(){
    this.activetourService.getAllPeriodByLang("tr").subscribe((resp:any)=>{
      this.periods = resp;
    })
  }

  getRegions(){
    this.activetourService.getAllRegionByLang("tr").subscribe((resp:any)=>{
      this.regions = resp;
      console.log(resp)
    })
  }
  getDepartures(){
    this.activetourService.getAllDepartureByLang("tr").subscribe((resp:any)=>{
      this.departures = resp;
      console.log("Hareket",resp)

    })
  }

  
  createCategoryForm(){

      this.activetourForm = this.fb.group({
        id: [''],
        code:[this.activetour?.code],
        tourRecordId:[""],
        tourCategoryId:[""],
        periodRecordId:[""],
        regionRecordId:[""],
        shortDescription:[""],
        quota:[""],
        description:[""],
        isChild:[false],
        childQuota:[0],
        dayCount:[],
        startDate:[],
        endDate:[""],
        finishDate:[],
        youtubeLink:[""],
        transportationTitle:[""],
        isActive: [false, [Validators.required]],
        showCase: [false, [Validators.required]],

      });
  }

  saveActiveTour(){
    if(this.activetourForm.valid){
      if(this.isEditMode){
        const activetour = this.createEditBuildObject();
        console.log("FORMVALUE",activetour);
        this.sub$.sink = this.activetourService.updateActiveTour(activetour).subscribe((resp) => {
          this.toastrService.success(this.translationService.getValue('ACTIVETOUR_UPDATED_SUCCESSFULLY'));
          this.router.navigate(['/activetour/manage/'+resp.data.id]);
        });
      }
      else{
        const activetour = this.createAddBuildObject();

        

        this.sub$.sink = this.activetourService.addActiveTour(activetour).subscribe((resp) => {

            this.toastrService.success(this.translationService.getValue('ACTIVETOUR_CREATED_SUCCESSFULLY'));
            this.router.navigate(['/activetour/manage/'+resp.data.id]);
        });
      }
  
    }
    else{
      this.activetourForm.markAllAsTouched();
    }
  }

  createAddBuildObject(): any {

    const activetour: any = {
      tourRecordId: this.activetourForm.get('tourRecordId').value,
      periodRecordId: this.activetourForm.get('periodRecordId').value,
      regionRecordId: this.activetourForm.get('regionRecordId').value,
      shortDescription: this.activetourForm.get('shortDescription').value,
      quota: this.activetourForm.get('quota').value,
      description: this.activetourForm.get('description').value,
      isChild: this.activetourForm.get('isChild').value,
      childQuota: this.activetourForm.get('childQuota').value,
      dayCount: this.activetourForm.get('dayCount').value,
      startDate: this.activetourForm.get('startDate').value,
      endDate: this.activetourForm.get('endDate').value,
      finishDate: this.activetourForm.get('finishDate').value,
      isActive: this.activetourForm.get('isActive').value,
      showCase: this.activetourForm.get('isActive').value,
      youtubeLink: this.activetourForm.get('youtubeLink').value,

      tourCategories:[{
        CategoryRecordId: this.activetourForm.get('tourCategoryId').value,
      }],
      tourDepartures:[],
      tourMedias:[],
      tourPrices:[],
      tourDays:[],
      tourSpecifications:[],
      tourTransportation:{
        title: this.activetourForm.get('transportationTitle').value,
      }
    }
    return activetour;
  }
  createEditBuildObject(): any {
    this.activetourForm.get('isActive').value
    const id = this.activetourForm.get('id').value;

    const activetour: any = {
      id: id,
      code:this.activetourForm.get('code').value,
      tourRecordId: this.activetourForm.get('tourRecordId').value,
      periodRecordId: this.activetourForm.get('periodRecordId').value,
      regionRecordId: this.activetourForm.get('regionRecordId').value,
      shortDescription: this.activetourForm.get('shortDescription').value,
      quota: this.activetourForm.get('quota').value,
      description: this.activetourForm.get('description').value,
      isChild: this.activetourForm.get('isChild').value,
      childQuota: this.activetourForm.get('childQuota').value,
      dayCount: this.activetourForm.get('dayCount').value,
      startDate: this.activetourForm.get('startDate').value,
      endDate: this.activetourForm.get('endDate').value,
      finishDate: this.activetourForm.get('finishDate').value,
      isActive: this.activetourForm.get('isActive').value,
      showCase: this.activetourForm.get('showCase').value,
      youtubeLink: this.activetourForm.get('youtubeLink').value,
      
      tourCategories:[{
        id:this.activetour.tourCategories[0]?.id,
        CategoryRecordId: this.activetourForm.get('tourCategoryId').value,
      }],
      tourTransportation:{
        id:this.activetour.tourTransportation?.id,
        title: this.activetourForm.get('transportationTitle').value,
      }
    }
    return activetour;
  }

  createPriceForm(){
    this.priceForm = this.fb.group({
      activeTourId:[this.activetour?.id],
      title:['',Validators.required],
      price:[0,Validators.required],
      currency:['€',Validators.required,],
      extraPrice:[0],
      discount:[0],
      isChildPrice:[false,Validators.required]
    })
  }

  savePrice(){
    if(this.priceForm.valid){
      const price= Object.assign({}, this.priceForm.value)
      this.sub$.sink = this.activetourService.addPrice(price).subscribe((resp) => {

        this.toastrService.success(this.translationService.getValue('ACTIVETOUR_CREATED_SUCCESSFULLY'));
        this.activetour.tourPrices.push(price)
    });
    }
    else{
      this.priceForm.markAllAsTouched();
    }
  }

  updatePrice(price?: any): void {
    let dialog =this.dialog.open(UpdatePriceComponent, {
      width: '350px',
      data: Object.assign({}, price),
      
    });

    dialog.afterClosed().subscribe(resp=>{
      const index = this.activetour.tourPrices.indexOf(price);

      this.activetour.tourPrices.splice(index,1)
      this.activetour.tourPrices.push(resp)
      console.log(resp)
    })

  }

  deletePrice(item){
    const areU = this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
    this.sub$.sink = this.commonDialogService.deleteConformationDialog(`${areU} :: ${item.title}`)
      .subscribe(isTrue => {
        if (isTrue) {
          const index = this.activetour.tourPrices.indexOf(item);
          this.activetour.tourPrices.splice(index,1)
          this.activetourService.deletePrice(item).subscribe(resp=>{

          });
        }
      });
  }

  createDayForm(){
    this.dayForm = this.fb.group({
      activeTourId:[this.activetour?.id],
      title:['',Validators.required],
      description:['',Validators.required],
     
    })
  }

  updateDay(item?: any): void {
    let dialog =this.dialog.open(UpdateDayComponent, {
      width: '350px',
      data: Object.assign({}, item),
      
    });

    dialog.afterClosed().subscribe(resp=>{
      const index = this.activetour.tourPrices.indexOf(item);

      this.activetour.tourDays.splice(index,1)
      this.activetour.tourDays.push(resp)
    })

  }

  deleteDay(item){
    const areU = this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
    this.sub$.sink = this.commonDialogService.deleteConformationDialog(`${areU} :: ${item.title}`)
      .subscribe(isTrue => {
        if (isTrue) {
          const index = this.activetour.tourDays.indexOf(item);
          this.activetour.tourDays.splice(index,1)
          this.activetourService.deleteDay(item).subscribe(resp=>{

          });
        }
      });
  }

  saveDay(){
    if(this.dayForm.valid){
      const day= Object.assign({}, this.dayForm.value)
      this.sub$.sink = this.activetourService.addDay(day).subscribe((resp) => {

        this.toastrService.success(this.translationService.getValue('ACTIVETOUR_CREATED_SUCCESSFULLY'));
        this.activetour.tourDays.push(day)
    });
    }
    else{
      this.priceForm.markAllAsTouched();
    }
  }


  createSpecForm(){
    this.specForm = this.fb.group({
      activeTourId:[this.activetour?.id],
      specification:['',Validators.required],
      inPrice:[false,Validators.required],
     
    })
  }

  saveSpec(){
    if(this.specForm.valid){
      const spec= Object.assign({}, this.specForm.value)
      this.sub$.sink = this.activetourService.addSpec(spec).subscribe((resp) => {

        this.toastrService.success(this.translationService.getValue('ACTIVETOUR_CREATED_SUCCESSFULLY'));
        this.activetour.tourSpecifications.push(spec)
    });
    }
    else{
      this.priceForm.markAllAsTouched();
    }
  }

  updateSpec(item?: any): void {
    let dialog =this.dialog.open(UpdateSpecificationComponent, {
      width: '350px',
      data: Object.assign({}, item),
      
    });

    dialog.afterClosed().subscribe(resp=>{
      const index = this.activetour.tourSpecifications.indexOf(item);

      this.activetour.tourSpecifications.splice(index,1)
      this.activetour.tourSpecifications.push(resp)
    })

  }

  deleteSpec(item){
    const areU = this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
    this.sub$.sink = this.commonDialogService.deleteConformationDialog(`${areU} :: ${item.specification}`)
      .subscribe(isTrue => {
        if (isTrue) {
          
          this.activetourService.deleteSpec(item).subscribe(resp=>{
            const index = this.activetour.tourSpecifications.indexOf(item);
            this.activetour.tourSpecifications.splice(index,1)
          });
        }
      });
  }

  createDepartureForm(){
    this.departureForm = this.fb.group({
      activeTourId:[this.activetour?.id],
      departureRecordId:['',Validators.required],
      departureTime:['',Validators.required],
      isMain:[false,Validators.required],
     
    })
  }

  saveDeparture(){
    if(this.departureForm.valid){
      const departure= Object.assign({}, this.departureForm.value)
      this.sub$.sink = this.activetourService.addDeparture(departure).subscribe((resp) => {

        this.toastrService.success(this.translationService.getValue('ACTIVETOUR_CREATED_SUCCESSFULLY'));
        this.activetour.tourDepartures.push(
          {
            departureTime:departure.departureTime,
            isMain:departure.isMain,
            departureRecord:{
              title:departure.title,
            }
          }
        )
        console.log(this.activetour.tourDepartures);
    });
    }
    else{
      this.priceForm.markAllAsTouched();
    }
  }
  updateDeparture(item?: any): void {
    let dialog =this.dialog.open(UpdateDepartureComponent, {
      width: '350px',
      data: Object.assign({}, item),
      
    });

    dialog.afterClosed().subscribe(resp=>{
      const index = this.activetour.tourSpecifications.indexOf(item);

      this.activetour.tourSpecifications.splice(index,1)
      this.activetour.tourSpecifications.push(resp)
    })

  }

  deleteDeparture(item){
    const areU = this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
    this.sub$.sink = this.commonDialogService.deleteConformationDialog(`${areU} :: ${item.specification}`)
      .subscribe(isTrue => {
        if (isTrue) {
          this.activetourService.deleteDeparture(item).subscribe(resp=>{
            const index = this.activetour.tourDepartures.indexOf(item);
            this.activetour.tourDepartures.splice(index,1)
          });
        }
      });
  }


  fileEvent($event) {
    this.fileSelected = $event.target.files[0];
    if (!this.fileSelected) {
      return;
    }
    const mimeType = this.fileSelected.type;
    if (mimeType.match(/image\/*/) == null) {
      return;
    }
    const reader = new FileReader();
    reader.readAsDataURL(this.fileSelected);
    // tslint:disable-next-line: variable-name
    reader.onload = (_event) => {
      const formData = new FormData();
      formData.append(this.fileSelected.name, this.fileSelected,this.activetour.id);
      
      console.log(formData);
      // const data = {
      //   id:0,
      //   activeTourId:this.activetour.id,
      //   formFile:formData
      // }
      this.activetourService.addMedia(formData).subscribe(resp=>{
        this.activetour.tourMedias.push(resp.data)
        console.log(resp)
      })  
      // this.userService.updateProfilePhoto(formData).subscribe((user: User) => {
      //   this.toastrService.success(this.translationService.getValue('PROFILE_PHOTO_UPDATED_SUCCESSFULLY'));
      //   this.imgURL = reader.result;
      //   this.securityService.updateUserProfile(user);
      //   $event.target.value = '';
      // });
    }
  }

  deleteMedia(item){
    const areU = this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
    this.sub$.sink = this.commonDialogService.deleteConformationDialog(`${areU} :: ${item.fileName}`)
      .subscribe(isTrue => {
        if (isTrue) {
          const index = this.activetour.tourMedias.indexOf(item);
          this.activetour.tourMedias.splice(index,1)
          this.activetourService.deleteMedia(item).subscribe(resp=>{
           
          });
        }
      });
  }
  
}
