import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth, Login } from 'src/app/model';
import { AuthService } from 'src/app/services';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthProvider } from 'src/app/provider/auth.provider';
import { map, catchError, Subject } from 'rxjs';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, MatFormFieldModule, ReactiveFormsModule, MatInputModule, MatButtonModule ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})

export class LoginComponent implements OnInit, OnDestroy {
  private destroy$ = new Subject<void>();
  loginForm: (FormGroup & Login) | any;
  showPassword = false;
  eyeIconClass: string = 'bi-eye';

  constructor(
    private formbuilder: FormBuilder,
    public router: Router,
    public authService: AuthService,
    private authProvider: AuthProvider) {}

  ngOnDestroy(): void {
     this.destroy$.next();
     this.destroy$.complete();
  }

  ngOnInit(): void {
    this.loginForm = this.formbuilder.group({
      email: ['user@custumer.com', [Validators.required, Validators.email]],
      password: ['12345', Validators.required]
    }) as (FormGroup & Login) | any;
  }

  onLoginClick() {
    let login: Login = this.loginForm.getRawValue();
    this.authService.signIn(login)
    .pipe(
      map((response: Auth) => {
        if (response.authenticated) {
          return response;
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
      next: (response) => {
        if (response) {
          this.authProvider.createAccessToken(response);
          this.router.navigate(['/']);
        }
        else {
          throw (response);
        }
      },
      error :(response : any) =>  {
        alert(response.error);
      },
      complete() { }
    });
  }

  onTooglePassword() {
    this.showPassword = !this.showPassword;
    this.eyeIconClass = (this.eyeIconClass === 'bi-eye') ? 'bi-eye-slash' : 'bi-eye';
  }
}
