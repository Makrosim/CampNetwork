'use strict';
app.controller('campPlaceController', ['campPlaceService', '$scope', '$rootScope', 'localStorageService', function (campPlaceService, $scope, $rootScope, localStorageService) {

    $rootScope.title = 'Места отдыха';

    var authData = localStorageService.get('authorizationData');

    $scope.response = null;

    campPlaceService.getUsersCampPlaces(authData.userName, function(data)
    { 
        $scope.response = data;

        if($scope.response != '204')
            $scope.$broadcast('dataReceived', data);  
    });
                                   
}]);