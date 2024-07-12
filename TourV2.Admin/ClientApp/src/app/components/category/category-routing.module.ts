import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '@core/security/auth.guard';
import { ManageComponent } from './manage/manage.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { CategoryDetailResolverService } from './category-detail.resolver';

const routes: Routes = [
  {
    path: '',
    component: CategoryListComponent,
    data: { claimType: 'category_list' },
    canActivate: [AuthGuard]
  },
  {
    path: 'manage/:id',
    component: ManageComponent,
    resolve: { category: CategoryDetailResolverService },
    data: { claimType: 'category_edit' },
    canActivate: [AuthGuard]
  },
  {
    path:'manage',
    component:ManageComponent,
    data:{ claimType:'category_add'}
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoryRoutingModule { }
