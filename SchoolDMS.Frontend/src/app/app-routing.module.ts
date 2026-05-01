import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  // These will be lazily loaded modules once we create them
  { path: 'auth', loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule) },
  { path: 'engineer', loadChildren: () => import('./modules/engineer/engineer.module').then(m => m.EngineerModule) },
  { path: 'ops', loadChildren: () => import('./modules/ops/ops.module').then(m => m.OpsModule) },
  { path: '', redirectTo: 'auth/login', pathMatch: 'full' },
  { path: '**', redirectTo: 'auth/login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
