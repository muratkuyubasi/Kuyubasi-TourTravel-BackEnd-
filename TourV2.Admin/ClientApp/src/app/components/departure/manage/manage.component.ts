import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';

import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { Validators } from 'ngx-editor';
import { DepartureService } from '../departure.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.css']
})
export class ManageComponent extends BaseComponent implements OnInit {

  departure: any;
  isEditMode = false;
  departureForm:UntypedFormGroup;

  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private fb:UntypedFormBuilder,
    private departureService:DepartureService,
    private toastrService:ToastrService,
    private translationService:TranslationService
  ){
    super()
  }

  ngOnInit(): void {
    this.createDepartureForm();
    this.sub$.sink = this.activeRoute.data.subscribe(
      (data: { departure: any }) => {
        if (data.departure) {
          console.log(data.departure)
          this.departure = data.departure;
          this.isEditMode = true;
          this.departureForm.patchValue(data.departure);
        }
      });
  }

  createDepartureForm(){

      this.departureForm = this.fb.group({
        id: [''],
        code:[''],
        departureId:[''],
        isActive: [false, [Validators.required]],
        languageCode:['tr'],
        title:['',Validators.required],
        isMain:[false],
        latLng:[''],
        departureTime:['',Validators.required]

       
      });
  }

  saveDeparture(){
    if(this.departureForm.valid){
      if(this.isEditMode){
        const departure = this.createEditBuildObject();
        console.log(departure);
        this.sub$.sink = this.departureService.updateDeparture(departure).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('DEPARTURE_UPDATED_SUCCESSFULLY'));
          this.router.navigate(['/departure']);
        });
      }
      else{
        const departure = this.createAddBuildObject();
 

        this.sub$.sink = this.departureService.addDeparture(departure).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('DEPARTURE_CREATED_SUCCESSFULLY'));
          this.router.navigate(['/departure']);
        });
      }
  
    }
    else{
      this.departureForm.markAllAsTouched();
    }
  }

  createAddBuildObject(): any {
    const id = this.departureForm.get('id').value;
    const departure: any = {
      id: id,
      isActive: this.departureForm.get('isActive').value,  
      DepartureRecords:[{
        title: this.departureForm.get('title').value,
        languageCode: this.departureForm.get('languageCode').value,  
        isActive:this.departureForm.get('isActive').value,
        isMain: this.departureForm.get('isMain').value,
        latLng: this.departureForm.get('latLng').value,
        departureTime: this.departureForm.get('departureTime').value,
      
      }]
    }
    return departure;
  }
  createEditBuildObject(): any {
    this.departureForm.get('isActive').value
    const id = this.departureForm.get('id').value;
    const departure: any = {
      id: id,
      code:this.departureForm.get('code').value,
      isActive: this.departureForm.get('isActive').value,
      title: this.departureForm.get('title').value,
      languageCode: this.departureForm.get('languageCode').value,
      departureId: this.departureForm.get('departureId').value,
      isMain: this.departureForm.get('isMain').value,
      latLng: this.departureForm.get('latLng').value,
      departureTime: this.departureForm.get('departureTime').value
    }
    return departure;
  }
  
}
