import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class NetworkService {

    host: string = "http://localhost:5000";

    constructor(public _http: HttpClient) {
    }

    getCurrencies(token) {
        return this._http.get(`${this.host}/api/currency`, {
            headers: {
                'Authorization': `Bearer ${token}`
            }
        });
    }
}