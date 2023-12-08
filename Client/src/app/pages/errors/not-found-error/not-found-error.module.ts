import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { NotFoundErrorRoutingModule } from './not-found-error-routing.module';
import { NotFoundErrorComponent } from './not-found-error.component';


@NgModule({
  declarations: [
    NotFoundErrorComponent
  ],
  imports: [
    CommonModule,
    NotFoundErrorRoutingModule
  ]
})
export class NotFoundErrorModule { }
