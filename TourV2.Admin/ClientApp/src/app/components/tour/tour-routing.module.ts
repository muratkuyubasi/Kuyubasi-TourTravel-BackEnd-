import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { TourListComponent } from './tour-list/tour-list.component';
import { AuthGuard } from '@core/security/auth.guard';
import { ManageComponent } from './manage/manage.component';
import { TourDetailResolverService } from './tour-detail.resolver';

const routes: Routes = [
  {
    path: '',
    component: TourListComponent,
    data: { claimType: 'tour_list' },
    canActivate: [AuthGuard]
  },
  {
    path: 'manage/:id',
    component: ManageComponent,
    resolve: { tour: TourDetailResolverService },
    data: { claimType: 'tour_edit' },
    canActivate: [AuthGuard]
  },
  {
    path:'manage',
    component:ManageComponent,
    data:{ claimType:'tour_add'}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class TourRoutingModule { }
