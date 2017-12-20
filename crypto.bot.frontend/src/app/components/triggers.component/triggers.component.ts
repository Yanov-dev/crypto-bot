import { Component, OnInit, ViewChild } from "@angular/core";
import { CurrencyTrigger } from "../../models/currency-trigger";
import { TriggerService } from "../../services/trigger-service";
import { MatDialog, MatTableDataSource, MatSort } from "@angular/material";
import { CurrencyService } from "../../services/currency-service";
import { Observable } from "rxjs/Observable";
import 'rxjs/add/observable/forkJoin';
import { Currency } from "../../models/currency";
import { AddPriceTriggerDialog } from "../dialogs/add.price.trigger.dialog/add.price.trigger.dialog";
import { PriceTrigger } from "../../models/price-trigger";

@Component({
  templateUrl: './triggers.component.html',
})

export class TriggersComponent implements OnInit {

  triggers: PriceTrigger[];
  currencies: Currency[];

  isLoading: boolean;

  @ViewChild(MatSort) sort: MatSort;

  dataSource: MatTableDataSource<PriceTrigger>;
  displayedColumns = ['сurrency', 'operator', 'price', 'remove'];

  constructor(
    private _triggerService: TriggerService,
    private _currencyService: CurrencyService,
    private _dialog: MatDialog) {
  }

  ngOnInit(): void {
    this.isLoading = true;
    Observable.forkJoin(
      this._currencyService.getCurrencies(),
      this._triggerService.getPriceTriggers()).subscribe(res => {
        this.currencies = res[0];
        this.setTriggers(res[1]);
        this.isLoading = false;
      })
  }

  setTriggers(triggers: PriceTrigger[]) {
    this.triggers = triggers;
    this.dataSource = new MatTableDataSource<PriceTrigger>(this.triggers);
    this.dataSource.sort = this.sort;
    console.log(this.triggers);
  }

  removeTrigger(id: string) {
    this.isLoading = true;
    this._triggerService.deletePriceTrigger(id).subscribe(e => {
      this._triggerService.getPriceTriggers().subscribe(triggers => {
        this.setTriggers(triggers);
        this.isLoading = false;
      });
    });
  }

  addTrigger() {
    let dialogRef = this._dialog.open(AddPriceTriggerDialog, {
      width: '250px',
      data: {
        currencies: this.currencies
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (!result)
        return;

      this.isLoading = true;
      this._triggerService.postPriceTrigger(result).subscribe(e => {
        this._triggerService.getPriceTriggers().subscribe(triggers => {
          this.setTriggers(triggers);
          this.isLoading = false;
        });
      });
    });
  }
}