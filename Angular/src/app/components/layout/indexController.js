'use strict';
app.controller('indexController', ['$scope', 'localStorageService', '$location', 'authService', function ($scope, localStorageService, $location, authService) {

	$scope.data = {};
    $scope.data.authData = localStorageService.get('authorizationData');
    $scope.data.availableOptions = 
	    [
		    {id: '1', name: 'Места отдыха'},
		    {id: '2', name: 'Пользователи'},
	    ];   	

    $scope.data.selectedOption = {id: '1', name: 'Места отдыха'};

    $scope.$on('login', function (event, isAuth) 
	{
		$scope.data.authData = localStorageService.get('authorizationData');
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
			$location.path('/search/' + $scope.data.selectedOption.name);
		}
		else
		{
			$location.path('/search/' + $scope.data.selectedOption.name + '/' + $scope.data.searchCriteria);		
		}
	}

    $scope.logOut = function ()
    {
        authService.logOut();
		$scope.data.isAuth = false;
        $location.path('/login');
    }
 
}]);