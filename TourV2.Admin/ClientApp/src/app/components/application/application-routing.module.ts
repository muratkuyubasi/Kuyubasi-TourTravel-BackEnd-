import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EducationApplicationListComponent } from './education-application-list/education-application-list.component';
import { UmrahApplicationListComponent } from './umrah-application-list/umrah-application-list.component';
import { AuthGuard } from '@core/security/auth.guard';
import { HajjApplicationDetailComponent } from './application-detail/hajj-application-detail/hajj-application-detail.component';
import { UmrahApplicationDetailComponent } from './application-detail/umrah-application-detail/umrah-application-detail.component';
import { UmrahApplicationManageComponent } from './application-manage/umrah-application-manage/umrah-application-manage.component';
import { HajjApplicationManageComponent } from './application-manage/education-application-manage/education-application-manage.component';
import { PeriodsComponent } from './periods/education-periods-list/periods.component';
import { UmrahPeriodsListComponent } from './periods/umrah-periods-list/umrah-periods-list.component';

const routes: Routes = [
  {
    path: 'education-list/:name/:id',
    component: EducationApplicationListComponent,
    // data: { claimType: 'application_list' }, 
    canActivate: [AuthGuard]
   
  },
  {
    path: 'umrah-list/:name/:id',
    component: UmrahApplicationListComponent,
    // data: { claimType: 'application_list' },
    canActivate: [AuthGuard]
  },
  {
    path: 'hajj-detail/:id',
    component:HajjApplicationDetailComponent ,
    data: { claimType: 'application_list' },
    canActivate: [AuthGuard]
  },
  {
    path: 'umrah-detail/:id',
    component:UmrahApplicationDetailComponent ,
    // data: { claimType: 'application_list' },
    canActivate: [AuthGuard]
  },
  {
    path: 'hajj-edit/:id',
    component:HajjApplicationManageComponent ,
    // data: { claimType: 'application_edit' },
    canActivate: [AuthGuard]
  },
  {
    path: 'umrah-edit/:id',
    component:UmrahApplicationManageComponent ,
    // data: { claimType: 'application_edit' },
    canActivate: [AuthGuard]
  },
  {
    path: 'education-periods',
    component:PeriodsComponent ,
    // data: { claimType: 'application_list' },
    canActivate: [AuthGuard]
  }
  ,
  {
    path: 'umrah-periods',
    component:UmrahPeriodsListComponent ,
    // data: { claimType: 'application_list' },
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ApplicationRoutingModule { }
