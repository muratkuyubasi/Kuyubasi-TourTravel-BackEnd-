import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ApplicationRoutingModule } from './application-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatInputModule } from '@angular/material/input';
import { MatMenuModule } from '@angular/material/menu';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSelectModule } from '@angular/material/select';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatSortModule } from '@angular/material/sort';
import { MatTableModule } from '@angular/material/table';
import { SharedModule } from '@shared/shared.module';
import { EducationApplicationListComponent } from './education-application-list/education-application-list.component';
import { UmrahApplicationListComponent } from './umrah-application-list/umrah-application-list.component';
import { HajjApplicationDetailComponent } from './application-detail/hajj-application-detail/hajj-application-detail.component';
import { UmrahApplicationDetailComponent } from './application-detail/umrah-application-detail/umrah-application-detail.component';
import { HajjApplicationManageComponent } from './application-manage/education-application-manage/education-application-manage.component';
import { UmrahApplicationManageComponent } from './application-manage/umrah-application-manage/umrah-application-manage.component';
import { PeriodsComponent } from './periods/education-periods-list/periods.component';
import { PeriodsManageComponent } from './periods/periods-manage/periods-manage.component';
import { UmrahPeriodsListComponent } from './periods/umrah-periods-list/umrah-periods-list.component';





@NgModule({
  declarations: [EducationApplicationListComponent,UmrahApplicationListComponent, HajjApplicationDetailComponent, UmrahApplicationDetailComponent, HajjApplicationManageComponent, UmrahApplicationManageComponent, PeriodsComponent, PeriodsManageComponent, UmrahPeriodsListComponent],
  imports: [
    CommonModule,
    ApplicationRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatDialogModule,
    MatSelectModule,
    MatSlideToggleModule,
    SharedModule,
    MatMenuModule,
    MatButtonModule,
    MatCardModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,

  ]
})
export class ApplicationModule { }
