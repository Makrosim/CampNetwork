'use strict';
app.controller('createPlaceController', ['$http', '$scope', '$location', '$timeout', 'localStorageService', function ($http, $scope, $location, $timeout, localStorageService)
{
    var serviceBase = 'http://localhost:56332/';
    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.campPlace = {};

    var authData = localStorageService.get('authorizationData');

    $scope.submit = function ()
    {
        $http.post(serviceBase + 'api/campPlace/?userName=' + authData.userName, $scope.campPlace).then
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