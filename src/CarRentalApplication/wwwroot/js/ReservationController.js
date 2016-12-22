
"use strict";

//Creates a Controller and registers it to the main application module
// angular.module("app") //Getting the Module
app.controller("ReservationController", ['userid', '$http', function (userid, $http) {

    var viewModel = this;
    viewModel.showDetails = false;
    viewModel.Reservations = [];


    viewModel.userId = userid;
    console.log(viewModel.userId);

    angular.forEach(viewModel.Reservations, function (currReservation) {
        currReservation.showfull = false;
    });

    var data = {
        user_id: viewModel.userId
    };

    var config = {
        params: data
    };
    $http.get("/api/Reservations/", config)
.then(function (response) {
    angular.copy(response.data, viewModel.Reservations);
}), function (error) {
    console.log("errororoe");
};



    //viewModel.toggleDetails = function () {
    //    
    //};

    viewModel.toggleDetails = function (reservation) {

        angular.forEach(viewModel.Reservations, function (currReservation) {
            currReservation.showfull = currReservation === reservation && !currReservation.showfull;
        });
    };
}]);

//This is the body of the ReservationControllerFunction
