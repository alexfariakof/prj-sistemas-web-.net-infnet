import { Component } from '@angular/core';
import { AuthService } from './services';
import { Router } from '@angular/router';
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  constructor(private authService: AuthService, private router: Router) {}

  isAuthenticated(): boolean {
    return !this.authService.isAuthenticated();
  }

  logout = (): void => {
    this.authService.clearLocalStorage();
    this.router.navigate(['/']);
  }

  title = 'AngularApp';
}
