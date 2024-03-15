import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth, Login } from 'src/app/model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AuthService } from 'src/app/services';
import { catchError, map } from 'rxjs';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, ReactiveFormsModule, MatInputModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit {
  loginForm: (FormGroup & Login) | any;
  showPassword = false;
  eyeIconClass: string = 'bi-eye';

  constructor(
    private formbuilder: FormBuilder,
    public router: Router,
    public authService: AuthService) {}

  ngOnInit(): void {
    this.loginForm = this.formbuilder.group({
      email: ['user@custumer.com', [Validators.required, Validators.email]],
      password: ['12345', Validators.required]
    }) as (FormGroup & Login) | any;
  }

  onLoginClick() {
    let login: Login = this.loginForm.getRawValue();
    this.authService.signIn(login).pipe(
      map((response: Auth | any) => {
        if (response.authenticated) {
          return this.authService.createAccessToken(response);
        }
        else {
          throw (response);
        }
      }),
      catchError((error) => {
        throw (error);
      })
    )
    .subscribe({
      next: (result: Boolean) => {
        if (result === true)
          this.router.navigate(['/favorites']);
      },
      error :(response : any) =>  {
        alert(response.error);
      },
      complete() {

      }
    });
  }

  onTooglePassword() {
    this.showPassword = !this.showPassword;
    this.eyeIconClass = (this.eyeIconClass === 'bi-eye') ? 'bi-eye-slash' : 'bi-eye';
  }
}
