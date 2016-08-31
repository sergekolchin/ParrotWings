(function () {
    'use strict';

    var app = angular.module('pwApp', ['ui.router', 'ngMessages', 'ngAnimate',
        'ui.bootstrap', 'ui.bootstrap.tpls', 'pwApp.services', 'pwApp.controllers'
    ]);
    app.value('$', $);

    app.config(
        ['$stateProvider', '$urlRouterProvider', '$locationProvider',
            function ($stateProvider, $urlRouterProvider, $locationProvider) {

              //  $locationProvider.html5Mode(true).hashPrefix('!');
                $urlRouterProvider.otherwise('/');

                $stateProvider
               .state("app", {
                   abstract: true,
                   template: '<ui-view/>'
               })
                //public area
                .state('app.public', {
                    url: '/',
                    templateUrl: 'app/wellcome.html'
                })
                .state('app.public.signin', {
                    url: 'signin',
                    onEnter: ['$state', '$uibModal', function ($state, $uibModal) {
                        $uibModal.open({
                            animation: true,
                            size: 'sm',
                            templateUrl: 'app/signin/signinModal.html',
                            resolve: {},
                            controller: 'signinCtrl as vm'
                        }).result.finally(function () {
                            //state has not changed
                            if ($state.$current.name === 'app.public.signin'
                            ) {
                                $state.go('^');
                            }
                        });
                    }]
                })
                //private area
                .state('app.transactions', {
                    url: '/transactions',
                    resolve: { authenticate: authenticate },
                    controller: 'transactionCtrl as vm',
                    templateUrl: 'app/transaction/transaction.html'
                })

                function authenticate($q, currentUser, $state, $timeout) {
                    if (currentUser.getProfile().isLoggedIn) {
                        // Resolve the promise successfully
                        return $q.when()
                    } else {
                        // The next bit of code is asynchronously tricky.
                        $timeout(function () {
                            // This code runs after the authentication promise has been rejected.
                            // Go to the signin page
                            $state.go('app.public');
                            $state.go('app.public.signin');
                        })
                        // Reject the authentication promise to prevent the state from loading
                        return $q.reject()
                    }
                }

            }]);

    app.run(['$state', function ($state) {
        //console.log('app.public');
        //$state.go('app.public'); //make a transition to 'home' state when app starts
    }]);

}());