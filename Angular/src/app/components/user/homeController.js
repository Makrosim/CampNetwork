'use strict';
app.controller('homeController', ['$http', '$scope', '$rootScope', '$location', '$timeout', '$routeParams', 'localStorageService', function ($http, $scope, $rootScope, $location, $timeout, $routeParams, localStorageService) {

    $rootScope.title = 'Домашняя страница';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');
    $scope.errorMessage = null;

    $scope.profile = null;
    $scope.isProfileExists = null;

    var param = $routeParams['userName'];

    if(param === undefined)
    {
        param = authData.userName;
    }

    $http.get(serviceBase + 'api/Profiles/?ownerName=' + param).then
    (
        function (response) 
        {
            $scope.profile = response.data;

            if($scope.profile.avatarBase64.length === 0)
                $scope.profile.avatarBase64 = serviceBase + 'content/images/empty.png';

            $scope.isProfileExists = true;
            getPosts();
        },
        function (err)
        {
            console.log(err);
            $scope.errorMessage = err.data.Message;
            $scope.isProfileExists = false;
        }
    );

    function getPosts()
    {
        $http.get(serviceBase + 'api/Users/' + authData.userName + '/Posts').then
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