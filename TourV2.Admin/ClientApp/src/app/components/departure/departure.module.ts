import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ManageComponent } from './manage/manage.component';
import { SharedModule } from '@shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DepartureListComponent } from './departure-list/departure-list.component';
import { DepartureRoutingModule } from './departure-routing.module';
import { DepartureDetailResolverService } from './departure-detail.resolver';



@NgModule({
  declarations: [
    ManageComponent,
    DepartureListComponent
  ],
  imports: [
    CommonModule,
    DepartureRoutingModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [DepartureDetailResolverService]
})
export class DepartureModule { }
