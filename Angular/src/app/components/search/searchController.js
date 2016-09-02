'use strict';
app.controller('searchController', ['$http', '$scope', '$location', '$routeParams', 'authService', 'localStorageService', function ($http, $scope, $location, $routeParams, authService, localStorageService) {

    var serviceBase = localStorageService.get('serviceBase');
	$scope.authData = authService.authentication;

	var queryString;
	var includeString;

	$scope.initData = function()
	{
	    return includeString;
	}

	switch($routeParams['type'])
	{
		case 'Места отдыха':
			queryString = serviceBase + 'api/campPlace/?campPlaceName=' + $routeParams['searchCriteria'];
			includeString = 'app/components/campPlace/campPlaceList.html';
			break;

		case 'Пользователи':
			queryString = serviceBase + 'api/userProfile/?searchCriteria=' + $routeParams['searchCriteria'];
			includeString = 'app/components/user/userList.html';
			break;
	};

    $http.get(queryString).then
    (
    	function (response)
	    {
            $scope.$broadcast('dataReceived', response.data);
	    },
	    function (err)
	    {
		    console.log(err.data);
	    }
    );
 
}]);