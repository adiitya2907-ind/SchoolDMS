export interface ApprovalRequest {
  visitId: string;
}

export interface RejectionRequest {
  visitId: string;
  rejectionReasons: string[];
  comments?: string;
}

export interface ApprovalResponse {
  visitId: string;
  status: string;
  processedAt: string;
  processedBy: string;
}
