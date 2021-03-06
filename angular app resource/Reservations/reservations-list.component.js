"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var reservation_api_service_1 = require('./reservation-api.service');
require('rxjs/add/operator/do');
var ReservationComponent = (function () {
    function ReservationComponent(_reservationApiService) {
        this._reservationApiService = _reservationApiService;
        this.name = "ayodele";
        this.showDetails = false;
        this.showUpdateForm = false;
    }
    ReservationComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._reservationApiService.getReservations()
            .subscribe(function (reservations) {
            _this.userReservations = reservations;
            console.log("The one in the lambda" + reservations);
            console.log("User Reservations Class Variable:  " + _this.userReservations);
        }, function (error) { return _this.errorMessage = error; });
    };
    ReservationComponent.prototype.toggleDetails = function () {
        this.showDetails = !this.showDetails;
    };
    ReservationComponent.prototype.toggleUpdate = function () {
        this.showUpdateForm = !this.showUpdateForm;
    };
    ReservationComponent = __decorate([
        core_1.Component({
            selector: 'reservationsList',
            //templateUrl: 'app/Reservations/views/reservations-list-view.component.html'
            templateUrl: 'Dashboard/ReservationList'
        }), 
        __metadata('design:paramtypes', [reservation_api_service_1.ReservationApiService])
    ], ReservationComponent);
    return ReservationComponent;
}());
exports.ReservationComponent = ReservationComponent;
//# sourceMappingURL=reservations-list.component.js.map