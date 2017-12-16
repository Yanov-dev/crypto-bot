import { Component, OnInit } from "@angular/core";
import { CurrencyTrigger } from "../../models/currency-trigger";
import { TriggerService } from "../../services/trigger-service";
import { MatDialog } from "@angular/material";
import { CurrencyService } from "../../services/currency-service";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/observable/forkJoin';
import { Currency } from "../../models/currency";
import { AddPriceTriggerDialog } from "../dialogs/add.price.trigger.dialog/add.price.trigger.dialog";

@Component({
  templateUrl: './triggers.component.html',
})

export class TriggersComponent implements OnInit {

  triggers: CurrencyTrigger[];
  currencies: Currency[];

  constructor(
    private _triggerService: TriggerService,
    private _currencyService: CurrencyService,
    private _dialog: MatDialog) {
  }

  ngOnInit(): void {
    Observable.forkJoin(
      this._currencyService.getCurrencies(),
      this._triggerService.getTriggers()).subscribe(res => {
        this.currencies = res[0];
        this.triggers = res[1];
      })
  }

  addTrigger() {
    let dialogRef = this._dialog.open(AddPriceTriggerDialog, {
      width: '250px',
      data: {
        currencies: this.currencies
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(result);
    });
  }
}