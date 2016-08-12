'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

	$scope.authData = authService.authentication
	
	if($scope.authData.isAuth == false)
	       $location.path('/login');

    $scope.$on('login', function (event, authentication) 
	{
		$scope.authData = authentication;
	})

    $scope.logOut = function () {
        authService.logOut();
        $scope.authData = authService.authentication;

        $location.path('/login');
    }
 
}]);