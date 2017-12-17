import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { VariableService } from './variable-service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { NetworkService } from './network-service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CurrencyTrigger } from '../models/currency-trigger';
import { PriceTrigger } from '../models/price-trigger';

@Injectable()
export class TriggerService {
    constructor(
        private _http: HttpClient,
        private _variableService: VariableService,
        private _networkService: NetworkService) {
    }

    postPriceTrigger(trigger: PriceTrigger): Observable<any> {
        return this._http.post(`${this._variableService.Host}/api/trigger`, {
            "type": "price",
            "trigger": trigger
        });;
    }

    getPriceTriggers(): Observable<PriceTrigger[]> {
        return this._http.get(`${this._variableService.Host}/api/trigger?type=price`).map(e => { return <PriceTrigger[]>e });
    }
}