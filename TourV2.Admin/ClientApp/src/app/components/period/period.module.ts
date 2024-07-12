import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageComponent } from './manage/manage.component';
import { SharedModule } from '@shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PeriodListComponent } from './period-list/period-list.component';
import { PeriodRoutingModule } from './period-routing.module';
import { PeriodDetailResolverService } from './period-detail.resolver';



@NgModule({
  declarations: [
    ManageComponent,
    PeriodListComponent
  ],
  imports: [
    CommonModule,
    PeriodRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [PeriodDetailResolverService]
})
export class PeriodModule { }
