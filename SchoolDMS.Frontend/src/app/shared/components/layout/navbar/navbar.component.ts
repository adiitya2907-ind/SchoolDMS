import { Component, OnInit } from '@angular/core';
import { AuthStateService } from '../../../../core/services/state/auth-state.service';
import { AuthService } from '../../../../core/services/api/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  isMenuOpen = false;
  isProfileMenuOpen = false;

  constructor(
    public authState: AuthStateService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {}

  toggleMenu() {
    this.isMenuOpen = !this.isMenuOpen;
  }

  toggleProfileMenu() {
    this.isProfileMenuOpen = !this.isProfileMenuOpen;
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/auth/login']);
  }
}
