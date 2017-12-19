import { Component, OnInit, ViewChild } from "@angular/core";
import { Currency } from "../../models/currency";
import { CurrencyService } from "../../services/currency-service";
import { MatTableDataSource, MatSort } from "@angular/material";

@Component({
    templateUrl: './currency.component.html',
    styleUrls: ['currency.component.css']
})

export class CurrencyComponent implements OnInit {

    currencies: Currency[];
    dataSource: MatTableDataSource<Currency>;
    displayedColumns = ['rank', 'id', 'name', 'priceUsd', 'symbol'];

    @ViewChild(MatSort) sort: MatSort;

    constructor(private _currencyService: CurrencyService) {
    }

    ngOnInit(): void {
        this._currencyService.getCurrencies().subscribe(currencies => {
            this.dataSource = new MatTableDataSource<Currency>(currencies);
            this.dataSource.sort = this.sort;
            this.currencies = currencies;
        })
    }
}