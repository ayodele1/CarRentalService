import { IReservation } from './reservation';
import { Http, Response } from '@angular/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';

@Injectable()
export class ReservationApiService {
    private apiUrl = 'http://localhost:5000/api/';
    constructor(private _http: Http) {

    }

    getReservations(): Observable<IReservation[]> {
        return this._http.get(this.apiUrl.concat('reservations?includevehicledetails=true'))
            .map((response: Response) => <IReservation[]>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    private handleError(error: Response) {
        console.error('This is the error' + error);
        return Observable.throw(error.json().error || 'Server error');
    }
}