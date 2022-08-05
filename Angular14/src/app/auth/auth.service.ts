import { Router } from '@angular/router';
import { LoginForm } from './../models/auth.model';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom, EMPTY } from 'rxjs';
import { User } from '../models/user.model';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'https://localhost:7066/api/users/';
  isAuthenticated: boolean = JSON.parse(localStorage.getItem('loggedIn') || 'false');
  currentUser:User = JSON.parse(localStorage.getItem('currentUser') || 'false');

  constructor(
    private http: HttpClient,
    private router: Router) { }

  async register(login:LoginForm): Promise<User>{
    return await lastValueFrom(this.http.put<User>(this.baseUrl,login));
  }

  async login(login:LoginForm): Promise<boolean>{
    this.isAuthenticated = await lastValueFrom(this.http.post<boolean>(this.baseUrl,login));
    if(this.isAuthenticated){
      this.router.navigate(['']);
      localStorage.setItem('loggedIn', 'true');
      this.currentUser = await this.getUserLoggedIn(login.email);
      localStorage.setItem('currentUser',  JSON.stringify(this.currentUser));
    }
    return this.isAuthenticated;
  }

  logOut(){
    this.router.navigate(['/login']);
    localStorage.setItem('loggedIn', 'false');
    localStorage.setItem('currentUser', '');
    this.currentUser = {} as User;
    this.isAuthenticated = false;
  }

  checkAuthenticated(){
    var result =  JSON.parse(localStorage.getItem('loggedIn')|| this.isAuthenticated.toString());
    if(result)
      this.router.navigate(['']);
    else 
      this.router.navigate(['/login']);
    return result;
  }

  async getUserLoggedIn(email:string): Promise<User>{
     return await lastValueFrom(this.http.get<User>(this.baseUrl+email));
  }
}

