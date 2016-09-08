'use strict';
app.controller('postlistController', ['$http', '$scope', '$location', '$routeParams', 'localStorageService', function ($http, $scope, $location, $routeParams, localStorageService) {

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');

	$scope.text ='';
	$scope.posts = null;
	$scope.userName = authData.userName;

    $scope.$on('dataReceived', function (event, data)
    {
  		$scope.posts = data;
	});

    $scope.set = function (index)
    {
        var area = document.getElementById(index);                    
        area.hidden = !area.hidden;
    };

	$scope.deletePost = function (postId, postIndex)
	{
        $http.delete(serviceBase + 'api/Posts/' + postId).then
        (
        	function (response)
	        {
				$scope.posts.splice(postIndex);
	        },
	        function (err)
	        {
	            console.log(err);
	        }
        );
	};

	$scope.delete = function (messageId, postId, messageIndex, postIndex)
	{
        $http.delete(serviceBase + 'api/Posts/' + postId + '/Messages/' + messageId).then
        (
        	function (response)
	        {
	        	if(postIndex === undefined)
	        	{
					$scope.posts.messages.splice(messageIndex);
	        	}
	        	else
	        	{
	        		$scope.posts[postIndex].messages.splice(messageIndex, 1);
	        	}    			
	        },
	        function (err)
	        {
	            console.log(err.statusText);
	        }
        );
	};

	$scope.comment = function (text, postId, postIndex)
	{
		var message = {};
		message.text = text;
		message.postId = postId;
		message.author = authData.userName;
		message.date = new Date();

        $http.post(serviceBase + 'api/Messages/', message).then
        (
        	function (response)
	        {
	        	if(postIndex === undefined)
	        	{
	        		$scope.posts.messages.push(response.data);
	        	}
	        	else
	        	{
	        		$scope.posts[postIndex].messages.push(response.data);
	        	}
	        },
	        function (err)
	        {
	            console.log(err);
	        }
        );

        var area = document.getElementById(postId);                    
        area.hidden = true;
	};

}]);