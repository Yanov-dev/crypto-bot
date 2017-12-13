import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions } from '@angular/http';
import { VariableService } from './variable-service';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { NetworkService } from './network-service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CurrencyTrigger } from '../models/currency-trigger';

@Injectable()
export class TriggerService {
    constructor(
        private _http: HttpClient,
        private _variableService: VariableService,
        private _networkService: NetworkService) {
    }

    getTriggers(): Observable<CurrencyTrigger[]> {
        return this._http.get(`${this._variableService.Host}/api/trigger`).map(e => { return <CurrencyTrigger[]>e });
    }
}