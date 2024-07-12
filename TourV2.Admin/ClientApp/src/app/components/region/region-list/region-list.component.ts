import { Component, OnInit } from '@angular/core';
import { BaseComponent } from 'src/app/base.component';
import { RegionService } from '../region.service';
import { CommonDialogService } from '@core/common-dialog/common-dialog.service';
import { ToastrService } from 'ngx-toastr';
import { TranslationService } from '@core/services/translation.service';

@Component({
  selector: 'app-region-list',
  templateUrl: './region-list.component.html',
  styleUrls: ['./region-list.component.css']
})
export class RegionListComponent extends BaseComponent implements OnInit {

  regions:any;
  constructor(
    private regionService:RegionService,
    private commonDialogService: CommonDialogService,
    private toastrService: ToastrService,
    private translationService: TranslationService
  ){
    super()
  }

  ngOnInit(): void {
    this.getRegions()
  }

  getRegions(){
    this.regionService.getAllRegionByLang("tr").subscribe((resp:any)=>{
      console.log(resp)
      this.regions = resp;
    })
  }

  deleteRegion(region: any) {
    const areU= this.translationService.getValue('ARE_YOU_SURE_YOU_WANT_TO_DELETE');
    this.sub$.sink = this.commonDialogService
      .deleteConformationDialog(`${areU} ${region.title}`)
      .subscribe((isTrue: boolean) => {
        if (isTrue) {
          this.sub$.sink = this.regionService.deleteRegion(region.id).subscribe(() => {
            this.toastrService.success(this.translationService.getValue('CATEGORY_DELETED_SUCCESSFULLY'));
            this.getRegions();
          });
        }
      });
  }
}
