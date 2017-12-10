import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AuthService {
    TOKEN_KEY : string = "token";
    TELEGRAM_USER_ID_KEY : string = "telegram_id";

    constructor(public _http: HttpClient) {

    }

    isLogined() : boolean {
        return sessionStorage.getItem(this.TOKEN_KEY) != null;
    }

    getTelegramId() : number {
        return 0;
    }
}