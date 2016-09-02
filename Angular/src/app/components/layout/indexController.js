'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {

	$scope.authData = authService.authentication;
	$scope.search = {};
    $scope.data = {
	    availableOptions:
	    [
		    {id: '1', name: 'Места отдыха'},
		    {id: '2', name: 'Пользователи'},
		    {id: '3', name: 'Группы'}
	    ],
    	selectedOption:
    		{id: '1', name: 'Места отдыха'} //This sets the default value of the select in the ui
    };

	if($scope.authData.isAuth == false)
	{
       $location.path('/login');
	}

	$scope.search = function ()
	{
		$location.path('/search/' + $scope.data.selectedOption.name + '/' + $scope.search.searchCriteria);
	}

    $scope.$on('login', function (event, authentication) 
	{
		$scope.authData = authentication;
	})

    $scope.logOut = function ()
    {
        authService.logOut();
        
        $scope.authData = authService.authentication;

        $location.path('/login');
    }
 
}]);