import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomePageComponent } from './pages/home-page/home.page';
import { CallBackPageComponent } from './pages/callback-page/callback.page';
import { TriggersComponent } from './components/triggers.component';

const appRoutes: Routes = [
  {
    path: 'triggers', component: TriggersComponent
  },
  {
    path: 'callback/:tokenId',
    component: CallBackPageComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(appRoutes, { enableTracing: false })],
  exports: [RouterModule]
})
export class AppRoutingModule {
}

