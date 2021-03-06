import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatToolbarModule, MatButtonModule, MatDialogModule, MatAutocompleteModule, MatInputModule, MatSelectModule, MatSortModule } from '@angular/material';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './/app-routing.module';
import { CommonModule } from '@angular/common';
import { HomePageComponent } from './pages/home-page/home.page';
import { CallBackPageComponent } from './pages/callback-page/callback.page';
import { NetworkService } from './services/network-service';
import { HttpClient, HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthService } from './services/auth-service';
import { VariableService } from './services/variable-service';
import { TriggerService } from './services/trigger-service';
import { AuthenticationInterceptor } from './services/authentication-interceptor';
import { CurrencyService } from './services/currency-service';
import { TriggersComponent } from './components/triggers.component/triggers.component';
import { CurrencyComponent } from './components/currency.component/currency.component';
import { MatTableModule } from '@angular/material/table';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AddPriceTriggerDialog } from './components/dialogs/add.price.trigger.dialog/add.price.trigger.dialog';
import { LoadingCardComponent } from './components/loading.card.component/loading.card.component';
import { LoadingModule, ANIMATION_TYPES } from 'ngx-loading';
import { LoginPageComponent } from './pages/login-page/login.page';
import { AuthorizationGuard } from './services/auth-guard';
import { ChartsModule } from 'ng2-charts';

@NgModule({
  declarations: [
    AddPriceTriggerDialog,
    HomePageComponent,
    CurrencyComponent,
    LoadingCardComponent,
    AppComponent,
    CallBackPageComponent,
    LoginPageComponent,
    TriggersComponent
  ],
  imports: [
    LoadingModule.forRoot({
      animationType: ANIMATION_TYPES.threeBounce,
      backdropBackgroundColour: 'rgba(0,0,0,0.3)',      
      primaryColour: '#ffffff',
      secondaryColour: '#ffffff',      
      tertiaryColour: '#ffffff'
    }),
    ChartsModule,
    MatSortModule,
    MatInputModule,
    MatSelectModule,
    FormsModule,
    ReactiveFormsModule,
    MatAutocompleteModule,
    MatDialogModule,
    MatTableModule,
    HttpClientModule,
    CommonModule,
    BrowserAnimationsModule,
    BrowserModule,
    MatToolbarModule,
    MatButtonModule,
    AppRoutingModule
  ],
  entryComponents: [
    AddPriceTriggerDialog
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthenticationInterceptor,
      multi: true,
    },
    AuthorizationGuard,
    TriggerService,
    NetworkService,
    CurrencyService,
    AuthService,
    VariableService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
