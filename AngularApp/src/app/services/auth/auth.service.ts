import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Login } from '../../model';

@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private routeUrl: string = 'api/auth';
  private routeSTSSeverUrl: string = 'http://localhost:5055/connect/token';

  constructor(private httpClient: HttpClient) { }

  signInIdentityServerSTS(login: Login): Observable<any> {
    let body = new URLSearchParams();
    body.set("username", login.email);
    body.set("password", login.password);
    body.set("client_id", "client-angular-lite-streaming");
    body.set("client_secret", "lite-streaming-secret");
    body.set("grant_type", "password");
    body.set("scope", "lite-streaming-scope");

     let options ={
       headers: new HttpHeaders().set('Content-Type', 'application/x-www-form-urlencoded')
     }

    return this.httpClient.post(`${this.routeSTSSeverUrl }`, body.toString(), options);
  }

  signIn(login: Login): Observable<any> {
    return this.httpClient.post<Login>(`${ this.routeUrl }`, login);
  }
}
