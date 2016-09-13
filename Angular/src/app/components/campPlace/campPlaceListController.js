'use strict';
app.controller('campPlaceListController', ['campPlaceService', '$scope', 'authService', function (campPlaceService, $scope, authService)
{
    $scope.campList = null;
	$scope.currentUser = authService.authentication.userName;

    $scope.$on('dataReceived', function (event, data)
    {
  		$scope.campList = data;
	});

    $scope.delete = function (id, index)
    {
	    campPlaceService.deleteCampPlace(id, function(data)
	    {
	        $scope.campList.splice(index, 1);
	    });
    }

}]);