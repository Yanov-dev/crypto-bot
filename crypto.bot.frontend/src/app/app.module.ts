import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { MatToolbarModule, MatButtonModule } from '@angular/material';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { AppRoutingModule } from './/app-routing.module';
import { CommonModule } from '@angular/common';
import { HomePageComponent } from './pages/home-page/home.page';
import { CallBackPageComponent } from './pages/callback-page/callback.page';
import { NetworkService } from './services/network-service';
import { HttpClient, HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    HomePageComponent,
    AppComponent,
    CallBackPageComponent
  ],
  imports: [
    HttpClientModule,
    CommonModule,
    BrowserAnimationsModule,
    BrowserModule,
    MatToolbarModule,
    MatButtonModule,
    AppRoutingModule
  ],
  providers: [
    NetworkService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
