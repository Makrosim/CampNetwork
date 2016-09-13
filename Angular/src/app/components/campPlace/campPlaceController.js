'use strict';
app.controller('campPlaceController', ['campPlaceService', '$scope', 'authService', function (campPlaceService, $scope, authService)
{
    $scope.response = null;

    campPlaceService.getUsersCampPlaces(authService.authentication.userName, function(data)
    { 
        $scope.response = data;

        if($scope.response != '204')
            $scope.$broadcast('dataReceived', data);  
    });
                                   
}]);