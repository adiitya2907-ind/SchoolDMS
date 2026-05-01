import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OpsComponent } from './ops.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { VisitsPendingComponent } from './pages/visits-pending/visits-pending.component';

const routes: Routes = [
  {
    path: '',
    component: OpsComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'pending', component: VisitsPendingComponent },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OpsRoutingModule { }
