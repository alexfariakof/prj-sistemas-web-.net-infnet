import { TestBed, inject } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { AuthService } from './auth.service';
import { Auth, Login } from '../../model';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { CustomInterceptor } from '../../interceptors/http.interceptor.service';

describe('Unit Test AuthService', () => {
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
    imports: [],
    providers: [AuthService,
        { provide: HTTP_INTERCEPTORS, useClass: CustomInterceptor, multi: true, }, provideHttpClient(withInterceptorsFromDi()), provideHttpClientTesting()]
});
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', inject([AuthService], (service: AuthService) => {
    // Assert
    expect(service).toBeTruthy();
  }));

  it('should send a POST request to the api/auth endpoint', inject(
    [AuthService, HttpTestingController],
    (service: AuthService, httpMock: HttpTestingController) => {
      const loginData: Login = {
        email: 'teste@teste.com',
        password: 'teste',
      };

      const mockResponse: Auth = {
        access_token: 'fakeToken',
        expires_in: '2023-01-01T00:00:00Z',
        authenticated: true,
        scope: 'fake-scope',
        refresh_token: 'fakeToken',
        token_type: 'Bearer'
      };

      service.signIn(loginData).subscribe((response: any) => {
        expect(response).toBeTruthy();
      });
      const expectedUrl = 'http://localhost:5055/connect/token';
      const req = httpMock.expectOne(expectedUrl);
      expect(req.request.method).toBe('POST');
      req.flush(mockResponse);
      httpMock.verify();
    }
  ));

});
