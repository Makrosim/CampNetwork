'use strict';
app.controller('campController', ['$http', '$scope', '$location', '$timeout', 'localStorageService', function ($http, $scope, $location, $timeout, localStorageService) {

    var serviceBase = localStorageService.get('serviceBase');
    $scope.message = "";
    $scope.points = {};
    $scope.campList = null;

    var authData = localStorageService.get('authorizationData');

    $scope.delete = function (id)
    {
        $http.delete(serviceBase + 'api/CampPlace/?Id=' + id).then
        (
            function (response) 
            {
                $scope.campList = response.data;
            },
            function (err)
            {
                console.log(err.statusText);
            }
        );
    }

    $http.get(serviceBase + 'api/CampPlace/GetList/?userName=' + authData.userName).then
    (
        function (response) 
        {
            $scope.campList = response.data;
        },
        function (err) 
        {
            console.log(err.statusText);
        }
    );

    $http.get(serviceBase + 'api/CampPlace/GetPoints/?userName=' + authData.userName).then
    (
        function (response)
        {
            $scope.points = response.data;
        },
        function (err)
        {
            console.log(err.statusText);
        }
     );

}]);