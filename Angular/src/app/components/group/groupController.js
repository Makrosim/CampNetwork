'use strict';
app.controller('groupController', ['$http', '$scope', '$location', 'authService', 'config', function ($http, $scope, $location, authService, config)
{
    var authData = authService.authentication;
        
    $scope.isGroupsExists = null;
    $scope.createdGroups = [];
    $scope.otherGroups = [];

    $http.get(config.serviceBase + 'api/Users/' + authData.userName + '/Groups').then
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
        $http.delete(config.serviceBase + 'api/Groups/' + id).then
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