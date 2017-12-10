import { Component } from '@angular/core';
import { MatToolbarModule } from '@angular/material/toolbar';
import { ActivatedRoute } from '@angular/router';
import { NetworkService } from '../../services/network-service';

@Component({
  templateUrl: './callback.page.html',
})
export class CallBackPageComponent {
  constructor(private activatedRoute: ActivatedRoute, private networkService: NetworkService) {
    this.activatedRoute.params.subscribe(params => {
      var token = params['token'];
      console.log(token);
      this.networkService.getCurrencies(token).subscribe(e => {
        console.log(e);
      })
    });
  }
}
