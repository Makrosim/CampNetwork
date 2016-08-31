'use strict';
app.controller('createGroupController', ['$http', '$scope', '$location', 'localStorageService', function ($http, $scope, $location, localStorageService)
{
    var serviceBase = localStorageService.get('serviceBase');
    $scope.groupData = {};
    
    var authData = localStorageService.get('authorizationData');

    $scope.create = function ()
    {
        $http.post(serviceBase + 'api/Group/?userName=' + authData.userName, $scope.groupData).then
        (
            function (response)
            {
                $location.path('/group');
            },
            function (err)
            {
                console.log(err.statusText);
            }
        );
    };
}]);