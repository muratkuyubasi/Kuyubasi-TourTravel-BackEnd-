import { NgModule } from '@angular/core';
import { TranslateModule } from '@ngx-translate/core';
import { HasClaimDirective } from './has-claim.directive';
import { PipesModule } from './pipes/pipes.module';
import { DragDropDirective } from './directives/drag-drop.directive';
import { BreadcrumbComponent } from './breadcrumb/breadcrumb.component';

@NgModule({
  exports: [
    HasClaimDirective,
    PipesModule,
    TranslateModule,
    DragDropDirective,
    BreadcrumbComponent,
  ],
  imports: [PipesModule,TranslateModule],
  declarations: [HasClaimDirective, DragDropDirective, BreadcrumbComponent],
})
export class SharedModule { }
