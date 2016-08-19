var app = angular.module('campApp', ['ngRoute', 'LocalStorageModule']);

app.config(function ($routeProvider) {


    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "app/views/signup.html"
    });

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "app/views/home.html"
    });

    $routeProvider.when("/profile", {
        controller: "profileController",
        templateUrl: "app/views/profile.html"
    });

    $routeProvider.when("/camp", {
        controller: "campController",
        templateUrl: "app/views/campPlace.html"
    });

    $routeProvider.when("/createPlace", {
        controller: "createPlaceController",
        templateUrl: "app/views/createPlace.html"
    });

    $routeProvider.when("/post", {
        controller: "postController",
        templateUrl: "app/views/post.html"
    });

    $routeProvider.when("/group", {
        controller: "groupController",
        templateUrl: "app/views/group.html"
    });

    $routeProvider.when("/createGroup", {
        controller: "createGroupController",
        templateUrl: "app/views/createGroup.html"
    });

    $routeProvider.when("/openGroup", {
        controller: "openGroupController",
        templateUrl: "app/views/openGroup.html"
    });

});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', 'localStorageService', function (authService, localStorageService)
{
    localStorageService.set('serviceBase', 'http://localhost:8080/CampNetwork/');
    authService.fillAuthData();
}]);