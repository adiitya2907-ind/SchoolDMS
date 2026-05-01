export interface User {
  id: string;
  email: string;
  firstName: string;
  lastName: string;
  role: UserRole;
  phoneNumber?: string;
  isActive: boolean;
  createdAt?: string;
  lastLoginAt?: string;
}

export enum UserRole {
  Admin = 'Admin',
  Engineer = 'Engineer',
  OpsVerifier = 'OpsVerifier',
  Vendor = 'Vendor'
}
