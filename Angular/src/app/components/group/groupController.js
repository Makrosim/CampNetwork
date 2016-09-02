'use strict';
app.controller('groupController', ['$http', '$scope', '$rootScope', '$location', 'localStorageService', function ($http, $scope, $rootScope, $location, localStorageService)
{
    $rootScope.title = 'Группы';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');
        
    $scope.isGroupsExists = null;
    $scope.createdGroups = [];
    $scope.otherGroups = [];

    $http.get(serviceBase + 'api/Group/?userName=' + authData.userName).then
    (
        function (response) 
        {
            response.data.forEach(getCreatedGroups);

            if(response.data.length > 0)
                $scope.isGroupsExists = true;
            else
                $scope.isGroupsExists = false;
        },
        function (err)
        {
            console.log(err.statusText);
        }
    );

    function getCreatedGroups(value, index, array)
    {
        if(value.isCreator === true)
        {
            $scope.createdGroups.push(value);
        }
        else
        {
            $scope.otherGrups.push(value);
        }
    }
}]);