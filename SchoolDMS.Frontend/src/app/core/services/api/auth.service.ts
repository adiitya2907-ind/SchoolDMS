import { Injectable } from '@angular/core';
import { ApiService } from './api.service';
import { AuthStateService } from '../state/auth-state.service';
import { LoginRequest, AuthResponse, RegisterRequest } from '../../models/auth.model';
import { Observable, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private basePath = '/auth';

  constructor(
    private apiService: ApiService,
    private authState: AuthStateService
  ) {}

  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.apiService.post<AuthResponse>(`${this.basePath}/login`, credentials)
      .pipe(
        tap(response => {
          if (response && response.accessToken) {
            this.authState.setAuthData(response.user, response.accessToken, response.refreshToken);
          }
        })
      );
  }

  register(data: RegisterRequest): Observable<AuthResponse> {
    return this.apiService.post<AuthResponse>(`${this.basePath}/register`, data)
      .pipe(
        tap(response => {
          if (response && response.accessToken) {
            this.authState.setAuthData(response.user, response.accessToken, response.refreshToken);
          }
        })
      );
  }

  logout(): void {
    // Optionally call backend logout endpoint here
    this.authState.clearAuthData();
  }
}
