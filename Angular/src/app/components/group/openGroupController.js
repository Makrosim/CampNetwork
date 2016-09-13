'use strict';
app.controller('openGroupController', ['$http', '$scope', '$location', '$routeParams', 'config', function ($http, $scope, $location, $routeParams, config)
{
    $scope.group = {};
    $scope.groupId = $routeParams['groupId'];
    $scope.postId = -1;

    $http.get(config.serviceBase + 'api/Groups/' + $scope.groupId).then
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
        $http.get(config.serviceBase + 'api/Group/AddPost/?groupId=' + $scope.group.id + '&postId=' + $scope.postId).then
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