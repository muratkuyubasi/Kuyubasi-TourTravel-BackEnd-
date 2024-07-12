import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageComponent } from './manage/manage.component';
import { SharedModule } from '@shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegionListComponent } from './region-list/region-list.component';
import { RegionRoutingModule } from './region-routing.module';
import { RegionDetailResolverService } from './region-detail.resolver';



@NgModule({
  declarations: [
    ManageComponent,
    RegionListComponent
  ],
  imports: [
    CommonModule,
    RegionRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [RegionDetailResolverService]
})
export class RegionModule { }
