'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

    $scope.loginData = {
        userName: "",
        password: ""
    };
    $scope.isAuthorized = authService.authentication.isAuth;

    $scope.message = "";

    $scope.login = function () {

        authService.login($scope.loginData).then(function (response) {
            $location.path('/home');
            $scope.isAuthorized = authService.authentication.isAuth;
        },
        function (err) {
             $scope.message = err.error_description;
        });
    };

}]);