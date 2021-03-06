﻿'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', 'config', function ($http, $q, localStorageService, config)
{
    var authServiceFactory = {};

    var _authentication = {
        isAuth: false
    };

    var _saveRegistration = function (registration)
    {
        _logOut();

        return $http.post(config.serviceBase + 'api/accounts', registration).then
        (
            function (response)
            {
                return response;
            }
        );
    };

    var _login = function (loginData)
    {
        var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        var deferred = $q.defer();

        $http.post(config.serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }).then
        (
            function (response)
            {
                localStorageService.set('authorizationData', { token: response.data.access_token, userName: loginData.userName });

                _authentication.isAuth = true;
                _authentication.userName = loginData.userName;

                deferred.resolve(response);
            },
            function (err)
            {
                _logOut();
                deferred.reject(err);
            }
        );

        return deferred.promise;
    };

    var _logOut = function ()
    {
        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";

    };

    var _fillAuthData = function ()
    {
        var authData = localStorageService.get('authorizationData');
        if (authData)
        {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
        }
    };

    authServiceFactory.saveRegistration = _saveRegistration;
    authServiceFactory.login = _login;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;

    return authServiceFactory;
}]);