'use strict';
app.controller('groupController', ['$http', '$scope', '$rootScope', '$location', 'localStorageService', function ($http, $scope, $rootScope, $location, localStorageService)
{
    $rootScope.title = 'Группы';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');
        
    $scope.isGroupsExists = null;
    $scope.createdGroups = [];
    $scope.otherGroups = [];

    $http.get(serviceBase + 'api/Users/' + authData.userName + '/Groups').then
    (
        function (response) 
        {
            if(response.status != "204")
                response.data.forEach(getCreatedGroups);

            if(response.data.length > 0)
                $scope.isGroupsExists = true;
            else
                $scope.isGroupsExists = false;
        },
        function (err)
        {
            console.log(err);
        }
    );

    function getCreatedGroups(value, index, array)
    {
        if(authData.userName == value.creator)
        {
            $scope.createdGroups.push(value);
        }
        else
        {
            $scope.otherGroups.push(value);
        }
    }

    $scope.delete = function (id, index, isCreator)
    {
        $http.delete(serviceBase + 'api/Groups/' + id).then
        (
            function (response) 
            {
                if(isCreator)
                {
                    $scope.createdGroups.splice(index, 1);
                }
                else
                {
                    $scope.otherGroups.splice(index, 1);
                }
                
            },
            function (err)
            {
                console.log(err);
            }
        );
    }

}]);