/// <reference path="../JavaScripts/Backbone/backbone.js" />
/// <reference path="../JavaScripts/jquery-1.7.2.js" />
/// <reference path="../JavaScripts/Backbone/json2.js" />
/// <reference path="../JavaScripts/Backbone/underscore-1.3.1.js" />
/// <reference path="../JavaScripts/Backbone/backbone-localstorage.js" />

$(function () {

    var Goods = Backbone.Model.extend({
        defaults: function () {
            return {
                gid: 0,
                title: "",
                price: 0.0
            };
        },
        initialize: function () {
            

        },
        validate: function () {
            console.log("model.toggle");
        },
        clear: function () {
            
        }
    });

});