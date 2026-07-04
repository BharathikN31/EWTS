export enum TaskItemStatus {
  Pending = 1,
  InProgress = 2,
  Completed = 3
}

export enum ApprovalStatus {
  Pending = 1,
  Approved = 2,
  Rejected = 3
}

export interface TaskItem {
  id: string;
  title: string;
  description: string;
  status: TaskItemStatus;
  assignedToUserId: string;
}

export interface CreateTaskDto {
  title: string;
  description: string;
  assignedToUserId: string;
}

export interface UpdateTaskStatusDto {
  taskId: string;
  status: TaskItemStatus;
}

export interface TaskFilterDto {
  status?: TaskItemStatus;
}

export interface ApproveTaskDto {
  taskId: string;
  status: ApprovalStatus;
  comments?: string;
}