import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { AccountService } from 'src/app/account/account.service';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

  constructor(private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          console.log('Error status', error.status)
          switch(error.status) {
            case 401:
              this.router.navigateByUrl('/account');
              break;
            case 403:
              this.router.navigateByUrl('no-page');
              break;
          }
        }
        return throwError(() => new Error(error.message))
      }
    ));
  }
}
