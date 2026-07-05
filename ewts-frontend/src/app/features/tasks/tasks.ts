import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { TaskService } from '../../core/services/task';
import { Auth } from '../../core/services/auth';
import { TaskItem, TaskItemStatus, CreateTaskDto } from '../../shared/models/task.model';
import { User } from '../../shared/models/user.model';
import { RouterLink } from '@angular/router';


@Component({
  selector: 'app-tasks',
  standalone: true,
  imports: [CommonModule, FormsModule,RouterLink],
  templateUrl: './tasks.html',
  styleUrl: './tasks.css'
})
export class Tasks implements OnInit {
  myTasks = signal<TaskItem[]>([]);
  users = signal<User[]>([]);
  errorMessage = signal<string>('');
  isLoading = signal<boolean>(false);

  TaskItemStatus = TaskItemStatus;

  newTask: CreateTaskDto = { title: '', description: '', assignedToUserId: '' };
  showCreateForm = signal<boolean>(false);

  constructor(private taskService: TaskService, private authService: Auth) {}

  ngOnInit(): void {
    this.loadMyTasks();
    this.loadUsers();
  }

  loadUsers(): void {
    this.authService.getAllUsers().subscribe({
      next: (users: User[]) => this.users.set(users),
      error: (err: HttpErrorResponse) => this.errorMessage.set('Failed to load users list.')
    });
  }

  loadMyTasks(): void {
    this.isLoading.set(true);
    this.taskService.getMyTasks().subscribe({
      next: (tasks: TaskItem[]) => {
        this.myTasks.set(tasks);
        this.isLoading.set(false);
      },
      error: (err: HttpErrorResponse) => {
        this.errorMessage.set('Failed to load tasks.');
        this.isLoading.set(false);
      }
    });
  }

  createTask(): void {
    this.taskService.create(this.newTask).subscribe({
      next: () => {
        this.newTask = { title: '', description: '', assignedToUserId: '' };
        this.showCreateForm.set(false);
        this.loadMyTasks();
      },
      error: (err: HttpErrorResponse) => this.errorMessage.set('Failed to create task.')
    });
  }

  updateStatus(taskId: string, status: TaskItemStatus): void {
    this.taskService.updateStatus({ taskId, status }).subscribe({
      next: () => this.loadMyTasks(),
      error: (err: HttpErrorResponse) => this.errorMessage.set('Failed to update task status.')
    });
  }

  statusLabel(status: TaskItemStatus): string {
    return TaskItemStatus[status];
  }
}