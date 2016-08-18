'use strict';
app.controller('homeController', ['$http', '$scope', '$location', '$timeout', 'localStorageService', function ($http, $scope, $location, $timeout, localStorageService) {

    var serviceBase = 'http://localhost:56332/';
    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.profile = {};

    var authData = localStorageService.get('authorizationData');

    $http.get(serviceBase + 'api/UserProfile/?userName=' + authData.userName).then
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
                $scope.avatar = response.data;
            },
            function (err)
            {
               console.log(err.statusText);
            }
        );    
    }


}]);