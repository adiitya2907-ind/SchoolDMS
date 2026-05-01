export enum DocumentType {
  BeforeInstallation = 'BeforeInstallation',
  AfterInstallation = 'AfterInstallation',
  SerialNumber = 'SerialNumber',
  Certificate = 'Certificate',
  Notes = 'Notes',
  Other = 'Other'
}

export interface Document {
  id: string;
  visitId: string;
  documentType: DocumentType;
  fileName: string;
  fileUrl: string;
  fileSize?: number;
  mimeType?: string;
  uploadedAt: string;
  uploadedBy?: string;
}

export interface DocumentUploadRequest {
  visitId: string;
  documentType: DocumentType;
  file: File;
}
