'use strict';
app.controller('openGroupController', ['$http', '$scope', '$location', '$routeParams', 'localStorageService', function ($http, $scope, $location, $routeParams, localStorageService)
{
    var serviceBase = 'http://localhost:56332/';
    $scope.group = {};
    $scope.groupId = $routeParams['groupId'];
    $scope.postId = -1;

    var authData = localStorageService.get('authorizationData');

    $scope.addPost = function ()
    {
        $http.get(serviceBase + 'api/Group/AddPost/?groupId=' + $scope.group.id + '&postId=' + $scope.postId).then
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

    $http.get(serviceBase + 'api/Group/?userName=' + authData.userName + '&groupId=' + $scope.groupId).then
    (
        function (response) 
        {
            $scope.group = response.data;
        },
        function (err)
        {
            console.log(err.statusText);
        }
    );
}]);