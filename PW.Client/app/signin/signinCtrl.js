(function () {
    'use strict';

    angular
        .module('pwApp.controllers')
        .controller('signinCtrl', ['userResource', 'currentUser', '$uibModalInstance', '$state', '$timeout',
            function MainCtrl(userResource, currentUser, $uibModalInstance, $state, $timeout) {
                var vm = this;
                vm.isLoggedIn = function () {
                    return currentUser.getProfile().isLoggedIn;
                };
                vm.message = '';
                vm.messageReg = '';
                vm.loginData = {
                    email: '',
                    password: ''
                };
                vm.regData = {
                    name: '',
                    email: '',
                    password: '',
                    confirmPassword: ''
                };

                vm.login = function () {
                    vm.loginData.grant_type = 'password';
                    vm.loginData.userName = vm.loginData.email;

                    userResource.login.loginUser(vm.loginData,
                        function (data) { //success
                            vm.message = '';
                            vm.loginData.password = '';
                            currentUser.setProfile(data.id, data.name, data.email, data.access_token, data.balance);
                            $uibModalInstance.close();
                            $timeout(function () {
                                $state.go('app.transactions');
                            });
                        },
                        function (response) { //error
                            vm.loginData.password = '';
                            vm.message = response.statusText + '\r\n';
                            if (response.data && response.data.exceptionMessage)
                                vm.message += response.data.exceptionMessage;

                            if (response.data && response.data.error) {
                                vm.message += response.data.error;
                            }
                        });
                }

                vm.register = function () {
                    vm.regData.confirmPassword = vm.regData.password;
                    userResource.registration.registerUser(vm.regData,
                        function (data) { //success
                            vm.confirmPassword = '';
                            $uibModalInstance.close();
                            vm.loginData.email = vm.regData.email;
                            vm.loginData.password = vm.regData.password;
                            vm.login();
                        },
                        function (response) { //error
                            currentUser.logout();
                            vm.messageReg = response.statusText + '\r\n';
                            if (response.data && response.data.exceptionMessage)
                                vm.messageReg += response.data.exceptionMessage;

                            // Validation errors
                            if (response.data && response.data.modelState) {
                                for (var key in response.data.modelState) {
                                    vm.messageReg += response.data.modelState[key] + '\r\n';
                                }
                            }
                        });
                }

                vm.cancel = function () {
                    $uibModalInstance.close();
                }
            }]);
})();
