import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { Auth } from '../../../core/services/auth';
import { LoginDto } from '../../../shared/models/user.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {
  credentials: LoginDto = { email: '', password: '' };
  errorMessage = signal<string>('');
  isLoading = signal<boolean>(false);

  constructor(private authService: Auth, private router: Router) {}

  onSubmit(): void {
    this.errorMessage.set('');
    this.isLoading.set(true);

    this.authService.login(this.credentials).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.router.navigate(['/dashboard']);
      },
      error: (err) => {
        this.isLoading.set(false);
        this.errorMessage.set(
          err.error?.message ?? 'Invalid email or password'
        );
      }
    });
  }
}