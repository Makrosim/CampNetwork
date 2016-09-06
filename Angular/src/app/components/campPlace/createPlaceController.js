'use strict';
app.controller('createPlaceController', ['$http', '$scope', '$rootScope', '$location', 'localStorageService', function ($http, $scope, $rootScope, $location, localStorageService)
{
    $rootScope.title = 'Создать место отдыха';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');

    $scope.campPlace = {};

    $scope.submit = function ()
    {
        $http.post(serviceBase + 'api/CampPlaces', $scope.campPlace).then
        (
            function (response)
            {
                $location.path('/camp');
            },
            function (err)
            {
                console.log(err.statusText);
            }
        );
    }
}]);