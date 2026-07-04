export enum UserRole {
  Admin = 1,
  Manager = 2,
  Employee = 3
}

export interface User {
  id: string;
  name: string;
  email: string;
  role: UserRole;
}

export interface CreateUserDto {
  name: string;
  email: string;
  password: string;
}

export interface LoginDto {
  email: string;
  password: string;
}

export interface LoginResponse {
  token: string;
}