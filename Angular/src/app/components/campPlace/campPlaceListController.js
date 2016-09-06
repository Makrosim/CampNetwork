'use strict';
app.controller('campPlaceListController', ['$http', '$scope', '$location', 'localStorageService', function ($http, $scope, $location, localStorageService) {

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');

    $scope.points = $scope.$parent.points;
    $scope.campList = null;
	$scope.currentUser = authData.userName;

    $scope.$on('dataReceived', function (event, data)
    {
  		$scope.campList = data;
	});

    $scope.delete = function (id, index)
    {
        $http.delete(serviceBase + 'api/CampPlaces/' + id).then
        (
            function (response) 
            {
                $scope.campList.splice(index, 1);
            },
            function (err)
            {
                console.log(err.statusText);
            }
        );
    }

}]);