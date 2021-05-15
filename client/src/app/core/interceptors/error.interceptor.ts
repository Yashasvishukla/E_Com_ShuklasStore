import {
  HttpEvent,
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private toastrService: ToastrService) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error) => {
        if (error) {
          if (error.status === 400) {
            if (error.error.errors)
            {
              console.log(error);
              throw error.error;
            }
            else
            {
              this.toastrService.error(error.error.message, error.statusCode);
            }
          }
          if (error.status === 401) {
            this.toastrService.error(error.error.message, error.statusCode);
          }
          // if (error.status === 404) {
          //   this.router.navigateByUrl('/not-found');
          // }

          if (error.status === 404) {
            const navigationExtras: NavigationExtras = {state: {error: error.error}};
            this.router.navigateByUrl('server-error', navigationExtras);
          }
        }
        return throwError(error);
      })
    );
  }
}
