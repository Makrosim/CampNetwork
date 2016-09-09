'use strict';
app.controller('profileController', ['$http', '$scope', '$rootScope', '$location', '$timeout', 'localStorageService', function ($http, $scope, $rootScope, $location, $timeout, localStorageService) {

    $rootScope.title = 'Профайл пользователя';

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');
    $scope.profile = {};

    $scope.myImage='';
    $scope.myCroppedImage='';

    $http.get(serviceBase + 'api/Profiles').then
    (
        function (response)
        {
            $scope.profile = response.data;
        },
        function (err)
        {
            console.log(err.statusText);
        }
    );

    $scope.submit = function () 
    {
        $http.post(serviceBase + 'api/Profiles/' + authData.userName, $scope.profile).then
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
            function dataURItoBlob(dataURI) {
                var binary = atob(dataURI.split(',')[1]);
                var array = [];
                for(var i = 0; i < binary.length; i++) {
                    array.push(binary.charCodeAt(i));
                }
                return new Blob([new Uint8Array(array)], {type: 'image/jpeg'});
            }

            var fd = new FormData()
            fd.append('avatar', image);

            $http.post(serviceBase + 'api/Profiles/' + authData.userName + '/Medias', fd, { transformRequest:angular.identity, headers: { 'Content-Type': undefined } }).then
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

    var handleFileSelect=function(evt)
    {
        var file=evt.currentTarget.files[0];
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
        
    angular.element(document.querySelector('#fileInput')).on('change',handleFileSelect);

}]);