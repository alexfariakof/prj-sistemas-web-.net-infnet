import { TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CustomInterceptor } from './http.interceptor.service';
import { HttpErrorResponse, HttpRequest, HttpHandler } from '@angular/common/http';
import { Observable } from 'rxjs';

describe('CustomInterceptor', () => {
  let interceptor: CustomInterceptor;
  let modalService: NgbModal;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CustomInterceptor, NgbModal],
    });

    interceptor = TestBed.inject(CustomInterceptor);
    modalService = TestBed.inject(NgbModal);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should be created', inject([CustomInterceptor], (service: CustomInterceptor) => {
    expect(service).toBeTruthy();
  }));

  it('should catch and handle HTTP errors', () => {
    // Arrange
    const mockError = new HttpErrorResponse({ status: 500, statusText: 'Internal Server Error' });

    const request = new HttpRequest('GET', 'test');
    const next: HttpHandler = {
      handle: () => {
        return new Observable((observer) => {
          observer.error(mockError);
        });
      }
    };

    let catchErrorCalled = false;

    // Act
    interceptor.intercept(request, next).subscribe({
      next: () => {},
      error: (error) => {
        catchErrorCalled = true;
        expect(error).toBe(mockError);
      }
    });
    // Assert
    expect(catchErrorCalled).toBeTrue();
  });

});
