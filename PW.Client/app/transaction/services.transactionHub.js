'use strict';

angular.module('pwApp.services')
	.service('transactionHub',
			['appSettings', '$', '$rootScope',
				function (appSettings, $, $rootScope) {
					var proxy = null;

					var initialize = function () {
						var connection = $.hubConnection(appSettings.serverPath);
						this.proxy = connection.createHubProxy('transactionHub');

						//!!! You must register at least one of your event handler(s) before calling the start() method
						//!!! otherwise no client methods will be invoked from the server

						//Publishing an event when server pushes a greeting message
						this.proxy.on('acceptGreet', function (message) {
							console.log('server pushes: ' + message);
							$rootScope.$emit("acceptGreet", message);
						});

						connection.start()
                            .done(function () {
                            	console.log("Connected, transport = " + connection.transport.name);
                            	console.log('connection ID = ' + connection.id);
                            })
						.fail(function () { console.log('Could not connect'); });;
					};

					var sendRequest = function () {
						//Invoking greetAll method defined in hub
						this.proxy.invoke('greetAll');
					};

					return {
						initialize: initialize,
						sendRequest: sendRequest
					}
				}]);
