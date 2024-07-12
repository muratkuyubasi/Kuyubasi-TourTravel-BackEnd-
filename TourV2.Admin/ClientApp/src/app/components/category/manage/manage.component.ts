import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';

import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormBuilder, UntypedFormGroup } from '@angular/forms';
import { Validators } from 'ngx-editor';
import { CategoryService } from '../category.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';

@Component({
  selector: 'app-manage',
  templateUrl: './manage.component.html',
  styleUrls: ['./manage.component.css']
})
export class ManageComponent extends BaseComponent implements OnInit {

  category: any;
  isEditMode = false;
  categoryForm:UntypedFormGroup;

  constructor(
    private activeRoute: ActivatedRoute,
    private router: Router,
    private fb:UntypedFormBuilder,
    private categoryService:CategoryService,
    private toastrService:ToastrService,
    private translationService:TranslationService
  ){
    super()
  }

  ngOnInit(): void {
    this.createCategoryForm();
    this.sub$.sink = this.activeRoute.data.subscribe(
      (data: { category: any }) => {
        if (data.category) {
  
          this.category = data.category;
          this.isEditMode = true;
          this.categoryForm.patchValue(data.category);
        } else {
          this.category = {
        
          };
        }
      });
  }

  createCategoryForm(){

      this.categoryForm = this.fb.group({
        id: [''],
        code:[''],
        categoryId:[''],
        isActive: [false, [Validators.required]],
        showCase: [false, [Validators.required]],
        languageCode:['tr'],
        title:['',Validators.required]
       
      });
  }

  saveCategory(){
    if(this.categoryForm.valid){
      if(this.isEditMode){
        const category = this.createEditBuildObject();
        this.sub$.sink = this.categoryService.updateCategory(category).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('CATEGORY_UPDATED_SUCCESSFULLY'));
          this.router.navigate(['/category']);
        });
      }
      else{
        const category = this.createAddBuildObject();
        this.sub$.sink = this.categoryService.addCategory(category).subscribe(() => {
          this.toastrService.success(this.translationService.getValue('CATEGORY_CREATED_SUCCESSFULLY'));
          this.router.navigate(['/category']);
        });
      }
  
    }
    else{
      this.categoryForm.markAllAsTouched();
    }
  }

  createAddBuildObject(): any {
    const id = this.categoryForm.get('id').value;
    const category: any = {
      id: id,
      isActive: this.categoryForm.get('isActive').value,
      showCase: this.categoryForm.get('showCase').value,
      CategoryRecords:[{
        title: this.categoryForm.get('title').value,
        languageCode: this.categoryForm.get('languageCode').value,  
        isActive:this.categoryForm.get('isActive').value,
        showCase:this.categoryForm.get('showCase').value,

      }]
    }
    return category;
  }
  createEditBuildObject(): any {
    this.categoryForm.get('isActive').value
    const id = this.categoryForm.get('id').value;
    const category: any = {
      id: id,
      code:this.categoryForm.get('code').value,
      isActive: this.categoryForm.get('isActive').value,
      showCase: this.categoryForm.get('showCase').value,

      title: this.categoryForm.get('title').value,
      languageCode: this.categoryForm.get('languageCode').value,
      categoryId: this.categoryForm.get('categoryId').value
    }
    return category;
  }
  
}
