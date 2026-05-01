import { School } from './school.model';
import { User } from './user.model';
import { Document } from './document.model';

export enum VisitStatus {
  Draft = 'Draft',
  Submitted = 'Submitted',
  PendingApproval = 'PendingApproval',
  Approved = 'Approved',
  Rejected = 'Rejected'
}

export interface Visit {
  id: string;
  schoolId: string;
  school?: School;
  engineerId: string;
  engineer?: User;
  visitDate: string;
  visitType: string;
  projectType: string;
  status: VisitStatus;
  
  // GPS Check-in
  checkInLatitude?: number;
  checkInLongitude?: number;
  checkInTime?: string;
  isGpsVerified?: boolean;
  
  // Completion
  workCompleted?: boolean;
  engineerNotes?: string;
  
  // Relations
  documents?: Document[];
  
  createdAt: string;
  updatedAt?: string;
}

export interface CreateVisitRequest {
  schoolId: string;
  visitType: string;
  projectType: string;
  visitDate: string;
  notes?: string;
}

export interface VisitListItem {
  id: string;
  schoolName: string;
  udiseCode: string;
  district: string;
  engineerName?: string;
  visitDate: string;
  visitType: string;
  status: VisitStatus;
  isGpsVerified: boolean;
}
