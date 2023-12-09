import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { UpdationComponent } from './updation.component';

const routes: Routes = [{ path: '', component: UpdationComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UpdationRoutingModule { }
