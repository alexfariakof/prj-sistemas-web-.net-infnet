import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, finalize, switchMap } from 'rxjs/operators';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { LoadingComponent } from '../components';
import { AuthProvider } from '../provider/auth.provider';

@Injectable()
export class CustomInterceptor implements HttpInterceptor {
  private activeRequests: number = 0;
  private isModalOpen$: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);

  constructor(private authProvider: AuthProvider, private modalService: NgbModal) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    this.showLoader();

    return this.authProvider.accessToken$.pipe(

      switchMap((accessToken) => {
          const modifiedRequest = request.clone({
          url: `${request.url}`,
          setHeaders: {
            Authorization: `Bearer ${localStorage.getItem('@token') ?? accessToken}`
          }
        });
        return next.handle(modifiedRequest).pipe(
          catchError((error: HttpErrorResponse) => {
            return throwError(() => error);
          }),
          finalize(() => this.hideLoader())
        );
      }),
      catchError((error) => {
        console.log(error);
        return throwError(() => error);
      })
    );
  }

  private showLoader(): void {
    if (this.activeRequests === 0) {
      this.isModalOpen$.next(true);
      const modalRef = this.modalService.open(LoadingComponent, {
        centered: true,
        fullscreen: true,
        windowClass: 'loading-modal',
      });
      modalRef.result.then(
        () => {},
        () => {
          if (!this.isModalOpen$.value) {
            this.activeRequests = 0;
          }
        }
      );
    }
    this.activeRequests++;
  }

  private hideLoader(): void {
    this.activeRequests--;
    if (this.activeRequests === 0) {
      this.isModalOpen$.next(false);
      this.modalService.dismissAll();
    }
  }
}
