import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { Observable } from 'rxjs';
import { Visit, CreateVisitRequest, VisitListItem } from '../../models/visit.model';

@Injectable({
  providedIn: 'root'
})
export class VisitService {
  private basePath = '/visits';

  constructor(private apiService: ApiService) {}

  getEngineerVisits(engineerId: string): Observable<VisitListItem[]> {
    return this.apiService.get<VisitListItem[]>(`${this.basePath}/by-engineer/${engineerId}`);
  }

  getPendingVerifications(): Observable<VisitListItem[]> {
    return this.apiService.get<VisitListItem[]>(`${this.basePath}/pending-verification`);
  }

  getVisitById(id: string): Observable<Visit> {
    return this.apiService.get<Visit>(`${this.basePath}/${id}`);
  }

  createVisit(data: CreateVisitRequest): Observable<Visit> {
    return this.apiService.post<Visit>(this.basePath, data);
  }

  updateVisit(id: string, data: Partial<CreateVisitRequest>): Observable<Visit> {
    return this.apiService.put<Visit>(`${this.basePath}/${id}`, data);
  }

  checkIn(id: string, latitude: number, longitude: number, photoUrl?: string): Observable<Visit> {
    return this.apiService.patch<Visit>(`${this.basePath}/${id}/check-in`, {
      latitude,
      longitude,
      schoolPhotoUrl: photoUrl
    });
  }

  submitVisit(id: string, workCompleted: boolean, notes?: string): Observable<Visit> {
    return this.apiService.patch<Visit>(`${this.basePath}/${id}/submit`, {
      workCompleted,
      notes
    });
  }
}
