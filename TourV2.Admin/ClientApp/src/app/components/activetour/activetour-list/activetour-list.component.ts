import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';
import { ActiveTourService } from '../activetour.service';
import { CommonDialogService } from '@core/common-dialog/common-dialog.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';
import { ClickService } from '@core/services/click.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-activetour-list',
  templateUrl: './activetour-list.component.html',
  styleUrls: ['./activetour-list.component.css']
})
export class ActiveTourListComponent extends BaseComponent implements OnInit {


  clickModel:any;
  activetours:any;
  constructor(
    private activetourService:ActiveTourService,
    private commonDialogService: CommonDialogService,
    private toastrService: ToastrService,
    private translationService: TranslationService,
    private clickService: ClickService,
    private router: ActivatedRoute
  ){
    super()
  }

  ngOnInit(): void {
    this.getActiveTours()
    
  }

  getActiveTours(){
    this.activetourService.getAllActiveTourByLang("tr").subscribe((resp:any)=>{
      console.log("aa",resp)
      this.activetours = resp;
    })
  }

  deleteActiveTour(activetour: any) {
    const areU= this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${areU} ${activetour.title}`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.activetourService.deleteActiveTour(activetour.id).subscribe(() => {
            this.toastrService.success(this.translationService.getValue('CATEGORY_DELETED_SUCCESSFULLY'));
            this.getActiveTours();
          });
        }
      });
  }


  getClickid(id:number){
    this.clickService.getid(id).subscribe(data=> {
      this.clickModel = data;
      console.log("Click kullanıcı", this.clickModel)
    })
  }

}
