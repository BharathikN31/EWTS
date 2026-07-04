import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpErrorResponse } from '@angular/common/http';
import { TaskService } from '../../core/services/task';
import { Auth } from '../../core/services/auth';
import {
  TaskItem,
  TaskItemStatus,
  ApprovalStatus,
  ApproveTaskDto
} from '../../shared/models/task.model';

@Component({
  selector: 'app-workflow',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './workflow.html',
  styleUrl: './workflow.css'
})
export class Workflow implements OnInit {
  allTasks = signal<TaskItem[]>([]);
  errorMessage = signal<string>('');
  successMessage = signal<string>('');
  isLoading = signal<boolean>(false);

  TaskItemStatus = TaskItemStatus;
  ApprovalStatus = ApprovalStatus;

  filterStatus: TaskItemStatus | '' = '';
  commentsByTask: Record<string, string> = {};

  constructor(private taskService: TaskService, private authService: Auth) {}

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.isLoading.set(true);
    const filter = this.filterStatus === '' ? {} : { status: this.filterStatus };

    this.taskService.filterTasks(filter).subscribe({
      next: (tasks: TaskItem[]) => {
        this.allTasks.set(tasks);
        this.isLoading.set(false);
      },
      error: (err: HttpErrorResponse) => {
        this.errorMessage.set('Failed to load tasks.');
        this.isLoading.set(false);
      }
    });
  }

  onFilterChange(): void {
    this.loadTasks();
  }

  approve(taskId: string, status: ApprovalStatus): void {
    const dto: ApproveTaskDto = {
      taskId,
      status,
      comments: this.commentsByTask[taskId] || undefined
    };

    this.taskService.approve(dto).subscribe({
      next: (message: string) => {
        this.successMessage.set(message);
        this.commentsByTask[taskId] = '';
        this.loadTasks();
        setTimeout(() => this.successMessage.set(''), 3000);
      },
      error: (err: HttpErrorResponse) => {
        this.errorMessage.set(err.error?.message ?? 'Approval failed.');
      }
    });
  }

  statusLabel(status: TaskItemStatus): string {
    return TaskItemStatus[status];
  }
}