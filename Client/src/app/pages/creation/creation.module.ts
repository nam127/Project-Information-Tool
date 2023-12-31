import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CreationRoutingModule } from './creation-routing.module';
import { CreationComponent } from './creation.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { MessagesModule } from 'primeng/messages';

@NgModule({
  declarations: [
    CreationComponent
  ],
  imports: [
    CommonModule,
    CreationRoutingModule,
    MatFormFieldModule,
    ReactiveFormsModule,
    ButtonModule,
    AutoCompleteModule,
    MessagesModule
  ]
})
export class CreationModule {}

