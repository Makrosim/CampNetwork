'use strict';
app.controller('postController', ['$http', '$scope', '$rootScope', '$location', '$routeParams', 'localStorageService', function ($http, $scope, $rootScope, $location, $routeParams, localStorageService)
{
    var authData = localStorageService.get('authorizationData');
    $rootScope.title = 'Добавление обзора';

    var serviceBase = localStorageService.get('serviceBase');
    $scope.post = {};

    $scope.submit = function ()
    {
        $scope.post.campPlaceId = $routeParams['id'];

        $http.post(serviceBase + 'api/Posts/', $scope.post).then
        (
            function (response)
            {
                $location.path('/home/' + authData.userName);
            },
            function (err)
            {
                console.log(err);
            }
        );
    }

}]);