'use strict';
app.controller('homeController', ['$http', '$scope', '$location', '$timeout', 'localStorageService', function ($http, $scope, $location, $timeout, localStorageService) {

    var serviceBase = 'http://localhost:56332/';
    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.profileData = {
        name: "",
        address: "",
        skype: "",
        phone: "",
        bitrhDate: "",
        additionalInformation: ""
    };

    var authData = localStorageService.get('authorizationData');
    $http.get(serviceBase + 'api/UserProfile/?userName=' + authData.userName).then(function (response) {

             console.log(response.data);
             $scope.profileData.name = response.data.firstName + ' ' + response.data.lastName;
             $scope.profileData.address = response.data.address;
             $scope.profileData.skype = response.data.skype;
             console.log($scope.profileData);
        },
         function (err) {
             console.log(err.statusText);
         });;
}]);