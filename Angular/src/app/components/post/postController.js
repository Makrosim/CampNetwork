'use strict';
app.controller('postController', ['$http', '$scope', '$rootScope', '$location', '$routeParams', 'localStorageService', function ($http, $scope, $rootScope, $location, $routeParams, localStorageService)
{
    $rootScope.title = 'Добавление обзора';

    var serviceBase = localStorageService.get('serviceBase');
    $scope.data = {};
    $scope.data.postText = "";

    $scope.submit = function ()
    {
        $http.post(serviceBase + 'api/Post/?campPlaceId=' + $routeParams['id'], '"' + $scope.data.postText + '"').then
        (
            function (response)
            {
                $location.path('/home');
            },
            function (err)
            {
                console.log(err);
            }
        );
    }

}]);