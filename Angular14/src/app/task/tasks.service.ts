import { User } from './../models/user.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Task } from '../models/task.model';
import { lastValueFrom, EMPTY } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TasksService {
  baseUrl = 'https://localhost:7066/api/tasks/';
  currentUser: User = JSON.parse(localStorage.getItem('currentUser') || 'false');
  constructor(private http: HttpClient) { }

  async getAllTasks(): Promise<Task[]>{
    return await lastValueFrom(this.http.get<Task[]>(this.baseUrl));
  }

  async getTasksByUser(): Promise<Task[]>{
    var email =  this.currentUser.email;
    return await lastValueFrom(this.http.get<Task[]>(this.baseUrl +email));
  }
  async updateTask(task: Task): Promise<Task>{
    task.createUser = this.currentUser.id
    return await lastValueFrom(this.http.put<Task>(this.baseUrl,task));
  }

  async getTask(id: string): Promise<Task>{
    return await lastValueFrom(this.http.get<Task>(this.baseUrl + id));
  }
}
