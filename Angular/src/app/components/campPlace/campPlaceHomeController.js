'use strict';
app.controller('campPlaceHomeController', ['$http', '$scope', '$rootScope', '$location', '$routeParams', 'localStorageService', function ($http, $scope, $rootScope, $location, $routeParams, localStorageService)
{
    $rootScope.title = 'Место отдыха';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');

    $scope.campPlaceId = $routeParams['campPlaceId'];
    $scope.campPlace = {};

    $http.get(serviceBase + 'api/CampPlace/?campPlaceId=' + $scope.campPlaceId).then
    (
        function (response) 
        {
            $scope.campPlace = response.data;
        },
        function (err)
        {
            console.log(err);
        }
    );

    $http.get(serviceBase + 'api/Post/GetCampPlacePosts/?campPlaceId=' + $scope.campPlaceId).then
    (
        function (response)
        {
            $scope.$broadcast('dataReceived', response.data.map(getMessages));
        },
        function (err)
        {
            console.log(err);
        }
    );

    function getMessages(value, index, array)
    {
        value.messages = {};

        $http.get(serviceBase + 'api/Message/?postId=' + value.id).then
        (
            function (response)
            {
                var messages = response.data;
                value.messages = messages;
            },
            function (err)
            {
                console.log(err);
            }
        )

        return value;
    };

}]);