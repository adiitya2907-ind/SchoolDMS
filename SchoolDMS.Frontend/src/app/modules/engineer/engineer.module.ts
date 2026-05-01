import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { EngineerRoutingModule } from './engineer-routing.module';
import { SharedModule } from '../../shared/shared.module';

import { EngineerComponent } from './engineer.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { VisitListComponent } from './pages/visit-list/visit-list.component';

@NgModule({
  declarations: [
    EngineerComponent,
    DashboardComponent,
    VisitListComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    EngineerRoutingModule,
    SharedModule
  ]
})
export class EngineerModule { }
