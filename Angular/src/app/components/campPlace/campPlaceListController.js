'use strict';
app.controller('campPlaceListController', ['campPlaceService', '$scope', 'localStorageService', function (campPlaceService, $scope, localStorageService) {

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');

    $scope.campList = null;
	$scope.currentUser = authData.userName;

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