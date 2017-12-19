import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { NetworkService } from '../../services/network-service';
import { LoginResponse } from '../../dto/login-response';

@Component({
    templateUrl: './login.page.html',
})
export class LoginPageComponent {

    tokenId: string = '';

    constructor(
        private activatedRoute: ActivatedRoute,
        private authService: AuthService,
        private networkService: NetworkService,
        private router: Router) {
    }

    login() {
        if (!this.tokenId)
            return;

        this.networkService.login(this.tokenId).subscribe(obj => {
            var response = <LoginResponse>obj;

            if (response.error) {
                console.log(response);
            } else {
                this.authService.setToken(response.jwt);
                this.router.navigateByUrl('');
            }
        })
    }
}
