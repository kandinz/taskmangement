import { AuthService } from './../auth.service';
import { LoginForm } from '../../models/auth.model';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  form: LoginForm  = {} as LoginForm;
  constructor(private authService: AuthService) { }

  ngOnInit(): void {
  }
  submit(){
    this.authService.login(this.form);
  }

}
