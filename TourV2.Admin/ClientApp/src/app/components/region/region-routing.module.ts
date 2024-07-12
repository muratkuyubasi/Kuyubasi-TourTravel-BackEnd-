import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegionListComponent } from './region-list/region-list.component';
import { AuthGuard } from '@core/security/auth.guard';
import { ManageComponent } from './manage/manage.component';
import { RegionDetailResolverService } from './region-detail.resolver';

const routes: Routes = [
  {
    path: '',
    component: RegionListComponent,
    data: { claimType: 'region_list' },
    canActivate: [AuthGuard]
  },
  {
    path: 'manage/:id',
    component: ManageComponent,
    resolve: { region: RegionDetailResolverService },
    data: { claimType: 'region_edit' },
    canActivate: [AuthGuard]
  },
  {
    path:'manage',
    component:ManageComponent,
    data:{ claimType:'region_add'}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class RegionRoutingModule { }
