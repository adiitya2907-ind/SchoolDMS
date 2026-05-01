import { Component } from '@angular/core';

@Component({
  selector: 'app-ops-layout',
  template: `
    <app-navbar></app-navbar>
    <app-sidebar [isOpen]="isSidebarOpen"></app-sidebar>
    
    <div class="p-4 lg:ml-64 pt-20 min-h-screen flex flex-col bg-gray-50">
      <main class="flex-grow">
        <router-outlet></router-outlet>
      </main>
      <app-footer></app-footer>
    </div>
  `
})
export class OpsComponent {
  isSidebarOpen = true;
}
