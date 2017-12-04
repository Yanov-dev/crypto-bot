import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ActivatedRoute } from '@angular/router';

@Component({
  templateUrl: './callback.page.html',
})
export class CallBackPageComponent {
  constructor(private activatedRoute: ActivatedRoute) {
    this.activatedRoute.params.subscribe(params => {
      var token = params['token'];
      console.log(token);
    });
  }
}
