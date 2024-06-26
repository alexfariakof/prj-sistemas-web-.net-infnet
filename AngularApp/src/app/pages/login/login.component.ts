import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Auth, Login } from '../../model';
import { AuthService } from '../../services';
import { AuthProvider } from '../../provider/auth.provider';
import { map, catchError } from 'rxjs';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  changeDetection: ChangeDetectionStrategy.Default
})

export class LoginComponent implements OnInit {
  loginForm: (FormGroup & Login) | any;
  showPassword = false;
  eyeIconClass: string = 'bi-eye';

  constructor(
    private formbuilder: FormBuilder,
    public router: Router,
    public authService: AuthService,
    private authProvider: AuthProvider) {}

  ngOnInit(): void {
    this.loginForm = this.formbuilder.group({
      email: ['user@customer.com', [Validators.required, Validators.email]],
      password: ['12345T!', Validators.required]
    }) as (FormGroup & Login) | any;
  }

  onLoginClick() {
    let login: Login = this.loginForm.getRawValue();
    this.authService.signIn(login)
    .pipe(
      map((response: Auth) => {
        if (response) {
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
