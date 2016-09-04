(function () {
    'use strict';

    angular
        .module('pwApp.services')
        .factory('transactionResource',
                ['$resource', 'appSettings', 'currentUser',
                 function ($resource, appSettings, currentUser) {
                     return $resource(appSettings.serverPath + '/api/transactions/:id', null,
                         {
                             'query': {
                                 headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token },
                                 isArray: true
                             },
                             'get': {
                                 headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
                             },
                             'save': {
                                 method: 'POST', //!set method! otherwise GET request instead POST
                                 headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
                             },
                             'update': {
                             method: 'PUT',
                             headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
                }
                         });
                 }])
    .factory('transactionByUserResource',
                ['$resource', 'appSettings', 'currentUser',
                 function ($resource, appSettings, currentUser) {
                     return $resource(appSettings.serverPath + '/api/Transactions/ByUser', null,
                         {
                             'query': {
                                 headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token },
                                 isArray: true
                             }
                         });
                 }]);
}());

