'use strict';
app.controller('campPlaceHomeController', ['campPlaceService', '$scope', '$routeParams', function (campPlaceService, $scope, $routeParams)
{
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