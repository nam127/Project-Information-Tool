import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundErrorComponent } from './not-found-error.component';

const routes: Routes = [{ path: '', component: NotFoundErrorComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class NotFoundErrorRoutingModule { }
