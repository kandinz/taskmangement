import { Task } from '../models/task.model';
import { TasksService } from './tasks.service';
import { Component, OnInit,ViewChild,ElementRef } from '@angular/core';

@Component({
  selector: 'app-task',
  templateUrl: './task.component.html',
  styleUrls: ['./task.component.css']
})
export class TaskComponent implements OnInit {
  title = 'TaskManagement';
  tasks: Task[] = [];
  // task: TaskModel = {
  //   id:'',
  //   name: '',
  //   content:'',
  // };

  task: Task = {} as Task;

  constructor(private taskService : TasksService){

  }

  async ngOnInit(){
   await this.getTasksByUser();
  }

  async getAllTasks() {
    try{
      this.tasks = await this.taskService.getAllTasks();
    } catch (err){
      console.log(err);
    }
  }

  async getTasksByUser() {
    try{
      this.tasks = await this.taskService.getTasksByUser();
    } catch (err){
      console.log(err);
    }
  }

  async updateTask() {
    try{
      var result = await this.taskService.updateTask(this.task);
      if(result == null) return;
      await this.getTasksByUser();
      this.clearTask();
      console.log(result);
    } catch (err){
      console.log(err);
    }
  }

  async getTask(id:string) {
    try{
      this.task = await this.taskService.getTask(id);
      this.task.id = id;
    } catch (err){
      console.log(err);
    }
  }

  clearTask(){
    this.task = {} as Task;
  }
}
