import { Component } from '@angular/core';

@Component({
  selector: 'app-forgot-password',
  template: `
    <div class="min-h-screen flex items-center justify-center bg-gray-50 py-12 px-4 sm:px-6 lg:px-8">
      <div class="max-w-md w-full space-y-8 bg-white p-8 rounded-xl shadow-lg border border-gray-100 text-center">
        <h2 class="text-2xl font-bold">Forgot Password</h2>
        <p class="text-gray-600">Please contact your administrator to reset your password.</p>
        <div class="mt-6">
          <a routerLink="/auth/login" class="text-primary hover:underline">Back to Login</a>
        </div>
      </div>
    </div>
  `
})
export class ForgotPasswordComponent {}
