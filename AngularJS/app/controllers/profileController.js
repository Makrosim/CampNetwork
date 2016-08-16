'use strict';
app.controller('profileController', ['$http', '$scope', '$location', '$timeout', 'localStorageService', function ($http, $scope, $location, $timeout, localStorageService) {

    var serviceBase = 'http://localhost:56332/';

    var authData = localStorageService.get('authorizationData');

    $http.get(serviceBase + 'api/UserProfile/?userName=' + authData.userName).then
    (
        function (response)
        {
            $scope.profile = response.data;
            console.log($scope.profile);
        },
        function (err)
        {
            console.log(err.statusText);
        }
    );

    $scope.submit = function () 
    {
        var fd = new FormData()

        fd.append('avatar', $scope.file);

        $http.post(serviceBase + 'api/UserProfile/PostImage', fd, { transformRequest:angular.identity, headers: { 'Content-Type': undefined } }).then
        (
            function (response) 
            {

            },
            function (err) 
            {
                 console.log(err.statusText);
            }
        );

        $http.post(serviceBase + 'api/UserProfile/PostProfile/?userName=' + authData.userName, $scope.profile).then
        (
            function (response)
            {

            },
            function (err)
            {
                console.log(err.statusText);
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