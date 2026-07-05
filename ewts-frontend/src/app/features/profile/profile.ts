import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpErrorResponse } from '@angular/common/http';
import { Auth } from '../../../core/services/auth';
import { User, UserRole } from '../../../shared/models/user.model';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.html',
  styleUrl: './profile.css'
})
export class Profile implements OnInit {
  profile = signal<User | null>(null);
  errorMessage = signal<string>('');
  isLoading = signal<boolean>(true);

  UserRole = UserRole;

  constructor(private authService: Auth) {}

  ngOnInit(): void {
    const userId = this.authService.getUserIdFromToken();

    if (!userId) {
      this.errorMessage.set('Could not determine your user ID from session.');
      this.isLoading.set(false);
      return;
    }

    this.authService.getById(userId).subscribe({
      next: (user: User) => {
        this.profile.set(user);
        this.isLoading.set(false);
      },
      error: (err: HttpErrorResponse) => {
        this.errorMessage.set('Failed to load profile.');
        this.isLoading.set(false);
      }
    });
  }

  roleLabel(role: UserRole): string {
    return UserRole[role];
  }
}