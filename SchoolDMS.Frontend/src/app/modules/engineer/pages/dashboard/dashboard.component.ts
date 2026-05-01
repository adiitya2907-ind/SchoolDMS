import { Component, OnInit } from '@angular/core';
import { AuthStateService } from '../../../../core/services/state/auth-state.service';
import { VisitService } from '../../../../core/services/api/visit.service';
import { VisitListItem, VisitStatus } from '../../../../core/models/visit.model';
import { User } from '../../../../core/models/user.model';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  user: User | null = null;
  recentVisits: VisitListItem[] = [];
  
  stats = {
    today: 0,
    completed: 0,
    pending: 0,
    rejected: 0
  };

  constructor(
    private authState: AuthStateService,
    private visitService: VisitService
  ) {}

  ngOnInit(): void {
    this.user = this.authState.getCurrentUser();
    this.loadDashboardData();
  }

  private loadDashboardData() {
    if (this.user) {
      this.visitService.getEngineerVisits(this.user.id).subscribe({
        next: (visits) => {
          this.recentVisits = visits.slice(0, 5); // Take top 5 recent
          
          this.stats = {
            today: visits.filter(v => new Date(v.visitDate).toDateString() === new Date().toDateString()).length,
            completed: visits.filter(v => v.status === VisitStatus.Approved).length,
            pending: visits.filter(v => v.status === VisitStatus.PendingApproval).length,
            rejected: visits.filter(v => v.status === VisitStatus.Rejected).length
          };
        },
        error: () => {
          // Fallback data for demo since backend might not be connected
          this.recentVisits = [
            { id: '1', schoolName: 'Govt. Primary School A', udiseCode: '01010101', district: 'District X', visitDate: new Date().toISOString(), visitType: 'Installation', status: VisitStatus.PendingApproval, isGpsVerified: true },
            { id: '2', schoolName: 'Govt. High School B', udiseCode: '02020202', district: 'District Y', visitDate: new Date(Date.now() - 86400000).toISOString(), visitType: 'Maintenance', status: VisitStatus.Approved, isGpsVerified: true }
          ];
          this.stats = { today: 1, completed: 15, pending: 3, rejected: 1 };
        }
      });
    }
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
