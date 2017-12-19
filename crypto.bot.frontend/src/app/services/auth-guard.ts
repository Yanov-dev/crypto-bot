import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from './auth-service';

@Injectable()
export class AuthorizationGuard implements CanActivate {
    constructor(
        private authService: AuthService,
         private router: Router) {
    }

    canActivate(): boolean {
        var isLogined = this.authService.isLogined();

        if (!isLogined)
            this.router.navigateByUrl('login');

        return isLogined;
    }
}
