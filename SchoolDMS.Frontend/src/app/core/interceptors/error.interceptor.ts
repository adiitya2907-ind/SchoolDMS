import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NotificationService } from '../services/notification/notification.service';
import { AuthStateService } from '../services/state/auth-state.service';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(
    private notificationService: NotificationService,
    private authState: AuthStateService,
    private router: Router
  ) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = 'An unknown error occurred!';

        if (error.error instanceof ErrorEvent) {
          // Client-side error
          errorMessage = error.error.message;
        } else {
          // Server-side error
          if (error.status === 401) {
            this.authState.clearAuthData();
            this.router.navigate(['/auth/login']);
            errorMessage = 'Your session has expired. Please log in again.';
          } else if (error.status === 403) {
            errorMessage = 'You do not have permission to perform this action.';
          } else if (error.error && error.error.message) {
            errorMessage = error.error.message;
          } else if (error.status === 404) {
            errorMessage = 'Resource not found.';
          } else if (error.status === 0) {
            errorMessage = 'Network error. Please check your connection.';
          }
        }

        this.notificationService.error(errorMessage);
        return throwError(() => new Error(errorMessage));
      })
    );
  }
}
