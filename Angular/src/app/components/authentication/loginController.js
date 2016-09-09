'use strict';
app.controller('loginController', ['$scope', '$rootScope', '$location', 'authService', function ($scope, $rootScope, $location, authService) {

    $rootScope.title = 'Логин';

    $scope.loginData = {};
    $scope.message = "";

    $scope.login = function (isValid)
    {
        if(isValid)
        {
            authService.login($scope.loginData).then
            (
                function (response)
                {
                    $scope.$emit('login', authService.authentication.isAuth);
                    $location.path('/home/' + $scope.loginData.userName);
                },
                function (err) 
                {
                    $scope.message = err.data.error_description;
                }
            );   
        }
    };

}]);