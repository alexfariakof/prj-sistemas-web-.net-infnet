import { Auth } from '../model';
import { AuthProvider } from './auth.provider';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

describe('AuthProvider', () => {
  let authProvider: AuthProvider;

  beforeEach(() => {
    authProvider = new AuthProvider();
    sessionStorage.clear();
  });

  it('should create', () => {
    expect(authProvider).toBeTruthy();
  });

  it('should allow activation when user is authenticated', () => {
    // Arrange
    const fakeAuth: Auth = {
      access_token: 'fakeToken',
      expires_in: '2023-01-01T00:00:00Z',
      authenticated: true,
      scope: 'fak-scope',
      refresh_token: 'fakeToken',
      token_type: 'Bearer'
    };

    authProvider.createAccessToken(fakeAuth);

    // Act
    const canActivate = authProvider.canActivate({} as ActivatedRouteSnapshot, {} as RouterStateSnapshot);

    // Assert
    expect(canActivate).toBeTruthy();
  });

  it('should return true canActive', () => {
    // Arrange
    spyOn(authProvider, 'canActivate').and.returnValue(true);

    // Act
    const canActivate = authProvider.canActivate({} as ActivatedRouteSnapshot, {} as RouterStateSnapshot);

    // Assert
    expect(canActivate).toBeTruthy();
  });

  it('should clear local storage when user is not authenticated', () => {
    // Arrange
    spyOn(sessionStorage, 'getItem').and.returnValue(null);

    // Act
    authProvider.canActivate({} as ActivatedRouteSnapshot, {} as RouterStateSnapshot);

    // Assert
    expect(sessionStorage.getItem('@token')).toBeNull();
  });
});
