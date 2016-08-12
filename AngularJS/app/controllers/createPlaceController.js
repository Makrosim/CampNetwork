'use strict';
app.controller('createPlaceController', ['$http', '$scope', '$location', '$timeout', 'localStorageService', function ($http, $scope, $location, $timeout, localStorageService) {

    var serviceBase = 'http://localhost:56332/';
    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.campPlace = {};

    var authData = localStorageService.get('authorizationData');

    $scope.submit = function () {

        $http.post(serviceBase + 'api/CampPlace/Save/?userName=' + authData.userName, $scope.campPlace).then(function (response) {

                $scope.campList = response.data;
                console.log(response.data);
            },
             function (err) {
                 console.log(err.statusText);
         });

    }


}]);