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
        return this._http.get(`${this._variableService.Host}/api/currency`).map(e => { return this.sortCurrencies(<Currency[]>e) });
    }

    private sortCurrencies(currencies: Currency[]): Currency[] {
        return currencies.sort(function (a, b) {
            if (a.rank > b.rank) {
                return 1;
            }
            if (a.rank < b.rank) {
                return -1;
            }

            return 0;
        })
    }
}