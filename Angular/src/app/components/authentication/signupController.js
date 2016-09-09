'use strict';
app.controller('signupController', ['$scope', '$rootScope', '$location', '$timeout', 'authService', function ($scope, $rootScope, $location, $timeout, authService) {

    $rootScope.title = 'Регистрация';

    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registration = {};

    $scope.signUp = function (isValid)
    {
        if(isValid)
        {
            authService.saveRegistration($scope.registration).then
            (
                function (response)
                {
                    $scope.savedSuccessfully = true;
                    $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
                    startTimer();

                },
                function (response)
                {
                    $scope.message = response.data;
                }
            );
        }
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }

}]);