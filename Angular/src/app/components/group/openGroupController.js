'use strict';
app.controller('openGroupController', ['$http', '$scope', '$rootScope', '$location', '$routeParams', 'localStorageService', function ($http, $scope, $rootScope, $location, $routeParams, localStorageService)
{
    $rootScope.title = 'Группа';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');

    $scope.group = {};
    $scope.groupId = $routeParams['groupId'];
    $scope.postId = -1;

    $http.get(serviceBase + 'api/Groups/' + $scope.groupId).then
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

}]);