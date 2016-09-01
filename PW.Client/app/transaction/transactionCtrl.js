'use strict';

angular.module('pwApp.controllers')
    .controller('transactionCtrl', ['transactionHub', '$scope', '$rootScope',
        function (transactionHub, $scope, $rootScope) {
            var vm = this;
            vm.text = 'asd';
            vm.transactions = '';

            vm.greetAll = function () {
                transactionHub.sendRequest();
            };

            vm.updateGreetingMessage = function (text) {
                vm.text = text;
            };

            transactionHub.initialize();

            //Updating greeting message after receiving a message through the event
            $rootScope.$on('acceptGreet', function (e, message) {
                $scope.$apply(function () {
                    console.log('$rootScope.$on');
                    vm.updateGreetingMessage(message)
                });
            });

            //get transactions history on connect
            $rootScope.$on('onConnected', function (e, transactions) {
                $scope.$apply(function () {
                    console.log('onConnected');
                    vm.transactions = transactions;
                });
            });
        }]);