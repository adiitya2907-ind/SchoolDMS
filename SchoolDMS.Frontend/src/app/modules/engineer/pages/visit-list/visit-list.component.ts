import { Component, OnInit } from '@angular/core';
import { VisitService } from '../../../../core/services/api/visit.service';
import { AuthStateService } from '../../../../core/services/state/auth-state.service';
import { VisitListItem, VisitStatus } from '../../../../core/models/visit.model';

@Component({
  selector: 'app-visit-list',
  templateUrl: './visit-list.component.html',
  styleUrls: ['./visit-list.component.css']
})
export class VisitListComponent implements OnInit {
  visits: VisitListItem[] = [];
  filteredVisits: VisitListItem[] = [];
  isLoading = true;
  
  // Filters
  searchTerm = '';
  statusFilter = '';

  constructor(
    private visitService: VisitService,
    private authState: AuthStateService
  ) {}

  ngOnInit(): void {
    this.loadVisits();
  }

  loadVisits(): void {
    const user = this.authState.getCurrentUser();
    if (user) {
      this.isLoading = true;
      this.visitService.getEngineerVisits(user.id).subscribe({
        next: (data) => {
          this.visits = data;
          this.applyFilters();
          this.isLoading = false;
        },
        error: () => {
          // Fallback dummy data
          this.visits = [
            { id: '1', schoolName: 'Govt. Primary School A', udiseCode: '01010101', district: 'District X', visitDate: new Date().toISOString(), visitType: 'Installation', status: VisitStatus.PendingApproval, isGpsVerified: true },
            { id: '2', schoolName: 'Govt. High School B', udiseCode: '02020202', district: 'District Y', visitDate: new Date(Date.now() - 86400000).toISOString(), visitType: 'Maintenance', status: VisitStatus.Approved, isGpsVerified: true },
            { id: '3', schoolName: 'Public School C', udiseCode: '03030303', district: 'District Z', visitDate: new Date().toISOString(), visitType: 'Training', status: VisitStatus.Draft, isGpsVerified: false }
          ];
          this.applyFilters();
          this.isLoading = false;
        }
      });
    }
  }

  applyFilters(): void {
    this.filteredVisits = this.visits.filter(visit => {
      const matchesSearch = visit.schoolName.toLowerCase().includes(this.searchTerm.toLowerCase()) || 
                            visit.udiseCode.includes(this.searchTerm) ||
                            visit.district.toLowerCase().includes(this.searchTerm.toLowerCase());
      
      const matchesStatus = this.statusFilter ? visit.status === this.statusFilter : true;
      
      return matchesSearch && matchesStatus;
    });
  }

  onSearchChange(event: any): void {
    this.searchTerm = event.target.value;
    this.applyFilters();
  }

  onStatusFilterChange(event: any): void {
    this.statusFilter = event.target.value;
    this.applyFilters();
  }

  getStatusClass(status: VisitStatus): string {
    switch(status) {
      case VisitStatus.Approved: return 'bg-green-100 text-green-800 border-green-200';
      case VisitStatus.Rejected: return 'bg-red-100 text-red-800 border-red-200';
      case VisitStatus.PendingApproval: return 'bg-yellow-100 text-yellow-800 border-yellow-200';
      case VisitStatus.Submitted: return 'bg-blue-100 text-blue-800 border-blue-200';
      default: return 'bg-gray-100 text-gray-800 border-gray-200';
    }
  }
}
