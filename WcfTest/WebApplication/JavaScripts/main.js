/// <reference path="sea-debug.js" />


/*
//require.js
require([
  'jquery',
  'jquerymobile'
], function (context) {
    PageReady(window, jQuery);
},erFun);
*/

seajs.config({
    base:'../javascripts/',
    alias: {
        'jquery': 'jquery-1.7.2'
    },
    charset:'utf-8',
    timeout:20000,
    preload: ['plugin-text'],
    debut:0
});

/*
seajs.use(['jquery'], function (jquery) {
    var $ = arguments[0];
    console.log($);
});
*/

define(function (require, exports, module) {
    console.log(0);
    require('jquery', function () {
        console.log($);
    });
    require('jquery');
    require('Backbone/json2.js');
    console.log(1);
    //var $ = require('jquery.js#');
    //var $ = require('jquery.ashx?t=a');
    console.log($);
    //var json = require('Backbone/json2.js');

});






