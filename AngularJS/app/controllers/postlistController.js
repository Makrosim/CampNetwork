'use strict';
app.controller('postlistController', ['$http', '$scope', '$location', '$timeout', 'localStorageService', function ($http, $scope, $location, $timeout, localStorageService) {

    var serviceBase = 'http://localhost:56332/';

    var authData = localStorageService.get('authorizationData');

	$scope.text ='';

	$scope.deletePost = function (postId)
	{
        $http.delete(serviceBase + 'api/Post/?postId=' + postId).then
        (
        	function (response)
	        {

	        },
	        function (err)
	        {
	            console.log(err.statusText);
	        }
        );
	};

	$scope.delete = function (messageId, postId)
	{
        $http.delete(serviceBase + 'api/Message/?messageId=' + messageId + '&postId=' + postId).then
        (
        	function (response)
	        {

	        },
	        function (err)
	        {
	            console.log(err.statusText);
	        }
        );
	};

	$scope.comment = function (text, postId)
	{
		var message = {};
		message.Text = text;
		message.postId = postId;

        $http.post(serviceBase + 'api/Message/?userName=' + authData.userName, message).then
        (
        	function (response)
	        {

	        },
	        function (err)
	        {
	            console.log(err.statusText);
	        }
        );
	};

    $http.get(serviceBase + 'api/Post/?userName=' + authData.userName).then
    (
    	function (response)
	    {
	        $scope.posts = response.data.map(getMessages);
	    },
	    function (err)
	    {
		    console.log(err.statusText);
	    }
    );

    function getMessages(value, index, array)
    {
    	value.messages = {};

	    $http.get(serviceBase + 'api/Message/?postId=' + value.id).then
	    (
	    	function (response)
		    {
		        var messages = response.data;
        	    value.messages = messages;
		    },
		    function (err)
		    {
			    console.log(err.statusText);
		    }
	    )

	    return value;
    };

}]);