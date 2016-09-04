'use strict';

angular.module('pwApp.services')
	.service('transactionHub',
			['appSettings', 'currentUser', '$', '$rootScope',
				function (appSettings, currentUser, $, $rootScope) {
				    var proxy = null;
				    var userBalance = null;

					var initialize = function () {
					    var connection = $.hubConnection(appSettings.serverPath);
					    connection.qs = { 'access_token': currentUser.getProfile().token }
					    //client proxy for the TransactionHub class (without generated proxy)
						this.proxy = connection.createHubProxy('transactionHub');

						//!You must register at least one of your event handler(s) before calling the start() method
						//!otherwise no client methods will be invoked from the server

                        //publishing an event on user connected
						this.proxy.on('onConnected', function (user) {
						    userBalance = user.balance;
						    $rootScope.$emit('onConnected', user);
						});

					    //publishing an event then transaction added
						this.proxy.on('transactionAdded', function (transaction, user) {
						    userBalance = user.balance;
						    $rootScope.$emit('transactionAdded', transaction, user);
						});

						connection.start()
                            .done(function () {
                            	//console.log('Connected, transport = ' + connection.transport.name);
                            	//console.log('connection ID = ' + connection.id);
                            })
						.fail(function () { console.log('Could not connect'); });;
					};

					var addTransaction = function (transaction) {
					    this.proxy.invoke('addTransaction', transaction);
					};

					var getUserBalance = function () {
					    return userBalance;
					};

					return {
					    initialize: initialize,
					    addTransaction: addTransaction,
					    getUserBalance: getUserBalance
					}
				}]);
