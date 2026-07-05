import { Component, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { Auth } from '../../../core/services/auth';
import { UserRole } from '../../../shared/models/user.model';

interface CreateUserWithRoleForm {
  name: string;
  email: string;
  password: string;
  role: UserRole;
}

@Component({
  selector: 'app-create-user',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './create-user.html',
  styleUrl: './create-user.css'
})
export class CreateUser {
  newUser: CreateUserWithRoleForm = {
    name: '',
    email: '',
    password: '',
    role: UserRole.Employee
  };

  UserRole = UserRole;
  errorMessage = signal<string>('');
  successMessage = signal<string>('');
  isLoading = signal<boolean>(false);

  constructor(private authService: Auth) {}

  onSubmit(): void {
    this.errorMessage.set('');
    this.successMessage.set('');
    this.isLoading.set(true);

    this.authService.createUserWithRole(this.newUser).subscribe({
      next: () => {
        this.isLoading.set(false);
        this.successMessage.set(`User "${this.newUser.name}" created successfully.`);
        this.newUser = { name: '', email: '', password: '', role: UserRole.Employee };
      },
      error: (err: HttpErrorResponse) => {
        this.isLoading.set(false);
        this.errorMessage.set(err.error?.message ?? 'Failed to create user.');
      }
    });
  }
}