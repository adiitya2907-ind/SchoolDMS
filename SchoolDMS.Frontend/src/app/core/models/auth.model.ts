import { User } from './user.model';

export interface LoginRequest {
  email: string;
  password?: string;
}

export interface AuthResponse {
  accessToken: string;
  refreshToken: string;
  expiresIn: number;
  user: User;
}

export interface RegisterRequest {
  firstName: string;
  lastName: string;
  email: string;
  password?: string;
  phoneNumber?: string;
}
