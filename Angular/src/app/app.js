var app = angular.module('campApp', ['ngRoute', 'LocalStorageModule', 'ngImgCrop']);

app.config(function ($routeProvider) {

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "app/components/authentication/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "app/components/authentication/signup.html"
    });

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "app/components/user/home.html"
    });

    $routeProvider.when("/home/:userName", {
        controller: "homeController",
        templateUrl: "app/components/user/home.html"
    });

    $routeProvider.when("/profile", {
        controller: "profileController",
        templateUrl: "app/components/user/profile.html"
    });

    $routeProvider.when("/camp", {
        controller: "campPlaceController",
        templateUrl: "app/components/campPlace/campPlace.html"
    });

    $routeProvider.when("/createPlace", {
        controller: "createPlaceController",
        templateUrl: "app/components/campPlace/createPlace.html"
    });

    $routeProvider.when("/createPlace/:id", {
        controller: "createPlaceController",
        templateUrl: "app/components/campPlace/createPlace.html"
    });

    $routeProvider.when("/campPlaceHome/:campPlaceId", {
        controller: "campPlaceHomeController",
        templateUrl: "app/components/campPlace/campPlaceHome.html"
    });

    $routeProvider.when("/post", {
        controller: "postController",
        templateUrl: "app/components/post/post.html"
    });

    $routeProvider.when("/group", {
        controller: "groupController",
        templateUrl: "app/components/group/group.html"
    });

    $routeProvider.when("/createGroup", {
        controller: "createGroupController",
        templateUrl: "app/components/group/createGroup.html"
    });

    $routeProvider.when("/openGroup/:groupId", {
        controller: "openGroupController",
        templateUrl: "app/components/group/openGroup.html"
    });

    $routeProvider.when("/search/:type/:searchCriteria", {
        controller: "searchController",
        templateUrl: "app/components/search/search.html"
    });

    $routeProvider.when("/search/:type", {
        controller: "searchController",
        templateUrl: "app/components/search/search.html"
    });

    $routeProvider.when("/error", {
        controller: "errorController",
        templateUrl: "app/components/layout/error.html"
    });
});

app.constant('config', 
{
    //serviceBase: 'http://localhost:56332/'
    serviceBase: 'http://localhost:8080/CampNetwork/'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', '$location', 'localStorageService', function (authService, $location, localStorageService)
{
    authService.fillAuthData();
}]);