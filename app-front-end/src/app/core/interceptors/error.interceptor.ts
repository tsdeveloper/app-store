import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {Router} from '@angular/router';
import {Injectable} from '@angular/core';
import {catchError} from 'rxjs/operators';
import {ToastrService} from 'ngx-toastr';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor{

    constructor(private router: Router, private toastr: ToastrService) {}
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error => {
                if (error){

                    if (error.status === 0 && !error.ok){
                        this.toastr.error('Not found server');
                    }

                    if (error.status === 400){
                        this.toastr.error(error.error.message, error.error.statusCode);
                    }
                    if (error.status === 401){
                        this.toastr.error(error.error.message, error.error.statusCode);
                    }

                    if (error.status === 404){
                        this.router.navigateByUrl('/not-found');
                    }
                    if (error.status === 500){
                        this.router.navigateByUrl('/server-error');
                    }
                }
                return throwError(error);
            })
        );
    }
}
