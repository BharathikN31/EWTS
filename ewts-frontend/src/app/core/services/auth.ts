import { Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { CreateUserDto, LoginDto, LoginResponse, User } from '../../shared/models/user.model';

@Injectable({
  providedIn: 'root'
})
export class Auth {
  private readonly apiUrl = 'http://localhost:5149/api/User';
  private readonly tokenKey = 'ewts_token';

  // Signal so components can reactively check login state
  isLoggedIn = signal<boolean>(!!localStorage.getItem(this.tokenKey));

  constructor(private http: HttpClient) {}

  register(dto: CreateUserDto): Observable<User> {
    return this.http.post<User>(`${this.apiUrl}/register`, dto);
  }

  login(dto: LoginDto): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.apiUrl}/login`, dto).pipe(
      tap((res) => {
        localStorage.setItem(this.tokenKey, res.token);
        this.isLoggedIn.set(true);
      })
    );
  }

  logout(): void {
    localStorage.removeItem(this.tokenKey);
    this.isLoggedIn.set(false);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }
  getAllUsers(): Observable<User[]> {
  return this.http.get<User[]>(this.apiUrl);
}

  // Decode JWT payload without a library (base64 decode the middle segment)
  getUserIdFromToken(): string | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'] ?? null;
    } catch {
      return null;
    }
  }

  getRoleFromToken(): string | null {
    const token = this.getToken();
    if (!token) return null;
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      return payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ?? null;
    } catch {
      return null;
    }
  }
}