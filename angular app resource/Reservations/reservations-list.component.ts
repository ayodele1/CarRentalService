import { Component, OnInit } from '@angular/core';
import { IReservation } from './reservation';
import { ReservationApiService } from './reservation-api.service';
import 'rxjs/add/operator/do';

@Component({
    selector: 'reservationsList',
    //templateUrl: 'app/Reservations/views/reservations-list-view.component.html'
    templateUrl: 'Dashboard/ReservationList'
})


export class ReservationComponent implements OnInit {
    errorMessage: string;
    userReservations: IReservation[];
    name: string = "ayodele";
    showDetails: boolean = false;
    showUpdateForm: boolean = false;

    constructor(private _reservationApiService: ReservationApiService) {
        
    }

    ngOnInit(): void {
        this._reservationApiService.getReservations()
            .subscribe(
            reservations => {
                this.userReservations = reservations;
                console.log("The one in the lambda" + reservations);
                console.log("User Reservations Class Variable:  " + this.userReservations);
                    },
            error => this.errorMessage = <any>error
            );        
    }

    toggleDetails(): void {
        this.showDetails = !this.showDetails;
    }

    toggleUpdate(): void {
        this.showUpdateForm = !this.showUpdateForm;
    }
}