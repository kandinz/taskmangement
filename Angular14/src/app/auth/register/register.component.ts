import { AuthService } from '../auth.service';
import { Component, OnInit } from '@angular/core';
import { LoginForm } from '../../models/auth.model';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  form: LoginForm  = {} as LoginForm;
  constructor(private authService : AuthService) { }

  ngOnInit(): void {
  }

  async submit(){
    var result = await this.authService.register(this.form);
    console.log(result);
  }


}
