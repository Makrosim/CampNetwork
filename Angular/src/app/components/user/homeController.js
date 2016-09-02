'use strict';
app.controller('homeController', ['$http', '$scope', '$rootScope', '$location', '$timeout', '$routeParams', 'localStorageService', function ($http, $scope, $rootScope, $location, $timeout, $routeParams, localStorageService) {

    $rootScope.title = 'Домашняя страница';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');

    $scope.profile = null;

    var param = $routeParams['userName'];

    if(param === undefined)
    {
        param = authData.userName;
    }

    $http.get(serviceBase + 'api/UserProfile/?userName=' + authData.userName + '&ownerName=' + param).then
    (
        function (response) 
        {
            $scope.profile = response.data;
            getMedia(); 
        },
        function (err)
        {
            console.log(err.statusText);
        }
    );

    function getMedia()
    {
        $http.get(serviceBase + 'api/UserProfile/?mediaId=' + $scope.profile.avatarId).then
        (
            function (response) 
            {
                if(response.data.length > 0)
                {
                    $scope.avatar = 'data:image/png;base64,' + response.data;
                }
                else
                {
                    $scope.avatar = serviceBase + 'content/images/empty.png'
                }
            },
            function (err)
            {
               console.log(err.statusText);
            }
        );    
    }

    $http.get(serviceBase + 'api/Post/?userName=' + authData.userName).then
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