'use strict';
app.controller('searchController', ['$http', '$scope', '$location', '$routeParams', 'authService', 'config', function ($http, $scope, $location, $routeParams, authService, config)
{
	var queryString;
	var includeString;

	$scope.response = {};
	$scope.searchCriteria = $routeParams['searchCriteria'];

	$scope.initData = function()
	{
	    return includeString;
	}

	if($scope.searchCriteria === undefined)
	{

	}
	else
	{
		switch($routeParams['type'])
		{
			case '1':
				queryString = config.serviceBase + 'api/Search/' + $scope.searchCriteria + '/CampPlaces';
				includeString = 'app/components/campPlace/campPlaceList.html';
				break;

			case '2':
				queryString = config.serviceBase + 'api/Search/' + $scope.searchCriteria + '/Profiles';
				includeString = 'app/components/user/userList.html';
				break;
		};

	    $http.get(queryString).then
	    (
	    	function (response)
		    {
	            $scope.$broadcast('dataReceived', response.data);
	            $scope.response = response;
		    },
		    function (err)
		    {
			    console.log(err.data);
		    }
	    );
	}

}]);