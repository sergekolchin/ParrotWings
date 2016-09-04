(function () {
    'use strict';

    angular
        .module('pwApp.services')
        .factory('userResource',
                ['$resource', 'appSettings', 'currentUser',
                    function ($resource, appSettings, currentUser) {
                        return {
                            registration: $resource(appSettings.serverPath + '/api/Account/Register', null,
                                    {
                                        'registerUser': { method: 'POST' }
                                    }),
                            login: $resource(appSettings.serverPath + '/Token', null,
                                    {
                                        'loginUser': {
                                            method: 'POST',
                                            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
                                            transformRequest: function (data, headersGetter) {
                                                var str = [];
                                                for (var d in data)
                                                    str.push(encodeURIComponent(d) + '=' +
                                                                        encodeURIComponent(data[d]));
                                                return str.join('&');
                                            }
                                        }
                                    })
                        }
                    }])
    .factory('userResourceToken',
            ['$resource', 'appSettings', 'currentUser',
             function ($resource, appSettings, currentUser) {
                 return $resource(appSettings.serverPath + '/api/Users', null,
                     {
                         'query': {
                             headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token },
                             isArray: true
                         }
                     });
             }]);

})();

