import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PeriodListComponent } from './period-list/period-list.component';
import { AuthGuard } from '@core/security/auth.guard';
import { ManageComponent } from './manage/manage.component';
import { PeriodDetailResolverService } from './period-detail.resolver';

const routes: Routes = [
  {
    path: '',
    component: PeriodListComponent,
    data: { claimType: 'period_list' },
    canActivate: [AuthGuard]
  },
  {
    path: 'manage/:id',
    component: ManageComponent,
    resolve: { period: PeriodDetailResolverService },
    data: { claimType: 'period_edit' },
    canActivate: [AuthGuard]
  },
  {
    path:'manage',
    component:ManageComponent,
    data:{ claimType:'period_add'}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PeriodRoutingModule { }
