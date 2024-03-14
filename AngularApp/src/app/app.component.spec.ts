import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { AuthService } from './services';
import { Router } from '@angular/router';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

describe('AppComponent', () => {
  let component: AppComponent;
  let fixture: ComponentFixture<AppComponent>;
  let authService: AuthService;
  let router: Router;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [AppComponent],
      imports: [HttpClientTestingModule, RouterTestingModule]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AppComponent);
    component = fixture.componentInstance;
    authService = TestBed.inject(AuthService);
    router = TestBed.inject(Router);
    httpMock = TestBed.inject(HttpTestingController);
  });

  afterEach(() => {
    httpMock.verify();
  });

  it('should create the app', () => {
    expect(component).toBeTruthy();
  });

  it('isAuthenticated should return true when user is not authenticated', () => {
    // Arrange
    spyOn(authService, 'isAuthenticated').and.returnValue(false);

    // Act
    const result = component.isAuthenticated();

    // Assert
    expect(result).toBeTrue();
    expect(authService.isAuthenticated).toHaveBeenCalled();
  });

  it('isAuthenticated should return false when user is authenticated', () => {
    // Arrange
    spyOn(authService, 'isAuthenticated').and.returnValue(true);

    // Act
    const result = component.isAuthenticated();

    // Assert
    expect(result).toBeFalse();
    expect(authService.isAuthenticated).toHaveBeenCalled();
  });

  it('logout should call authService clearLocalStorage method and navigate to root route', () => {
    // Arrange
    spyOn(authService, 'clearLocalStorage');
    spyOn(router, 'navigate');

    // Act
    component.logout();

    // Assert
    expect(authService.clearLocalStorage).toHaveBeenCalled();
    expect(router.navigate).toHaveBeenCalledWith(['/']);
  });
});
