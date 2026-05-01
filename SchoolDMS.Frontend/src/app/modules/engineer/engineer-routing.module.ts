import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EngineerComponent } from './engineer.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { VisitListComponent } from './pages/visit-list/visit-list.component';
// import { VisitFormComponent } from './pages/visit-form/visit-form.component';

const routes: Routes = [
  {
    path: '',
    component: EngineerComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'visits', component: VisitListComponent },
      // { path: 'visits/new', component: VisitFormComponent },
      { path: '', redirectTo: 'dashboard', pathMatch: 'full' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EngineerRoutingModule { }
