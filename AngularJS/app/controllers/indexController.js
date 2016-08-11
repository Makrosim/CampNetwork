'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
 
    $scope.isAuthorized = authService.authentication.isAuth;

    $scope.userName = authService.authentication.userName;

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/login');
    }
 
    $scope.authentication = authService.authentication;
 
}]);