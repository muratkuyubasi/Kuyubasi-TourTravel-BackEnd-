import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageComponent } from './manage/manage.component';
import { SharedModule } from '@shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { TourListComponent } from './tour-list/tour-list.component';
import { TourRoutingModule } from './tour-routing.module';
import { TourDetailResolverService } from './tour-detail.resolver';



@NgModule({
  declarations: [
    ManageComponent,
    TourListComponent
  ],
  imports: [
    CommonModule,
    TourRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [TourDetailResolverService]
})
export class TourModule { }
