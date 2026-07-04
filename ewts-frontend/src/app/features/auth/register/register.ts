import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Auth } from '../../../core/services/auth';
import { CreateUserDto } from '../../../shared/models/user.model';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {
  newUser: CreateUserDto = { name: '', email: '', password: '' };
  errorMessage = signal<string>('');
  successMessage = signal<string>('');
  isLoading = signal<boolean>(false);

  constructor(private authService: Auth, private router: Router) {}

  onSubmit(): void {
    this.errorMessage.set('');
    this.successMessage.set('');
    this.isLoading.set(true);

    this.authService.register(this.newUser).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.successMessage.set('Account created! Redirecting to login...');
        setTimeout(() => this.router.navigate(['/login']), 1500);
      },
      error: (err) => {
        this.isLoading.set(false);
        this.errorMessage.set(
          err.error?.message ?? 'Registration failed. Try a different email.'
        );
      }
    });
  }
}