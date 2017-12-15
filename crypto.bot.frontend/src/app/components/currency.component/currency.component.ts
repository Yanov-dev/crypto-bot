import { Component, OnInit } from "@angular/core";
import { Currency } from "../../models/currency";
import { CurrencyService } from "../../services/currency-service";
import { MatTableDataSource } from "@angular/material";

@Component({
    templateUrl: './currency.component.html',
})

export class CurrencyComponent implements OnInit {

    currencies: Currency[];
    dataSource: MatTableDataSource<Currency>;
    displayedColumns = ['rank', 'id', 'name', 'priceUsd', 'symbol'];

    constructor(private _currencyService: CurrencyService) {
    }

    ngOnInit(): void {
        this._currencyService.getCurrencies().subscribe(currencies => {
            this.dataSource = new MatTableDataSource<Currency>(currencies);
            this.currencies = currencies;
        })
    }
}