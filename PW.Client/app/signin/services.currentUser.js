(function () {
    'use strict';

    var app = angular.module('pwApp.services');

    app.config(function (localStorageServiceProvider) {
        localStorageServiceProvider.setPrefix("currentUser");
        localStorageServiceProvider.setStorageType("sessionStorage");
    });

        app.factory('currentUser', ['localStorageService',
    function (localStorageService) {
        var profile = {
            isLoggedIn: false,
            id: '',
            name: '',
            email: '',
            token: ''
        };

        var setProfile = function (id, name, email, token) {
            profile.id = id;
            profile.name = name;
            profile.email = email;
            profile.token = token;
            profile.isLoggedIn = true;
            localStorageService.set('currentUser', profile);
        };

        var getProfile = function () {
            var profile = localStorageService.get('currentUser');
            if (profile) return profile;
            else return {
                isLoggedIn: false,
                id: '',
                name: '',
                email: '',
                token: ''
            }
            //return profile;
        }

        var logout = function () {
            localStorageService.set('currentUser', {
                isLoggedIn: false,
                id: '',
                username: '',
                email: '',
                token: '' });
        }

        return {
            setProfile: setProfile,
            getProfile: getProfile,
            logout: logout
        }
    }]);
})();
