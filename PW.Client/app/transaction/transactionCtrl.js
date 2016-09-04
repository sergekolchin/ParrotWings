'use strict';

angular.module('pwApp.controllers')
    .controller('transactionCtrl', ['transactionHub', 'transactionResource', '$scope', '$rootScope', '$filter',
        function (transactionHub, transactionResource, $scope, $rootScope, $filter) {
            var vm = this;
            vm.transactions = [];
            vm.user = {};

            vm.greetAll = function () {
                transactionHub.sendRequest();
            };

            transactionHub.initialize();

            $rootScope.$on('onConnected', function (e, user) {
                $scope.$apply(function () {
                    //current user info
                    vm.user = user;
                    //query all history transactions
                    transactionResource.query(function (data) {
                        vm.transactions = data;
                        for (var i = 0; i < vm.transactions.length; i++) {
                            //if credit transaction - green color
                            if (vm.transactions[i].userToId === user.id)
                                vm.transactions[i].credit = true;
                        }
                    });
                });
            });

            $rootScope.$on('transactionAdded', function (e, transaction, user) {
                $scope.$apply(function () {
                    //update user balance
                    vm.user = user;
                    //if credit transaction - green color
                    if (transaction.userToId === user.id)
                        transaction.credit = true;
                    //add new transaction
                    vm.transactions.unshift(transaction);
                });
            });
        }]);