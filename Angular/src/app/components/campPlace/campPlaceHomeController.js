'use strict';
app.controller('campPlaceHomeController', ['campPlaceService', '$scope', '$rootScope', '$location', '$routeParams', 'localStorageService', function (campPlaceService, $scope, $rootScope, $location, $routeParams, localStorageService)
{
    $rootScope.title = 'Место отдыха';

    var authData = localStorageService.get('authorizationData');
    var campPlaceId = $routeParams['campPlaceId'];

    $scope.response = null;

    campPlaceService.getCampPlaceData(campPlaceId, function(data)
    {
        $scope.response = data;
    });

    campPlaceService.getCampPlacePosts(campPlaceId, function(data)
    {
        if(data != '204')
            $scope.$broadcast('dataReceived', data);
    });

}]);