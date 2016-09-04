(function () {
'use strict';

angular.module('pwApp.controllers')
    .controller('addTransactionCtrl', ['newTransaction', 'transactionHub', '$scope', '$rootScope', '$uibModalInstance',
        'transactionByUserResource', 'userResourceToken', 'transactionResource', 'currentUser',
        function (newTransaction, transactionHub, $scope, $rootScope, $uibModalInstance,
            transactionByUserResource, userResourceToken, transactionResource, currentUser) {
            var vm = this;
            vm.userTransactions = null;
            vm.users = null;
            vm.message = '';
            vm.newTransaction = newTransaction;
            vm.userTo = null;

            //previous user transactions list
            transactionByUserResource.query(function (data) {
                vm.userTransactions = data;
            });

            //all users list except current
            userResourceToken.query(function (data) {
                vm.users = data;
            });

            vm.create = function () {
                if (transactionHub.getUserBalance() - vm.newTransaction.amount < 0) {
                    vm.addTrnForm.amount.$error.lessThanZero = true;
                    //console.log('amount.$error.validationError = true');
                } else {
                    vm.addTrnForm.amount.$error.lessThanZero = false;
                    //console.log('amount.$error.validationError = false');

                vm.newTransaction.userFromId = currentUser.getProfile().id;
                vm.newTransaction.userFromName = currentUser.getProfile().name;
                vm.newTransaction.userToId = vm.userTo.id;
                vm.newTransaction.userToName = vm.userTo.name;

                vm.newTransaction.$save(function (transaction) {//success
                    //send new transaction to transaction hub
                    transactionHub.addTransaction(transaction);
                    //console.log('Success: ' + JSON.stringify(transaction));
                    $uibModalInstance.close();
                },
                    function (response) { //error
                        vm.message = response.statusText + "\r\n";
                        if (response.data.modelState) {
                            for (var key in response.data.modelState) {
                                vm.message += response.data.modelState[key] + "\r\n";
                            }
                        }
                        if (response.data.exceptionMessage)
                            vm.message += response.data.exceptionMessage;
                    });
                }
            };

            vm.cancel = function () {
                $uibModalInstance.close();
            }
        }]);
})();