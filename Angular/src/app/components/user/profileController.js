'use strict';
app.controller('profileController', ['$http', '$scope', '$rootScope', '$location', '$timeout', 'localStorageService', function ($http, $scope, $rootScope, $location, $timeout, localStorageService) {

    $rootScope.title = 'Профайл пользователя';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');
    $scope.profile = {};

    $http.get(serviceBase + 'api/UserProfile/?userName=' + authData.userName + '&ownerName=' + authData.userName).then
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
        if($scope.file === undefined)
        {
            postProfile();
        }
        else
        {
            var fd = new FormData()

            fd.append('avatar', $scope.file);

            $http.post(serviceBase + 'api/UserProfile', fd, { transformRequest:angular.identity, headers: { 'Content-Type': undefined } }).then
            (
                function (response) 
                {
                    $scope.profile.avatarId = response.data;
                    postProfile();
                },
                function (err) 
                {
                    console.log(err);
                }
            );
        }
    }


    function postProfile()
    {
        $http.post(serviceBase + 'api/UserProfile/?userName=' + authData.userName, $scope.profile).then
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
    };
}]);

app.directive('fileModel', ['$parse', function ($parse) {
    return {
        restrict: 'A',
        link: function(scope, element, attrs) {
            var model = $parse(attrs.fileModel);
            var modelSetter = model.assign;
            
            element.bind('change', function(){
                scope.$apply(function(){
                    modelSetter(scope, element[0].files[0]);
                });
            });
        }
    };
}]);