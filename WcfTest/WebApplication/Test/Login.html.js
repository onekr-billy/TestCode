/// <reference path="../JavaScripts/Backbone/backbone.js" />
/// <reference path="../JavaScripts/jquery-1.7.2.js" />
/// <reference path="../JavaScripts/Backbone/json2.js" />
/// <reference path="../JavaScripts/Backbone/underscore-1.3.1.js" />
/// <reference path="../JavaScripts/Backbone/backbone-localstorage.js" />


$(function () {

    var UserLogin = Backbone.Model.extend({
        defaults: function () {
            return {
                uid: "",
                pwd: ""
            };
        },
        initialize: function () {
            if (!this.get("uid"))
                alert("请输入用户名");
            if (!this.get("pwd"))
                alert("请输入密码");
        },
        toggle: function () {
            this.save({ uid: this.get("uid"), pwd: this.get("pwd") });
        },
        clear: function () {
            this.destroy();
        }
    });



});