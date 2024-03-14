import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Auth, Login } from 'src/app/model';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private accessTokenSubject = new BehaviorSubject<string | undefined>(undefined);
  private routeUrl:string = 'auth';

  accessToken$ = this.accessTokenSubject.asObservable();

  constructor(private httpClient: HttpClient) {
    try {
      const accessToken = localStorage.getItem('@token');
      if (accessToken) {
        this.setAccessToken(accessToken);
      } else {
        this.clearLocalStorage();
      }
    } catch {
      this.clearLocalStorage();
    }
  }

  signIn(login: Login): Observable<any> {
    return this.httpClient.post<Login>(`${ this.routeUrl }`, login);
  }

  public clearLocalStorage() {
    this.setAccessToken(undefined);
    localStorage.clear();
  }

  private setAccessToken(token: string | undefined) {
    this.accessTokenSubject.next(token);
  }

  isAuthenticated(): boolean {
    const accessToken = this.accessTokenSubject.getValue() ?? localStorage.getItem('@token');
    if (accessToken === null || accessToken === undefined) {
      this.clearLocalStorage();
      return false;
    }
    return true;
  }

  createAccessToken(auth: Auth): Boolean {
    try {
      localStorage.setItem('@token', auth.accessToken ?? '');
      this.setAccessToken(auth.accessToken);
      return true;
    } catch (error) {
      return false;
    }
  }
}
