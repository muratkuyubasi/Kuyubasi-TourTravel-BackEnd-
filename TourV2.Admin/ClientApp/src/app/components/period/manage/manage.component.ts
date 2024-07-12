import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';

import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { Validators } from 'ngx-editor';
import { PeriodService } from '../period.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.css']
})
export class ManageComponent extends BaseComponent implements OnInit {

  period: any;
  isEditMode = false;
  periodForm:UntypedFormGroup;

  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private fb:UntypedFormBuilder,
    private periodService:PeriodService,
    private toastrService:ToastrService,
    private translationService:TranslationService
  ){
    super()
  }

  ngOnInit(): void {
    this.createCategoryForm();
    this.sub$.sink = this.activeRoute.data.subscribe(
      (data: { period: any }) => {
        if (data.period) {
          console.log(data.period)
          this.period = data.period;
          this.isEditMode = true;
          this.periodForm.patchValue(data.period);
        }
      });
  }

  createCategoryForm(){

      this.periodForm = this.fb.group({
        id: [''],
        code:[''],
        periodId:[''],
        isActive: [false, [Validators.required]],
        languageCode:['tr'],
        title:['',Validators.required]
       
      });
  }

  saveCategory(){
    if(this.periodForm.valid){
      if(this.isEditMode){
        const period = this.createEditBuildObject();
        this.sub$.sink = this.periodService.updatePeriod(period).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('PERIOD_UPDATED_SUCCESSFULLY'));
          this.router.navigate(['/period']);
        });
      }
      else{
        const period = this.createAddBuildObject();
        this.sub$.sink = this.periodService.addPeriod(period).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('PERIOD_CREATED_SUCCESSFULLY'));
          this.router.navigate(['/period']);
        });
      }
  
    }
    else{
      this.periodForm.markAllAsTouched();
    }
  }

  createAddBuildObject(): any {
    const id = this.periodForm.get('id').value;
    const period: any = {
      id: id,
      isActive: this.periodForm.get('isActive').value,
      PeriodRecords:[{
        title: this.periodForm.get('title').value,
        languageCode: this.periodForm.get('languageCode').value,  
        isActive:this.periodForm.get('isActive').value,
      }]
    }
    return period;
  }
  createEditBuildObject(): any {
    this.periodForm.get('isActive').value
    const id = this.periodForm.get('id').value;
    const period: any = {
      id: id,
      code:this.periodForm.get('code').value,
      isActive: this.periodForm.get('isActive').value,
      title: this.periodForm.get('title').value,
      languageCode: this.periodForm.get('languageCode').value,
      periodId: this.periodForm.get('periodId').value
    }
    return period;
  }
  
}
