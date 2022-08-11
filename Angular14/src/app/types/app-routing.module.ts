import { AuthGuard } from './../auth/auth.guard';
import { RegisterComponent } from './../auth/register/register.component';
import { TaskComponent } from './../task/task.component';
import { NgModule, OnInit } from '@angular/core';
import { RouterModule, Routes, CanActivate } from '@angular/router';
import { LoginComponent } from '../auth/login/login.component';

const routes: Routes = [
  { path:'',component:TaskComponent,canActivate : [AuthGuard]},
  { path:'login',component:LoginComponent},
  { path:'register',component:RegisterComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule implements OnInit {
  constructor(){
  }

  ngOnInit(){
  }
 }
