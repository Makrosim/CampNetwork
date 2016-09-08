'use strict';
app.factory('campPlaceService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {

    var serviceBase = localStorageService.get('serviceBase');
    var campPlaceServiceFactory = {};

    var _getUsersCampPlaces = function (userName, callback)
    {
	    $http.get(serviceBase + 'api/Users/' + userName + '/CampPlaces').then
	    (
	        function (response) 
	        {
	            if(response.status == "204")
	            {
	        		callback('204');		
	            }   
            	else
            	{
            		callback(response.data);
            	}
	        },
	        function (err) 
	        {
	            console.log(err);
	        }
	    );
    };

    var _getCampPlaceData = function (campPlaceId, callback)
    {
	    $http.get(serviceBase + 'api/CampPlaces/' + campPlaceId).then
	    (
	        function (response) 
	        {
	            if(response.status == "204")
	            {
	        		callback('204');		
	            }   
            	else
            	{
            		callback(response.data);
            	}
	        },
	        function (err)
	        {
	            console.log(err);
	        }
	    );
    };

    var _getCampPlacePosts = function (campPlaceId, callback)
    {
	    $http.get(serviceBase + 'api/CampPlaces/' + campPlaceId + '/Posts').then
	    (
	        function (response)
	        {
	            if(response.status == "204")
	            {
	        		callback('204');		
	            }   
            	else
            	{
            		callback(response.data.map(getMessages));
            	}
	        },
	        function (err)
	        {
	            console.log(err);
	        }
	    );	
    };

    function getMessages(value, index, array)
    {
        value.messages = {};

        $http.get(serviceBase + 'api/Posts/' + value.id + '/Messages').then
        (
            function (response)
            {
                var messages = response.data;
                value.messages = messages;
            },
            function (err)
            {
                console.log(err);
            }
        )

        return value;
    };

    var _createCampPlace = function (campPlace, callback)
    {
        $http.post(serviceBase + 'api/CampPlaces/', campPlace).then
        (
            function (response)
            {
            	callback();
                $location.path('/camp');
            },
            function (err)
            {
                console.log(err.statusText);
            }
        );
    };

    var _updateCampPlace = function (campPlace, callback)
    {
        $http.put(serviceBase + 'api/CampPlaces/', campPlace).then
        (
            function (response)
            {
            	callback();
                $location.path('/camp');
            },
            function (err)
            {
                console.log(err.statusText);
            }
        );
    };

    campPlaceServiceFactory.getUsersCampPlaces = _getUsersCampPlaces;
    campPlaceServiceFactory.getCampPlaceData = _getCampPlaceData;
    campPlaceServiceFactory.getCampPlacePosts = _getCampPlacePosts;

    return campPlaceServiceFactory;
}]);