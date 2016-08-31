(function () {
    'use strict';

    var app = angular.module('pwApp.services')
        .directive('userPanel', ['currentUser', '$state',
    function (currentUser, $state) {
        return {
            restrict: 'E',
            templateUrl: 'app/signin/userPanel.html',
            controller: function ($scope, currentUser) {
                $scope.isLoggedIn = function () {
                    return currentUser.getProfile().isLoggedIn;
                }
                $scope.name = function () {
                    return currentUser.getProfile().name;
                }
                $scope.logout = function () {
                    currentUser.logout();
                    $state.go('app.public');
                }
            }
        }
    }]);
}());