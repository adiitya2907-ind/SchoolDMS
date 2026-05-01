import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthStateService } from '../services/state/auth-state.service';
import { NotificationService } from '../services/notification/notification.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {
  constructor(
    private authState: AuthStateService,
    private router: Router,
    private notificationService: NotificationService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    
    const requiredRoles: string[] = route.data['roles'];
    
    if (!requiredRoles || requiredRoles.length === 0) {
      return true;
    }

    const currentUser = this.authState.getCurrentUser();
    
    if (currentUser && requiredRoles.includes(currentUser.role)) {
      return true;
    }

    this.notificationService.warning('You do not have permission to access this page.');
    
    // Redirect based on role if possible
    if (currentUser) {
      if (currentUser.role === 'Engineer') return this.router.createUrlTree(['/engineer/dashboard']);
      if (currentUser.role === 'OpsVerifier') return this.router.createUrlTree(['/ops/dashboard']);
    }
    
    return this.router.createUrlTree(['/auth/login']);
  }
}
