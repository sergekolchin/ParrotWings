(function () {
    'use strict';

    angular
        .module('pwApp.services')
        .factory('transactionResource',
                ['$resource',
                 'appSettings',
                 'currentUser',
                    productResource])

    function productResource($resource, appSettings, currentUser) {
        return $resource(appSettings.serverPath + '/api/transactions/:id', null,
            {
                'get': {
                    headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
                },

                'save': {
                    headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
                },

                'update': {
                    method: 'PUT',
                    headers: { 'Authorization': 'Bearer ' + currentUser.getProfile().token }
                }
            });
    }
}());

