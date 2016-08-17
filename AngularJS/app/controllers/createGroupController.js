'use strict';
app.controller('createGroupController', ['$http', '$scope', '$location', 'localStorageService', function ($http, $scope, $location, localStorageService)
{
    var serviceBase = 'http://localhost:56332/';
    $scope.groupData = {};
    
    var authData = localStorageService.get('authorizationData');

    $scope.create = function ()
    {
        $http.post(serviceBase + 'api/Group/?userName=' + authData.userName, $scope.groupData).then
        (
            function (response)
            {

            },
            function (err)
            {
                console.log(err.statusText);
            }
        );
    };
}]);