import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

export type NotificationType = 'success' | 'error' | 'info' | 'warning';

export interface Notification {
  id: string;
  type: NotificationType;
  message: string;
  title?: string;
  duration?: number;
}

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private notificationsSubject = new BehaviorSubject<Notification[]>([]);
  public notifications$ = this.notificationsSubject.asObservable();

  show(type: NotificationType, message: string, title?: string, duration: number = 3000) {
    const id = Math.random().toString(36).substring(2, 9);
    const notification: Notification = { id, type, message, title, duration };
    
    const current = this.notificationsSubject.value;
    this.notificationsSubject.next([...current, notification]);

    if (duration > 0) {
      setTimeout(() => this.remove(id), duration);
    }
  }

  success(message: string, title?: string) {
    this.show('success', message, title);
  }

  error(message: string, title?: string) {
    this.show('error', message, title);
  }

  info(message: string, title?: string) {
    this.show('info', message, title);
  }

  warning(message: string, title?: string) {
    this.show('warning', message, title);
  }

  remove(id: string) {
    const current = this.notificationsSubject.value;
    this.notificationsSubject.next(current.filter(n => n.id !== id));
  }
}
