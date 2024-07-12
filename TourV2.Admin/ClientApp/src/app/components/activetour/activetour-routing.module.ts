import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ActiveTourListComponent } from './activetour-list/activetour-list.component';
import { AuthGuard } from '@core/security/auth.guard';
import { ManageComponent } from './manage/manage.component';
import { ActiveTourDetailResolverService } from './activetour-detail.resolver';

const routes: Routes = [
  {
    path: '',
    component: ActiveTourListComponent,
    data: { claimType: 'activetour_list' },
    canActivate: [AuthGuard]
  },
  {
    path: 'manage/:id',
    component: ManageComponent,
    resolve: { activetour: ActiveTourDetailResolverService },
    data: { claimType: 'activetour_edit' },
    canActivate: [AuthGuard]
  },
  {
    path:'manage',
    component:ManageComponent,
    data:{ claimType:'activetour_add'}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ActiveTourRoutingModule { }
