
require.config({
    baseUrl: "/javascripts/",
    config: {
        'urls': {
            Root: '/',
            JsRoot: '/JavaScripts/',
            ImgRoot: '/Images/'
        }
    },
    //paths 文件别名配置
    paths: {
        jquery: ['jquery'],
        jquerymobile: 'jquery.mobile-1.1.0'
    },
    //依赖加载 'jquerymobile': ['jquery','live'] 加载 jquerymobile 模块需要首先加载 'jquery','live'
    shim: {
        'jquerymobile': ['jquery']
    },
    //不同版本加载
    map: {
        '*': { jquery: 'jquery' },
        'old': { jquery: 'jquery-1.7.2' },
        'new': { jquery: 'jquery' }
    }
});

//发生错误，则移除错误
var errorFun = function (err) {
    var failedId = err.requireModules && err.requireModules[0];
    console.log(failedId);
    requirejs.undef(failedId);
};

//加载Js
var LoadJs = function (requires, callback) {
    require(requires, function (context) {
        if (typeof (callback) == 'function')
            callback(window, jQuery);
    }, errorFun);
};


