'use strict';
app.controller('createPlaceController', ['$http', '$scope', '$routeParams', '$rootScope', '$location', 'localStorageService', function ($http, $scope, $routeParams, $rootScope, $location, localStorageService)
{
    $rootScope.title = 'Создать место отдыха';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');

    $scope.campPlaceId = $routeParams['id'];
    $scope.campPlace = {};

    if($scope.campPlaceId != undefined)
    {
        $http.get(serviceBase + 'api/CampPlaces/' + $scope.campPlaceId).then
        (
            function (response)
            {
                $scope.campPlace = response.data;
            },
            function (err)
            {
                console.log(err);
            }
        );
    }

    $scope.submit = function ()
    {
        if($scope.campPlaceId == undefined)
        {
            $http.post(serviceBase + 'api/CampPlaces/', $scope.campPlace).then
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
        else
        {
            $http.put(serviceBase + 'api/CampPlaces/', $scope.campPlace).then
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
    }

}]);