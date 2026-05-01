export interface DashboardMetrics {
  totalVisitsThisMonth: number;
  pendingVerificationCount: number;
  approvedVisitsCount: number;
  rejectionRatePercentage: number;
  
  visitsByDistrict: { label: string; value: number }[];
  visitsByStatus: { label: string; value: number }[];
  visitsByProjectType: { label: string; value: number }[];
  dailySubmissionTrend: { date: string; count: number; approved: number; rejected: number }[];
  
  topEngineers: { name: string; approvedCount: number }[];
  bottomEngineers: { name: string; rejectionRate: number }[];
}

export interface ExcelExportFilter {
  status?: string;
  district?: string;
  engineerId?: string;
  startDate?: string;
  endDate?: string;
}

export interface PDFGenerateRequest {
  visitIds: string[];
}
