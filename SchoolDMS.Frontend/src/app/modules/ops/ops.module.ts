import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { OpsRoutingModule } from './ops-routing.module';
import { SharedModule } from '../../shared/shared.module';

import { OpsComponent } from './ops.component';
import { DashboardComponent } from './pages/dashboard/dashboard.component';
import { VisitsPendingComponent } from './pages/visits-pending/visits-pending.component';

@NgModule({
  declarations: [
    OpsComponent,
    DashboardComponent,
    VisitsPendingComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    OpsRoutingModule,
    SharedModule
  ]
})
export class OpsModule { }
