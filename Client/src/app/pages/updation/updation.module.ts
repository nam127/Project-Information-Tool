import { NgModule } from '@angular/core';
import { CommonModule, DatePipe } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { DialogModule } from 'primeng/dialog';


import { UpdationRoutingModule } from './updation-routing.module';
import { UpdationComponent } from './updation.component';


@NgModule({
  declarations: [
    UpdationComponent
  ],
  imports: [
    CommonModule,
    UpdationRoutingModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    ButtonModule,
    AutoCompleteModule,
    DialogModule
  ],
  providers: [DatePipe]
})
export class UpdationModule { }
