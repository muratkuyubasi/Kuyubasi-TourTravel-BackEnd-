import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DepartureListComponent } from './departure-list/departure-list.component';
import { AuthGuard } from '@core/security/auth.guard';
import { ManageComponent } from './manage/manage.component';
import { DepartureDetailResolverService } from './departure-detail.resolver';

const routes: Routes = [
  {
    path: '',
    component: DepartureListComponent,
    data: { claimType: 'departure_list' },
    canActivate: [AuthGuard]
  },
  {
    path: 'manage/:id',
    component: ManageComponent,
    resolve: { departure: DepartureDetailResolverService },
    data: { claimType: 'departure_edit' },
    canActivate: [AuthGuard]
  },
  {
    path:'manage',
    component:ManageComponent,
    data:{ claimType:'departure_add'}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DepartureRoutingModule { }
