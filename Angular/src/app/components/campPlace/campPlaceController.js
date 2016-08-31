'use strict';
app.controller('campPlaceController', ['$http', '$scope', '$rootScope', '$location', 'localStorageService', function ($http, $scope, $rootScope, $location, localStorageService) {

    $rootScope.title = 'Места отдыха';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');

    $scope.points = {};

    $http.get(serviceBase + 'api/CampPlace/GetList/?userName=' + authData.userName).then
    (
        function (response) 
        {
            $scope.$broadcast('dataReceived', response.data);
        },
        function (err) 
        {
            console.log(err);
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

    $scope.delete = function (id)
    {
        $http.delete(serviceBase + 'api/CampPlace/?Id=' + id).then
        (
            function (response) 
            {
                $scope.data = response.data;
            },
            function (err)
            {
                console.log(err.statusText);
            }
        );
    }

}]);