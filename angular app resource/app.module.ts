import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { ReservationComponent } from './Reservations/reservations-list.component';
import { ReservationApiService } from './Reservations/reservation-api.service';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule
    ],
  declarations: [
      AppComponent,
      ReservationComponent
    ],
  providers: [
      ReservationApiService
  ],
  bootstrap:    [ AppComponent ]
})
export class AppModule { }
