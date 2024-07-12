import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';
import { CommonDialogService } from '@core/common-dialog/common-dialog.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';
import { ContentService } from '@core/services/content.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './slider-list.component.html',
  styleUrls: ['./slider-list.component.css']
})
export class CategoryListComponent extends BaseComponent implements OnInit {

    getContentModel!:any;
    constructor(
    private contentService:ContentService,
    private commonDialogService: CommonDialogService,
    private toastrService: ToastrService,
    private translationService: TranslationService
  ){
    super()
  }

  ngOnInit(): void {
    this.getcontentList()
  }
  getcontentList(){
    this.contentService.getList().subscribe(data=>{
     this.getContentModel = data;
     console.log("Slider",data)
    });
   }

//   deleteCategory(category: any) {
//     const areU= this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
//     this.sub$.sink = this.commonDialogService
//       .deleteConformationDialog(`${areU} ${category.title}`)
//       .subscribe((isTrue: boolean) => {
//         if (isTrue) {
//           this.sub$.sink = this.categoryService.deleteCategory(category.id).subscribe(() => {
//             this.toastrService.success(this.translationService.getValue('CATEGORY_DELETED_SUCCESSFULLY'));
//             this.getCategories();
//           });
//         }
//       });
//   }
}
