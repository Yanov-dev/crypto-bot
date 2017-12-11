import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { VariableService } from './variable-service';

@Injectable()
export class AuthService {
    TOKEN_KEY: string = "token";
    TELEGRAM_USER_ID_KEY: string = "telegram_id";

    constructor(
        private _http: HttpClient,
        private variableService: VariableService) {

    }

    isLogined(): boolean {
        return this.getToken() != null;
    }

    setToken(jwt: string) {
        sessionStorage.setItem(this.TOKEN_KEY, jwt);
    }

    getToken(): string {
        return sessionStorage.getItem(this.TOKEN_KEY);
    }

    getTelegramId(): number {
        return 0;
    }
}