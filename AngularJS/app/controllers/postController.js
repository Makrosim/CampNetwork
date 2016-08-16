'use strict';
app.controller('postController', ['$http', '$scope', '$location', '$routeParams', 'localStorageService', function ($http, $scope, $routeParams, $location, $timeout, localStorageService)
{
    var serviceBase = 'http://localhost:56332/';
    $scope.data = {};
    $scope.data.postText = "";

    $scope.submit = function ()
    {
        $http.post(serviceBase + 'api/Post/?campPlaceId=' + $routeParams.$$search.id, '"' + $scope.data.postText + '"').then
        (
            function (response)
            {
                $location.path('#/home');
            },
            function (err)
            {
                console.log(err.statusText);
            }
        );
    }

}]);