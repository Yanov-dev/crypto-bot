import { Injectable } from '@angular/core';
import { VariableService } from './variable-service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { HttpClient } from '@angular/common/http';
import { CurrencyTrigger } from '../models/currency-trigger';
import { Currency } from '../models/currency';

@Injectable()
export class CurrencyService {
    constructor(
        private _http: HttpClient,
        private _variableService: VariableService) {
    }

    getCurrencies(): Observable<Currency[]> {
        return this._http.get(`${this._variableService.Host}/api/currency`).map(e => { return <Currency[]>e });
    }
}