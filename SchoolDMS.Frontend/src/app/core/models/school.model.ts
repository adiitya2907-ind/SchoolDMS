export interface School {
  id: string;
  udiseCode: string;
  name: string;
  address: string;
  district: string;
  block: string;
  contactPerson: string;
  contactPhone: string;
  latitude: number;
  longitude: number;
  isActive: boolean;
}

export interface SchoolDTO extends School {
  totalVisits?: number;
  lastVisitDate?: string;
}
