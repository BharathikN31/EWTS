import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {
  TaskItem,
  CreateTaskDto,
  UpdateTaskStatusDto,
  TaskFilterDto,
  ApproveTaskDto
} from '../../shared/models/task.model';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private readonly apiUrl = 'http://localhost:5149/api/Task';

  constructor(private http: HttpClient) {}

  create(dto: CreateTaskDto): Observable<TaskItem> {
    return this.http.post<TaskItem>(this.apiUrl, dto);
  }

  getAll(): Observable<TaskItem[]> {
    return this.http.get<TaskItem[]>(this.apiUrl);
  }

  getById(id: string): Observable<TaskItem> {
    return this.http.get<TaskItem>(`${this.apiUrl}/${id}`);
  }

  updateStatus(dto: UpdateTaskStatusDto): Observable<TaskItem> {
    return this.http.put<TaskItem>(`${this.apiUrl}/status`, dto);
  }

  getMyTasks(): Observable<TaskItem[]> {
    return this.http.get<TaskItem[]>(`${this.apiUrl}/my-tasks`);
  }

  filterTasks(filter: TaskFilterDto): Observable<TaskItem[]> {
    return this.http.post<TaskItem[]>(`${this.apiUrl}/filter`, filter);
  }

  approve(dto: ApproveTaskDto): Observable<string> {
    return this.http.post<string>(`${this.apiUrl}/approve`, dto);
  }
}