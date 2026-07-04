import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { Auth } from '../../core/services/auth';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard implements OnInit {
  userRole = signal<string>('');

  constructor(private authService: Auth) {}

  ngOnInit(): void {
    this.userRole.set(this.authService.getRoleFromToken() ?? 'Unknown');
  }

  get isManagerOrAdmin(): boolean {
    return this.userRole() === 'Manager' || this.userRole() === 'Admin';
  }

  logout(): void {
    this.authService.logout();
    window.location.href = '/login';
  }
}