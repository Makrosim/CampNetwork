'use strict';
app.controller('loginController', ['$scope', '$rootScope', '$location', 'authService', function ($scope, $rootScope, $location, authService) {

    $rootScope.title = 'Логин';

    $scope.loginData = {
        userName: "",
        password: ""
    };

    $scope.message = "";

    $scope.login = function ()
    {
        authService.login($scope.loginData).then
        (
            function (response)
            {
                $scope.$emit('login', authService.authentication.isAuth);
                $location.path('/home');
            },
            function (err) 
            {
                $scope.message = err.error_description;
            }
        );
    };

}]);