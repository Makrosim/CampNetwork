'use strict';
app.controller('postController', ['$http', '$scope', '$location', '$routeParams', 'authService', 'config', function ($http, $scope, $location, $routeParams, authService, config)
{    
    $scope.post = {};

    $scope.submit = function (isValid)
    {
        if(isValid)
        {
            $scope.post.campPlaceId = $routeParams['id'];

            $http.post(config.serviceBase + 'api/Posts/', $scope.post).then
            (
                function (response)
                {
                    $location.path('/home/' + authService.authentication.userName);
                },
                function (err)
                {
                    console.log(err);
                }
            );
        }
    }

}]);