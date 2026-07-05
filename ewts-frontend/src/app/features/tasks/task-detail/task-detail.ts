import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpErrorResponse } from '@angular/common/http';
import { TaskService } from '../../../core/services/task';
import { Auth } from '../../../core/services/auth';
import { TaskItem, TaskItemStatus } from '../../../shared/models/task.model';
import { User } from '../../../shared/models/user.model';

@Component({
  selector: 'app-task-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './task-detail.html',
  styleUrl: './task-detail.css'
})
export class TaskDetail implements OnInit {
  task = signal<TaskItem | null>(null);
  assignedUser = signal<User | null>(null);
  errorMessage = signal<string>('');
  isLoading = signal<boolean>(true);

  TaskItemStatus = TaskItemStatus;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private taskService: TaskService,
    private authService: Auth
  ) {}

  ngOnInit(): void {
    const taskId = this.route.snapshot.paramMap.get('id');

    if (!taskId) {
      this.errorMessage.set('No task ID provided.');
      this.isLoading.set(false);
      return;
    }

    this.loadTask(taskId);
  }

  loadTask(taskId: string): void {
    this.taskService.getById(taskId).subscribe({
      next: (task: TaskItem) => {
        this.task.set(task);
        this.loadAssignedUser(task.assignedToUserId);
      },
      error: (err: HttpErrorResponse) => {
        this.errorMessage.set(err.error?.message ?? 'Task not found.');
        this.isLoading.set(false);
      }
    });
  }

  loadAssignedUser(userId: string): void {
    this.authService.getAllUsers().subscribe({
      next: (users: User[]) => {
        const match = users.find((u) => u.id === userId);
        this.assignedUser.set(match ?? null);
        this.isLoading.set(false);
      },
      error: () => {
        this.isLoading.set(false);
      }
    });
  }

  statusLabel(status: TaskItemStatus): string {
    return TaskItemStatus[status];
  }

  goBack(): void {
    this.router.navigate(['/tasks']);
  }
}