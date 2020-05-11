import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/nav-bar.component';



@NgModule({
  declarations: [NavbarComponent],
  imports: [
    CommonModule
  ],
  exports:[NavbarComponent]
})
export class CoreModule { }
