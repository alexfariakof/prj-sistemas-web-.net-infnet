import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/model';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  loginForm: (FormGroup & Login) | any;
  showPassword = false;
  eyeIconClass: string = 'bi-eye';

  constructor(private formbuilder: FormBuilder, public router: Router) {
  }

  ngOnInit(): void {
    this.loginForm = this.formbuilder.group({
      email: ['teste@teste.com', [Validators.required, Validators.email]],
      senha: ['12345T!', Validators.required]
    }) as FormGroup & Login;
  }

  onLoginClick() {
    let login: Login = this.loginForm.getRawValue();
  }

  onTooglePassword() {
    this.showPassword = !this.showPassword;
    this.eyeIconClass = (this.eyeIconClass === 'bi-eye') ? 'bi-eye-slash' : 'bi-eye';
  }
}
