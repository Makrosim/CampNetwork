'use strict';
app.controller('loginController', ['$scope', '$location', 'authService', function ($scope, $location, authService)
{
    $scope.loginData = {};
    $scope.message = "";

    (function ()
    {
        authService.logOut();
        $scope.$emit('login', false);
    })();

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