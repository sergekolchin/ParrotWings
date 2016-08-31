'use strict';
angular.module('pwApp.services', ['ngResource', 'LocalStorageModule', 'ui.validate'])
    .constant('appSettings',
    {
        serverPath: 'http://localhost:23007'
    });

