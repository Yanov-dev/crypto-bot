import { Injectable } from '@angular/core';
import { VariableService } from './variable-service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from './auth-service';

@Injectable()
export class NetworkService {
    constructor(
        private _http: HttpClient,
        private variableService: VariableService,
        private _authService: AuthService) {
    }

    getCurrencies(token) {
        return this._http.get(`${this.variableService.Host}/api/currency`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    }

    login(tokenId: string) {
        return this._http.post(`${this.variableService.Host}/api/account/login`, { tokenId: tokenId });
    }
}