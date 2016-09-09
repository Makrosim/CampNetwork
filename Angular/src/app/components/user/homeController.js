'use strict';
app.controller('homeController', ['$http', '$scope', '$rootScope', '$location', '$timeout', '$routeParams', 'localStorageService', function ($http, $scope, $rootScope, $location, $timeout, $routeParams, localStorageService) {

    $rootScope.title = 'Домашняя страница';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');

    $scope.response = null;
    $scope.defaultAvatarUri = serviceBase + 'content/images/empty.png';

    console.log($scope.defaultAvatarUri);

    var userName = $routeParams['userName'];

    if($routeParams['userName'] == undefined)
        userName = authData.userName;

    $http.get(serviceBase + 'api/Profiles/' + userName).then
    (
        function (response) 
        {
            $scope.response = response;
            $scope.profile = response.data;

            if(response.status != '204')
            {
                console.log($scope.profile.avatarBase64.length);
                getPosts();
            }
        },
        function (err)
        {
            console.log(err);
        }
    );

    function getPosts()
    {
        $http.get(serviceBase + 'api/Users/' + $routeParams['userName'] + '/Posts').then
        (
            function (response)
            {
                if(response.status != "204")                   
                    $scope.$broadcast('dataReceived', response.data.map(getMessages));
            },
            function (err)
            {
                console.log(err);
            }
        );
    }


    function getMessages(value, index, array)
    {
        value.messages = {};

        $http.get(serviceBase + 'api/Posts/' + value.id + '/Messages').then
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