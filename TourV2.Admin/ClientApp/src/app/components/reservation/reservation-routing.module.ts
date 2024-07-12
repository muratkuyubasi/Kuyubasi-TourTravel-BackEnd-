import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@core/security/auth.guard';
import { ManageComponent } from './manage/manage.component';
import { ReservationDetailResolverService } from './reservation-detail.resolver';
import { ReservationListComponent } from './reservation-list/reservation-list.component';
import { GeneralBookingListComponent } from './generalreservation-list/generalreservation-list.component';

const routes: Routes = [
  {
    path: '',
    component: ReservationListComponent,
    data: { claimType: 'reservation_list' },
    canActivate: [AuthGuard]
  },
  {
    path: 'generalbookinglist/:id',
    component: GeneralBookingListComponent,
    data: { claimType: '' },
    canActivate: [AuthGuard]
  },
  {
    path: 'manage/:id',
    component: ManageComponent,
    resolve: { reservation: ReservationDetailResolverService },
    data: { claimType: 'reservation_edit' },
    canActivate: [AuthGuard]
  },
  {
    path:'manage',
    component:ManageComponent,
    data:{ claimType:'reservation_add'}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ReservationRoutingModule { }
