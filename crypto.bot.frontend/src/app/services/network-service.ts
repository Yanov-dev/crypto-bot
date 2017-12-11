import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { VariableService } from './variable-service';

@Injectable()
export class NetworkService {
    constructor(public _http: HttpClient, private variableService : VariableService) {
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