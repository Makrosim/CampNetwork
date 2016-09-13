'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', function ($scope, $location, authService)
{
	$scope.data = {};
    $scope.data.authData = authService.authentication;
    $scope.data.availableOptions = 
	    [
		    {id: '1', name: 'Camp places'},
		    {id: '2', name: 'Users'},
	    ];   	

    $scope.data.selectedOption = {id: '1', name: 'Camp places'};

    $scope.$on('login', function (event, isAuth) 
	{
		$scope.data.isAuth = isAuth;
	})	

	if($scope.data.authData)
	{
    	$scope.data.isAuth = true;
	}
	else
	{
		$scope.data.isAuth = false;
		$location.path('/login');
	}

	$scope.search = function ()
	{	
		if($scope.data.searchCriteria === undefined)
		{
			$location.path('/search/' + $scope.data.selectedOption.id);
		}
		else
		{
			$location.path('/search/' + $scope.data.selectedOption.id + '/' + $scope.data.searchCriteria);		
		}
	}

    $scope.logOut = function ()
    {
        authService.logOut();
		$scope.data.isAuth = false;
        $location.path('/login');
    }
 
}]);