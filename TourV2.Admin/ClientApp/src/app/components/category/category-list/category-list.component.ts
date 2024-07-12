import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';
import { CategoryService } from '../category.service';
import { CommonDialogService } from '@core/common-dialog/common-dialog.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent extends BaseComponent implements OnInit {

  categories:any;
  constructor(
    private categoryService:CategoryService,
    private commonDialogService: CommonDialogService,
    private toastrService: ToastrService,
    private translationService: TranslationService
  ){
    super()
  }

  ngOnInit(): void {
    this.getCategories()
  }

  getCategories(){
    this.categoryService.getAllCategoryByLang("tr").subscribe((resp:any)=>{
      console.log(resp)
      this.categories = resp;
    })
  }

  deleteCategory(category: any) {
    const areU= this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${areU} ${category.title}`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.categoryService.deleteCategory(category.id).subscribe(() => {
            this.toastrService.success(this.translationService.getValue('CATEGORY_DELETED_SUCCESSFULLY'));
            this.getCategories();
          });
        }
      });
  }
}
