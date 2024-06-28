import { TestBed, inject } from '@angular/core/testing';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { CustomInterceptor } from './http.interceptor.service';
import { HttpErrorResponse, HttpRequest, HttpHandler, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RouterTestingModule } from '@angular/router/testing';
import { Location } from '@angular/common';
import { DummyComponent } from '../__mocks__/mock.component';

describe('CustomInterceptor', () => {
  let interceptor: CustomInterceptor;
  let modalService: NgbModal;
  let httpMock: HttpTestingController;
  let router: Router;
  let location: Location;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule.withRoutes([
          { path: 'access-denied', component: DummyComponent }
        ])
      ],
      providers: [
        CustomInterceptor,
        NgbModal,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
      ]
    });

    interceptor = TestBed.inject(CustomInterceptor);
    modalService = TestBed.inject(NgbModal);
    httpMock = TestBed.inject(HttpTestingController);
    router = TestBed.inject(Router);
    location = TestBed.inject(Location);

    router.initialNavigation();
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', inject([CustomInterceptor], (service: CustomInterceptor) => {
    expect(service).toBeTruthy();
  }));

  it('should catch 403 errors and navigate to access-denied', (done) => {
    const mockError = new HttpErrorResponse({ status: 403, statusText: 'Forbidden' });

    const request = new HttpRequest('GET', 'test');
    const next: HttpHandler = {
      handle: () => {
        return new Observable((observer) => {
          observer.error(mockError);
        });
      }
    };

    spyOn(router, 'navigate').and.callFake(() => Promise.resolve(true));

    interceptor.intercept(request, next).subscribe({
      next: () => {},
      error: (error) => {
        expect(error).toBe(mockError);
        expect(router.navigate).toHaveBeenCalledWith(['access-denied']);
        done();
      }
    });
  });
});

