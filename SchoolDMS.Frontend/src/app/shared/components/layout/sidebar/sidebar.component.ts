import { Component, Input } from '@angular/core';
import { AuthStateService } from '../../../../core/services/state/auth-state.service';
import { UserRole } from '../../../../core/models/user.model';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent {
  @Input() isOpen = false;

  constructor(public authState: AuthStateService) {}

  get isEngineer(): boolean {
    return this.authState.isEngineer();
  }

  get isOpsVerifier(): boolean {
    return this.authState.isOpsVerifier();
  }
}
