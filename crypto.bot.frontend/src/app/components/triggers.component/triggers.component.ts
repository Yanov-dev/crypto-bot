import { Component, OnInit } from "@angular/core";
import { CurrencyTrigger } from "../../models/currency-trigger";
import { TriggerService } from "../../services/trigger-service";
import { MatDialog } from "@angular/material";
import { AddTriggerDialog } from "../add.trigger.component/add.trigger.dialog";

@Component({
  templateUrl: './triggers.component.html',
})

export class TriggersComponent implements OnInit {

  triggers: CurrencyTrigger[];

  constructor(
    private _triggerService: TriggerService,
    private _dialog: MatDialog) {
  }

  ngOnInit(): void {
    this._triggerService.getTriggers().subscribe(triggers => {
      this.triggers = triggers;
    })
  }

  addTrigger() {
    let dialogRef = this._dialog.open(AddTriggerDialog, {
      width: '250px',
      data: { }
    });
  }
}