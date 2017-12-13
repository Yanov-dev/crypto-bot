import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { AuthService } from './auth-service';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        var token = localStorage.getItem("token");
        return next.handle(request.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        }));
    }
}