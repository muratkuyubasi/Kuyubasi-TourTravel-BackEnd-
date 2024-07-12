import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';

import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { Validators } from 'ngx-editor';
import { RegionService } from '../region.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.css']
})
export class ManageComponent extends BaseComponent implements OnInit {

  region: any;
  isEditMode = false;
  regionForm:UntypedFormGroup;

  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private fb:UntypedFormBuilder,
    private regionService:RegionService,
    private toastrService:ToastrService,
    private translationService:TranslationService
  ){
    super()
  }

  ngOnInit(): void {
    this.createCategoryForm();
    this.sub$.sink = this.activeRoute.data.subscribe(
      (data: { region: any }) => {
        if (data.region) {
          console.log(data.region)
          this.region = data.region;
          this.isEditMode = true;
          this.regionForm.patchValue(data.region);
        }
      });
  }

  createCategoryForm(){

      this.regionForm = this.fb.group({
        id: [''],
        code:[''],
        regionId:[''],
        isActive: [false, [Validators.required]],
        languageCode:['tr'],
        title:['',Validators.required]
       
      });
  }

  saveCategory(){
    if(this.regionForm.valid){
      if(this.isEditMode){
        const region = this.createEditBuildObject();
        this.sub$.sink = this.regionService.updateRegion(region).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('REGION_UPDATED_SUCCESSFULLY'));
          this.router.navigate(['/region']);
        });
      }
      else{
        const region = this.createAddBuildObject();
        this.sub$.sink = this.regionService.addRegion(region).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('REGION_CREATED_SUCCESSFULLY'));
          this.router.navigate(['/region']);
        });
      }
  
    }
    else{
      this.regionForm.markAllAsTouched();
    }
  }

  createAddBuildObject(): any {
    const id = this.regionForm.get('id').value;
    const region: any = {
      id: id,
      isActive: this.regionForm.get('isActive').value,
      RegionRecords:[{
        title: this.regionForm.get('title').value,
        languageCode: this.regionForm.get('languageCode').value,  
        isActive:this.regionForm.get('isActive').value,
      }]
    }
    return region;
  }
  createEditBuildObject(): any {
    this.regionForm.get('isActive').value
    const id = this.regionForm.get('id').value;
    const region: any = {
      id: id,
      code:this.regionForm.get('code').value,
      isActive: this.regionForm.get('isActive').value,
      title: this.regionForm.get('title').value,
      languageCode: this.regionForm.get('languageCode').value,
      regionId: this.regionForm.get('regionId').value
    }
    return region;
  }
  
}
