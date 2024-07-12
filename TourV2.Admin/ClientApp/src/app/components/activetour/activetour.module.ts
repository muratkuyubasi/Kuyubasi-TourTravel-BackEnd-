import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageComponent } from './manage/manage.component';
import { SharedModule } from '@shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActiveTourListComponent } from './activetour-list/activetour-list.component';
import { ActiveTourRoutingModule } from './activetour-routing.module';
import { ActiveTourDetailResolverService } from './activetour-detail.resolver';
import { UpdatePriceComponent } from './update-price/update-price.component';
import { UpdateDayComponent } from './update-date/update-day.component';
import { UpdateSpecificationComponent } from './update-specification/update-specification.component';
import { UpdateDepartureComponent } from './update-departure/update-departure.component';



@NgModule({
  declarations: [
    ManageComponent,
    ActiveTourListComponent,
    UpdatePriceComponent,
    UpdateDayComponent,
    UpdateSpecificationComponent,
    UpdateDepartureComponent
  ],
  imports: [
    CommonModule,
    ActiveTourRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  providers: [ActiveTourDetailResolverService]
})
export class ActiveTourModule { }
