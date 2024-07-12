import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';

import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { Validators } from 'ngx-editor';
import { TourService } from '../tour.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.css']
})
export class ManageComponent extends BaseComponent implements OnInit {

  tour: any;
  isEditMode = false;
  tourForm:UntypedFormGroup;

  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private fb:UntypedFormBuilder,
    private tourService:TourService,
    private toastrService:ToastrService,
    private translationService:TranslationService
  ){
    super()
  }

  ngOnInit(): void {
    this.createCategoryForm();
    this.sub$.sink = this.activeRoute.data.subscribe(
      (data: { tour: any }) => {
        if (data.tour) {
          console.log(data.tour)
          this.tour = data.tour;
          this.isEditMode = true;
          this.tourForm.patchValue(data.tour);
        }
      });
  }

  createCategoryForm(){

      this.tourForm = this.fb.group({
        id: [''],
        code:[''],
        tourId:[''],
        isActive: [false, [Validators.required]],
        languageCode:['tr'],
        title:['',Validators.required]
       
      });
  }

  saveCategory(){
    if(this.tourForm.valid){
      if(this.isEditMode){
        const tour = this.createEditBuildObject();
        this.sub$.sink = this.tourService.updateTour(tour).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('TOUR_UPDATED_SUCCESSFULLY'));
          this.router.navigate(['/tour']);
        });
      }
      else{
        const tour = this.createAddBuildObject();
        this.sub$.sink = this.tourService.addTour(tour).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('TOUR_CREATED_SUCCESSFULLY'));
          this.router.navigate(['/tour']);
        });
      }
  
    }
    else{
      this.tourForm.markAllAsTouched();
    }
  }

  createAddBuildObject(): any {
    const id = this.tourForm.get('id').value;
    const tour: any = {
      id: id,
      isActive: this.tourForm.get('isActive').value,
      TourRecords:[{
        title: this.tourForm.get('title').value,
        languageCode: this.tourForm.get('languageCode').value,  
        isActive:this.tourForm.get('isActive').value,
      }]
    }
    return tour;
  }
  createEditBuildObject(): any {
    this.tourForm.get('isActive').value
    const id = this.tourForm.get('id').value;
    const tour: any = {
      id: id,
      code:this.tourForm.get('code').value,
      isActive: this.tourForm.get('isActive').value,
      title: this.tourForm.get('title').value,
      languageCode: this.tourForm.get('languageCode').value,
      tourId: this.tourForm.get('tourId').value
    }
    return tour;
  }
  
}
