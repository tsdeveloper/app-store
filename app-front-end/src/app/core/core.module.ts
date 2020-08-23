import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/nav-bar.component';
import {RouterModule} from '@angular/router';
import { TestErrorComponent } from './test-error/test-error.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import {SectionHeaderComponent} from './section-header/section-header.component';
import {BreadcrumbModule} from 'xng-breadcrumb';
import { SharedModule } from '../shared/shared.module';
import { ToastrModule } from 'ngx-toastr';



@NgModule({
  declarations: [NavbarComponent, TestErrorComponent, NotFoundComponent, ServerErrorComponent, SectionHeaderComponent],
  imports: [
    CommonModule,
    RouterModule,
      BreadcrumbModule,
    SharedModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    })
  ],
  exports: [NavbarComponent, SectionHeaderComponent, ServerErrorComponent]
})
export class CoreModule { }
