import { User } from './../models/user.model';
import { Router } from '@angular/router';
import { AuthService } from './../auth/auth.service';
import { Component, OnInit,ViewChild,ElementRef } from '@angular/core';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  isAuthenticated: boolean = false;
  constructor(private authService: AuthService){
  }

  ngOnInit(){
    this.isAuthenticated = this.authService.checkAuthenticated();
  }

  logOut(){
    this.authService.logOut();
  }

  getEmailLoggedIn(){
    return this.authService.currentUser.email
  }

  checkAuthenticated(){
    return this.authService.isAuthenticated;
  }
}
