import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Auth } from '../../../core/services/auth';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css'
})
export class Navbar implements OnInit {
  userRole = signal<string>('');

  constructor(private authService: Auth) {}

  ngOnInit(): void {
    this.userRole.set(this.authService.getRoleFromToken() ?? '');
  }

  get isManagerOrAdmin(): boolean {
    return this.userRole() === 'Manager' || this.userRole() === 'Admin';
  }

  get isLoggedIn(): boolean {
    return !!this.authService.getToken();
  }

  logout(): void {
    this.authService.logout();
    window.location.href = '/login';
  }
}