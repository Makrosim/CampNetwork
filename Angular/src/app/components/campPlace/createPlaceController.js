'use strict';
app.controller('createPlaceController', ['campPlaceService', '$http', '$scope', '$routeParams', '$location', function (campPlaceService, $http, $scope, $routeParams, $location)
{
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