'use strict';
app.controller('campPlaceHomeController', ['campPlaceService', '$scope', '$rootScope', '$location', '$routeParams', 'localStorageService', function (campPlaceService, $scope, $rootScope, $location, $routeParams, localStorageService)
{
    $rootScope.title = 'Место отдыха';

    var authData = localStorageService.get('authorizationData');
    var campPlaceId = $routeParams['campPlaceId'];

    $scope.campPlace = {};

    campPlaceService.getCampPlaceData(campPlaceId, function(data)
    {
        $scope.campPlace = data;
    });

    campPlaceService.getCampPlacePosts(campPlaceId, function(data)
    {
        $scope.$broadcast('dataReceived', data);
    });

}]);