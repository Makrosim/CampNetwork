'use strict';
app.controller('homeController', ['$http', '$scope', '$location', '$timeout', 'localStorageService', function ($http, $scope, $location, $timeout, localStorageService) {

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');

    $scope.savedSuccessfully = false;
    $scope.message = "";
    $scope.profile = {};

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