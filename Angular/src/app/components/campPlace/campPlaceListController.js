'use strict';
app.controller('campPlaceListController', ['$http', '$scope', '$location', 'localStorageService', function ($http, $scope, $location, localStorageService) {

    var authData = localStorageService.get('authorizationData');

    $scope.points = $scope.$parent.points;
    $scope.campList = null;
	$scope.currentUser = authData.userName;

    $scope.$on('dataReceived', function (event, data)
    {
  		$scope.campList = data;
	});

}]);