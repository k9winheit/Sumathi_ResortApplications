//var app = angular.module('app', ['notificationModule', 'busyIndicatorModule', 'oitozero.ngSweetAlert', 'ngDialog', 'ngSanitize']);
var app = angular.module('app', []);

var serviceBase = 'http://localhost:51403/api';
var loginBase = 'http://localhost:51403/';

app.constant('ngAuthSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'DemoLS',
    loginServiceBasUri: loginBase
});

