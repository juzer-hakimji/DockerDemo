import { Injectable, Inject } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from '../Services/AuthenticationService/authentication.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  BaseURL: string;
  constructor(private authenticationService: AuthenticationService, @Inject('BASE_URL') baseUrl: string) {
    this.BaseURL = baseUrl;
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let currentUser = this.authenticationService.currentUserValue;
    const isLoggedIn = currentUser && currentUser.Token;
    const isApiUrl = request.url.startsWith(this.BaseURL);
    if (isLoggedIn && isApiUrl) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.Token}`
        }
      });
    }

    return next.handle(request);
  }
}
