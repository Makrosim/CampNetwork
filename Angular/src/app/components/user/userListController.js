'use strict';
app.controller('userListController', ['$http', '$scope', '$location', 'localStorageService', function ($http, $scope, $location, localStorageService) {

    $scope.profiles = null;
    
    $scope.$on('dataReceived', function (event, data)
    {
  		$scope.profiles = data;
	});

}]);