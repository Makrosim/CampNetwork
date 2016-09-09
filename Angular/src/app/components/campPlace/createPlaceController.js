'use strict';
app.controller('createPlaceController', ['campPlaceService', '$http', '$scope', '$routeParams', '$rootScope', '$location', 'localStorageService', function (campPlaceService, $http, $scope, $routeParams, $rootScope, $location, localStorageService)
{
    $rootScope.title = 'Создать место отдыха';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');
    var campPlaceId = $routeParams['id'];

    $scope.campPlace = {};

    if(campPlaceId != undefined)
    {
        campPlaceService.getCampPlaceData(campPlaceId, function(data)
        {
            $scope.campPlace = data;
        });
    }

    $scope.submit = function (isValid)
    {
        console.log(isValid);
        if(isValid)
        {
            if(campPlaceId == undefined)
            {
                campPlaceService.createCampPlace($scope.campPlace, function(data)
                {
                    $location.path('/camp');
                });
            }
            else
            {
                campPlaceService.updateCampPlace($scope.campPlace, function(data)
                {
                    $location.path('/camp');
                });
            }
        }
    }

}]);