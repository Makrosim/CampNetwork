'use strict';
app.controller('profileController', ['$http', '$scope', '$location', 'authService', 'config', function ($http, $scope, $location, authService, config)
{
    var authData = authService.authentication;
    $scope.profile = {};

    $scope.myImage='';
    $scope.myCroppedImage='';

    $scope.data.availableDays = [];
    $scope.data.availableYears = [];
    $scope.data.availableMonths = 
        [
            {id: 0, name: 'January'}, {id: 1, name: 'February'},
            {id: 2, name: 'March'}, {id: 3, name: 'April'}, {id: 4, name: 'May'},
            {id: 5, name: 'June'}, {id: 6, name: 'July'}, {id: 7, name: 'August'},
            {id: 8, name: 'September'}, {id: 9, name: 'October'}, {id: 10, name: 'November'},
            {id: 11, name: 'December'}
        ];

    (function()
    {
        for(var i = 0; i < 31; i++)
        {
            $scope.data.availableDays.push({id: i, name: i + 1});
        }

        for(var i = 0; i < 100; i++)
        {
            $scope.data.availableYears.push({id: i, name: 1916 + i});
        }
    }());

    $http.get(config.serviceBase + 'api/Profiles').then
    (
        function (response)
        {
            $scope.profile = response.data;
            $scope.data.selectedDay = {id: response.data.birthDateDay - 1, name: response.data.birthDateDay};
            $scope.data.selectedYear = {id: response.data.birthDateYear - 1916, name: response.data.birthDateYear};
            $scope.data.selectedMonth = $scope.data.availableMonths.filter(function(value, index, array)
                {
                    if(response.data.birthDateMounth == value.name)
                    {
                        return true;
                    }
                })[0];
        },
        function (err)
        {
            console.log(err.statusText);
        }
    );

    $scope.submit = function () 
    {
        $scope.profile.birthDateDay = $scope.data.selectedDay.name;
        $scope.profile.birthDateMounth = $scope.data.selectedMonth.name;
        $scope.profile.birthDateYear = $scope.data.selectedYear.name;

        $http.post(config.serviceBase + 'api/Profiles/' + authData.userName, $scope.profile).then
        (
            function (response)
            {
                $location.path('/home/' + authData.userName);
            },
            function (err)
            {
                console.log(err);
            }
        );

        if($scope.myCroppedImage !== undefined)
        {
            var image = dataURItoBlob($scope.myCroppedImage);

            function dataURItoBlob(dataURI)
            {
                var binary = atob(dataURI.split(',')[1]);
                var array = [];

                for(var i = 0; i < binary.length; i++)
                {
                    array.push(binary.charCodeAt(i));
                }

                return new Blob([new Uint8Array(array)], {type: 'image/jpeg'});
            }

            var fd = new FormData()
            fd.append('avatar', image);

            $http.post(config.serviceBase + 'api/Profiles/' + authData.userName + '/Medias', fd, { transformRequest:angular.identity, headers: { 'Content-Type': undefined } }).then
            (
                function (response) 
                {

                },
                function (err) 
                {
                    console.log(err);
                }
            );
        }
    }

    var handleFileSelect = function(evt)
    {
        var file = evt.currentTarget.files[0];
        var reader = new FileReader();
        reader.onload = function (evt)
        {
            $scope.$apply
            (
                function($scope)
                {
                    $scope.myImage=evt.target.result;
                }
            );

        };

        reader.readAsDataURL(file);
    };
        
    angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect);

}]);