import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'projects/create', loadChildren: () => import('./pages/creation/creation.module').then(m => m.CreationModule) },
  { path: 'error/500', loadChildren: () => import('./pages/errors/internal-server-error/internal-server-error.module').then(m => m.InternalServerErrorModule) },
  { path: 'error/404', loadChildren: () => import('./pages/errors/not-found-error/not-found-error.module').then(m => m.NotFoundErrorModule) },
  { path: '**', redirectTo: 'error/404' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }