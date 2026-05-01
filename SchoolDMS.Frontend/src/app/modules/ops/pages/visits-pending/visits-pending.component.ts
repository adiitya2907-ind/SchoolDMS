import { Component, OnInit } from '@angular/core';
import { VisitService } from '../../../../core/services/api/visit.service';
import { VisitListItem, VisitStatus } from '../../../../core/models/visit.model';

@Component({
  selector: 'app-visits-pending',
  templateUrl: './visits-pending.component.html',
  styleUrls: ['./visits-pending.component.css']
})
export class VisitsPendingComponent implements OnInit {
  pendingVisits: VisitListItem[] = [];
  filteredVisits: VisitListItem[] = [];
  isLoading = true;
  searchTerm = '';

  constructor(private visitService: VisitService) {}

  ngOnInit(): void {
    this.loadPendingVisits();
  }

  loadPendingVisits(): void {
    this.isLoading = true;
    this.visitService.getPendingVerifications().subscribe({
      next: (data) => {
        this.pendingVisits = data;
        this.applyFilters();
        this.isLoading = false;
      },
      error: () => {
        // Dummy data for preview
        this.pendingVisits = [
          { id: '1', schoolName: 'Govt. Primary School A', udiseCode: '01010101', district: 'District X', engineerName: 'John Doe', visitDate: new Date().toISOString(), visitType: 'Installation', status: VisitStatus.PendingApproval, isGpsVerified: true },
          { id: '4', schoolName: 'City High School', udiseCode: '04040404', district: 'District Y', engineerName: 'Jane Smith', visitDate: new Date(Date.now() - 43200000).toISOString(), visitType: 'Maintenance', status: VisitStatus.PendingApproval, isGpsVerified: true }
        ];
        this.applyFilters();
        this.isLoading = false;
      }
    });
  }

  applyFilters(): void {
    if (!this.searchTerm) {
      this.filteredVisits = this.pendingVisits;
      return;
    }
    
    const term = this.searchTerm.toLowerCase();
    this.filteredVisits = this.pendingVisits.filter(visit => 
      visit.schoolName.toLowerCase().includes(term) || 
      visit.udiseCode.includes(term) ||
      (visit.engineerName && visit.engineerName.toLowerCase().includes(term))
    );
  }

  onSearchChange(event: any): void {
    this.searchTerm = event.target.value;
    this.applyFilters();
  }
}
