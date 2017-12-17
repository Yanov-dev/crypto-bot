import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material";
import { Currency } from "../../../models/currency";
import { FormControl } from "@angular/forms";
import { Observable } from "rxjs/Observable";
import { startWith } from 'rxjs/operators/startWith';
import { map } from 'rxjs/operators/map';

@Component({
  templateUrl: 'add.price.trigger.dialog.html',
})
export class AddPriceTriggerDialog {
  stateCtrl: FormControl;
  priceCtrl: FormControl = new FormControl();
  currencies: Currency[];
  filteredCurrencies: Observable<Currency[]>

  operator: any;

  constructor(
    public dialogRef: MatDialogRef<AddPriceTriggerDialog>,
    @Inject(MAT_DIALOG_DATA) public data: any) {

    this.currencies = data.currencies;

    this.stateCtrl = new FormControl();
    this.filteredCurrencies = this.stateCtrl.valueChanges
      .pipe(
      startWith(''),
      map(currency => currency ? this.filterStates(currency) : this.currencies.slice())
      );
  }

  addClick() {
    this.dialogRef.close({
      operator: this.operator,
      currency: this.stateCtrl.value,
      price: this.priceCtrl.value
    });
  }

  cancelClick(): void {
    this.dialogRef.close();
  }

  filterStates(name: string) {
    return this.currencies.filter(currency =>
      currency.name.toLowerCase().indexOf(name.toLowerCase()) === 0);
  }
}