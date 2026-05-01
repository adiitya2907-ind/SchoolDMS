import { Component, OnInit } from '@angular/core';
import { DashboardMetrics } from '../../../../core/models/report.model';

@Component({
  selector: 'app-ops-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  metrics: DashboardMetrics = {
    totalVisitsThisMonth: 145,
    pendingVerificationCount: 12,
    approvedVisitsCount: 130,
    rejectionRatePercentage: 2.1,
    visitsByDistrict: [],
    visitsByStatus: [],
    visitsByProjectType: [],
    dailySubmissionTrend: [],
    topEngineers: [],
    bottomEngineers: []
  };

  constructor() {}

  ngOnInit(): void {
    // In a real app, call reportService.getDashboardMetrics()
  }
}
