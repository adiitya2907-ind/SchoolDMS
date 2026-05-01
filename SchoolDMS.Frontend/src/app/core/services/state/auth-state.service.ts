import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User, UserRole } from '../../models/user.model';
import { LocalStorageService } from '../storage/local-storage.service';
import { environment } from '../../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthStateService {
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  private isAuthenticatedSubject = new BehaviorSubject<boolean>(false);
  public isAuthenticated$ = this.isAuthenticatedSubject.asObservable();

  constructor(private storage: LocalStorageService) {
    this.loadUserFromStorage();
  }

  private loadUserFromStorage() {
    const user = this.storage.getItem<User>('current_user');
    const token = this.storage.getItem<string>(environment.tokenKey);
    
    if (user && token) {
      this.currentUserSubject.next(user);
      this.isAuthenticatedSubject.next(true);
    }
  }

  setAuthData(user: User, token: string, refreshToken: string) {
    this.storage.setItem('current_user', user);
    this.storage.setItem(environment.tokenKey, token);
    this.storage.setItem(environment.refreshTokenKey, refreshToken);
    
    this.currentUserSubject.next(user);
    this.isAuthenticatedSubject.next(true);
  }

  clearAuthData() {
    this.storage.removeItem('current_user');
    this.storage.removeItem(environment.tokenKey);
    this.storage.removeItem(environment.refreshTokenKey);
    
    this.currentUserSubject.next(null);
    this.isAuthenticatedSubject.next(false);
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }

  isEngineer(): boolean {
    const user = this.getCurrentUser();
    return user?.role === UserRole.Engineer;
  }

  isOpsVerifier(): boolean {
    const user = this.getCurrentUser();
    return user?.role === UserRole.OpsVerifier;
  }
}
