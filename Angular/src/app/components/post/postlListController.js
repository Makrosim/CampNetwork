'use strict';
app.controller('postlistController', ['$http', '$scope', '$location', '$routeParams', 'localStorageService', function ($http, $scope, $location, $routeParams, localStorageService) {

    var serviceBase = localStorageService.get('serviceBase');
    var authData = localStorageService.get('authorizationData');

	$scope.text ='';
	$scope.posts = {};
	$scope.posts.messages = {};
	$scope.userName = authData.userName;

	var string = serviceBase + 'api/Post/?groupId=' + $scope.$parent.groupId;

    $scope.$on('dataReceived', function (event, data)
    {
  		$scope.posts = data;
	});

	$scope.deletePost = function (postId)
	{
        $http.delete(serviceBase + 'api/Post/?userName=' + authData.userName + '&postId=' + postId).then
        (
        	function (response)
	        {

	        },
	        function (err)
	        {
	            console.log(err);
	        }
        );
	};

	$scope.delete = function (messageId, postId, messageIndex, postIndex)
	{
        $http.delete(serviceBase + 'api/Message/?messageId=' + messageId + '&postId=' + postId).then
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

        $http.post(serviceBase + 'api/Message/?userName=' + authData.userName, message).then
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
	};

}]);