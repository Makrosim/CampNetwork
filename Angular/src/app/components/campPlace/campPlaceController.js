'use strict';
app.controller('campPlaceController', ['$http', '$scope', '$rootScope', '$location', 'localStorageService', function ($http, $scope, $rootScope, $location, localStorageService) {

    $rootScope.title = 'Места отдыха';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');

    $scope.response = null;

    $http.get(serviceBase + 'api/Users/' + authData.userName + '/CampPlaces').then
    (
        function (response) 
        {
            $scope.response = response;

            if(response.status != "204")     
                $scope.$broadcast('dataReceived', response.data);
        },
        function (err) 
        {
            console.log(err);
        }
    );

}]);