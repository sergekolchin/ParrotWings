'use strict';

angular.module('pwApp.controllers')
    .controller('transactionCtrl', ['transactionHub', 'transactionResource', '$scope', '$rootScope', '$filter',
        function (transactionHub, transactionResource, $scope, $rootScope, $filter) {
            var vm = this;
            $scope.transactions = [];
            vm.user = {};
   
            transactionHub.initialize();

            $rootScope.$on('onConnected', function (e, user) {
                $scope.$apply(function () {
                    //current user info
                    vm.user = user;
                    //query all history transactions
                    transactionResource.query({ id: user.id }, function (data) {
                        $scope.transactions = data;
                        for (var i = 0; i < $scope.transactions.length; i++) {
                            //if credit transaction - green color
                            if ($scope.transactions[i].userToId === user.id)
                                $scope.transactions[i].credit = true;
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
                    //check duplicate transaction
                    if (!containsObject(transaction, $scope.transactions)) {
                        //add new transaction
                        $scope.transactions.unshift(transaction);
                        //$scope.transactions.push(transaction);
                    }
                });
            });

            function containsObject(obj, list) {
                var i;
                for (i = 0; i < list.length; i++) {
                    if (list[i] === obj) {
                        return true;
                    }
                }
                return false;
            }

        }]);