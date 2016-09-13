'use strict';
app.controller('homeController', ['$http', '$scope', '$location', '$routeParams', 'authService', 'config', function ($http, $scope, $location, $routeParams, authService, config)
{
    $scope.response = null;
    $scope.defaultAvatarUri = config.serviceBase + 'content/images/empty.png';

    var userName = $routeParams['userName'];

    if($routeParams['userName'] == undefined)
        userName = authService.authentication.userName;

    $http.get(config.serviceBase + 'api/Profiles/' + userName).then
    (
        function (response) 
        {
            $scope.response = response;
            $scope.profile = response.data;

            if(response.status != '204')
            {
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
        $http.get(config.serviceBase + 'api/Users/' + $routeParams['userName'] + '/Posts').then
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

        $http.get(config.serviceBase + 'api/Posts/' + value.id + '/Messages').then
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