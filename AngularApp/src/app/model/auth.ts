export interface Auth {
  authenticated: boolean,
  access_token: string,
  expires_in: string
  token_type: string,
  refresh_token: string;
  scope: string;
}
