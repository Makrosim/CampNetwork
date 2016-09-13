'use strict';
app.controller('createGroupController', ['$http', '$scope', '$location', 'config', function ($http, $scope, $location, config)
{
    $scope.groupData = {};

    $scope.create = function ()
    {
        $http.post(config.serviceBase + 'api/Groups/', $scope.groupData).then
        (
            function (response)
            {
                $location.path('/group');
            },
            function (err)
            {
                console.log(err);
            }
        );
    };
}]);