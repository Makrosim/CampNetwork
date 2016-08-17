'use strict';
app.controller('groupController', ['$http', '$scope', '$location', 'localStorageService', function ($http, $scope, $location, localStorageService)
{
    var serviceBase = 'http://localhost:56332/';
    $scope.isGroupsExists = false;
    $scope.createdGroups = [];
    $scope.otherGroups = [];
    
    var authData = localStorageService.get('authorizationData');

    $http.get(serviceBase + 'api/Group/?userName=' + authData.userName).then
    (
        function (response) 
        {
            response.data.forEach(getCreatedGroups);

            if(response.data.length > 0)
                $scope.isGroupsExists = true;
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