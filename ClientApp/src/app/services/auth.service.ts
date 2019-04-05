import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import * as auth0 from 'auth0-js';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Profile } from 'selenium-webdriver/firefox';

@Injectable()
export class AuthService {

  private _idToken: string;
  private _accessToken: string;
  private _expiresAt: number;
  private _roles: string[] = [];
  private _userProfile: any;

  auth0 = new auth0.WebAuth({
    clientID: 'X7WBO92Dg5WKw1q3IeNzc3C1MXTrZtzO',
    domain: 'vdeal.auth0.com',
    responseType: 'token id_token',
    redirectUri: 'https://localhost:5001/vehicles',
    scope: 'openid profile'
  });

  constructor(public router: Router) {
    this._idToken = '';
    this._accessToken = '';
    this._expiresAt = 0;
    this.getAuthInfo();
    this.getUserInfo();
  }

  get accessToken(): string {
    return this._accessToken;
  }

  get idToken(): string {
    return this._idToken;
  }

  public login(): void {
    this.auth0.authorize();
  }

  public handleAuthentication(): void {
    this.auth0.parseHash((err, authResult) => {
      if (authResult && authResult.accessToken && authResult.idToken) {
        window.location.hash = '';
        this.localLogin(authResult);
        this.router.navigate(['/']);
      } else if (err) {
        this.router.navigate(['/']);
        console.log(err);
      }
    });
  }

  private localLogin(authResult): void {
    this.saveAuthInformation(authResult);
    this.getUserInfo();
  }

  public saveAuthInformation(authResult) {
    // Set the time that the access token will expire at
    const expiresAt = (authResult.expiresIn * 1000) + new Date().getTime();
    this._accessToken = authResult.accessToken;
    this._idToken = authResult.idToken;
    this._expiresAt = expiresAt;

    // Set isLoggedIn flag in localStorage
    localStorage.setItem('isLoggedIn', 'true');
    localStorage.setItem('token', this._idToken);
    localStorage.setItem('accessToken', this._accessToken);
    localStorage.setItem('expiresAt', this._expiresAt.toString());
    this.getProfile((err, profile) => {
      this._userProfile = profile;
    });
  }

  public getUserInfo() {
    var token = localStorage.getItem('token');
    if (!token)
      return;

    var jwtHelper = new JwtHelperService();
    var decodedToken = jwtHelper.decodeToken(token);
    this._roles = decodedToken['https://api.vdeal.com/roles'];
  }

  public getAuthInfo() {
    var token = localStorage.getItem('token');
    if (!token)
      return;

    this._accessToken = localStorage.getItem('accessToken');
    this._idToken = localStorage.getItem('token');
    this._expiresAt = parseInt(localStorage.getItem('expiresAt'));
  }

  public logout(): void {
    // Remove tokens and expiry time
    this._accessToken = '';
    this._idToken = '';
    this._expiresAt = 0;
    this._roles = [];
    this._userProfile = "";

    // Remove data from localStorage
    localStorage.removeItem('isLoggedIn');
    localStorage.removeItem('token');
    localStorage.removeItem('accessToken');
    localStorage.removeItem('expiresAt');
    // Go back to the home route
    this.router.navigate(['/']);
  }

  public renewTokens(): void {
    this.auth0.checkSession({}, (err, authResult) => {
      if (authResult && authResult.accessToken && authResult.idToken) {
        this.localLogin(authResult);
      } else if (err) {
        alert(`Could not get a new token (${err.error}: ${err.error_description}).`);
        this.logout();
      }
    });
  }

  public isAuthenticated(): boolean {
    // Check whether the current time is past the
    // access token's expiry time
    return new Date().getTime() < this._expiresAt;
  }

  public isInRole(role: string) {
    //TODO: Prevent infinite loop here
    return this._roles.indexOf(role.toLowerCase()) > -1;
  }

  public userHasRoles(roles: string[]){
    for(var role of roles){
      var result = this.isInRole(role);
      if(result)
        return true;
    }
    return false;
  }

  public getProfile(cb): void {
    if (!this._accessToken) {
      throw new Error('Access Token must exist to fetch profile');
    }

    const self = this;
    this.auth0.client.userInfo(this._accessToken, (err, profile) => {
      if (profile) {
        self._userProfile = profile;
      }
      cb(err, profile);
    });
  }





}