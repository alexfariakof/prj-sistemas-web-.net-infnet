import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { Auth } from '../model';
@Injectable({
  providedIn: 'root',
})

export class AuthProvider implements CanActivate {
  private accessTokenSubject = new BehaviorSubject<string | undefined>(undefined);

  accessToken$ = this.accessTokenSubject.asObservable();

  constructor() {
    try {
      const accessToken = sessionStorage.getItem('@token');
      if (accessToken) {
        this.setAccessToken(accessToken);
      } else {
        this.clearSessionStorage();
      }
    } catch {
      this.clearSessionStorage();
    }
  }

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot ): boolean {
    return this.isAuthenticated();
  }

  public clearSessionStorage() {
    this.setAccessToken(undefined);
    sessionStorage.clear();
  }

  private setAccessToken(token: string | undefined) {
    this.accessTokenSubject.next(token);
  }

  isAuthenticated(): boolean {
    const accessToken = this.accessTokenSubject.getValue() ?? sessionStorage.getItem('@token') ;
    if (accessToken === null || accessToken === undefined) {
      this.clearSessionStorage();
      return false;
    }
    return true;
  }

  createAccessToken(auth: Auth): void {
    sessionStorage.setItem('@token', auth.access_token);
    this.setAccessToken(auth.access_token);
  }
}
