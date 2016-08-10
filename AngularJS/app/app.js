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

});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);