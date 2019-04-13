import { AuthService } from './../services/auth.service';
import { Injectable } from '@angular/core';
import { HttpRequest,HttpHandler,HttpEvent,HttpInterceptor} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {
  constructor(public auth: AuthService) {}
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    //TODO: not intercept every http post
    request = request.clone({
      setHeaders: {
        Authorization: `Bearer ${this.auth.getTokenFromLocalStorage()}`
      }
    });
    return next.handle(request);
  }
}