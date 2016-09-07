'use strict';
app.controller('profileController', ['$http', '$scope', '$rootScope', '$location', '$timeout', 'localStorageService', function ($http, $scope, $rootScope, $location, $timeout, localStorageService) {

    $rootScope.title = 'Профайл пользователя';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');
    $scope.profile = {};

    $http.get(serviceBase + 'api/Profiles').then
    (
        function (response)
        {
            $scope.profile = response.data;
        },
        function (err)
        {
            console.log(err.statusText);
        }
    );

    $scope.submit = function () 
    {
        $http.post(serviceBase + 'api/Profiles/' + authData.userName, $scope.profile).then
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

        if($scope.file !== undefined)
        {
            var fd = new FormData()
            fd.append('avatar', $scope.file);

            $http.post(serviceBase + 'api/Profiles/' + authData.userName + '/Medias', fd, { transformRequest:angular.identity, headers: { 'Content-Type': undefined } }).then
            (
                function (response) 
                {

                },
                function (err) 
                {
                    console.log(err);
                }
            );
        }
    }

}]);