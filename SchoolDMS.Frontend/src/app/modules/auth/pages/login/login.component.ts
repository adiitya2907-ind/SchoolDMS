import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthService } from '../../../../core/services/api/auth.service';
import { NotificationService } from '../../../../core/services/notification/notification.service';
import { AuthStateService } from '../../../../core/services/state/auth-state.service';
import { UserRole } from '../../../../core/models/user.model';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  isLoading = false;
  returnUrl: string = '/';

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authService: AuthService,
    private authState: AuthStateService,
    private notificationService: NotificationService
  ) {
    // Redirect to home if already logged in
    if (this.authState.getCurrentUser()) {
      this.redirectUser(this.authState.getCurrentUser()?.role);
    }
    
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      rememberMe: [false]
    });
  }

  ngOnInit(): void {
    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  get f() { return this.loginForm.controls; }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      return;
    }

    this.isLoading = true;
    this.authService.login({
      email: this.f['email'].value,
      password: this.f['password'].value
    }).subscribe({
      next: (response) => {
        this.notificationService.success('Logged in successfully');
        
        if (this.returnUrl !== '/') {
          this.router.navigateByUrl(this.returnUrl);
        } else {
          this.redirectUser(response.user.role);
        }
      },
      error: (error) => {
        this.isLoading = false;
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }

  private redirectUser(role?: UserRole) {
    if (role === UserRole.Engineer) {
      this.router.navigate(['/engineer/dashboard']);
    } else if (role === UserRole.OpsVerifier) {
      this.router.navigate(['/ops/dashboard']);
    } else if (role === UserRole.Admin) {
      this.router.navigate(['/admin/dashboard']);
    } else {
      this.router.navigate(['/']);
    }
  }
}
