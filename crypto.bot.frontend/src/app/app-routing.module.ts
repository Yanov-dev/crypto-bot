import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomePageComponent } from './pages/home-page/home.page';
import { CallBackPageComponent } from './pages/callback-page/callback.page';
import { TriggersComponent } from './components/triggers.component/triggers.component';
import { CurrencyComponent } from './components/currency.component/currency.component';
import { LoginPageComponent } from './pages/login-page/login.page';
import { AuthorizationGuard } from './services/auth-guard';

const appRoutes: Routes = [
  {
    path: '',
    component: TriggersComponent,
    pathMatch: 'full'
  },
  { path: 'triggers', component: TriggersComponent, canActivate: [AuthorizationGuard] },
  { path: "currencies", component: CurrencyComponent, canActivate: [AuthorizationGuard] },
  { path: 'login', component: LoginPageComponent },
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

