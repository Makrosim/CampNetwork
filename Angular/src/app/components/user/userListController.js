'use strict';
app.controller('userListController', ['$http', '$scope', '$location', 'config', function ($http, $scope, $location, config)
{
    $scope.profiles = null;
    $scope.defaultAvatarUri = config.serviceBase + 'content/images/empty.png';
    
    $scope.$on('dataReceived', function (event, data)
    {
  		$scope.profiles = data;
	});

}]);