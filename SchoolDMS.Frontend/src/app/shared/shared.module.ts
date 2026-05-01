import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { NavbarComponent } from './components/layout/navbar/navbar.component';
import { SidebarComponent } from './components/layout/sidebar/sidebar.component';
import { FooterComponent } from './components/layout/footer/footer.component';

// Common Components
import { LoadingSpinnerComponent } from './components/common/loading-spinner/loading-spinner.component';
import { ErrorAlertComponent } from './components/common/error-alert/error-alert.component';

@NgModule({
  declarations: [
    NavbarComponent,
    SidebarComponent,
    FooterComponent,
    LoadingSpinnerComponent,
    ErrorAlertComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule
  ],
  exports: [
    CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    NavbarComponent,
    SidebarComponent,
    FooterComponent,
    LoadingSpinnerComponent,
    ErrorAlertComponent
  ]
})
export class SharedModule { }
