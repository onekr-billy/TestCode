/// <reference path="../jQueryMobile/jquery-1.8.0.js" />
/// <reference path="../jQueryMobile/jquery.mobile-1.1.1.js" />

var Debug = false;
window.mobile = {};
window.mobile.pages = {};

$(function () {

    PageFun.Init();

    $(document).bind("pageinit", function (event, data) {
        //Log("pageinit");
        //Log(data);
    });

    $(document).bind("pageload", function (event, data) {
        //Log("pageload");
        //Log(data);
        //CallFun(data.absUrl);
    });

    //    $(document).bind("pageshow", function (event, data) {
    //        Log("pageshow");
    //        Log(data);
    //    });

    $(document).bind("pagechange", function (event, data) {
        //Log("pagechange");
        //Log(data);
        PageFun.Init(data.toPage[0].id);
    });

});

//#region 调用 Page Function

//#region Page函数列表
// 命名规则，funName = pageId
// pageId = url 域名之后的 值，trim 前后的 反斜杠，然后替换所有反斜杠为 下划线
// 比如： http://localhost:38839/User/Info 页面id：User_Info_Page
var PageFuns = {
    Index_Page: function () {
        if (Debug)
            alert("加载 Index_Page");
    },
    User_Page: function () {
        if (Debug)
            alert("加载 User_Page");
    },
    User_Info_Page: function () {
        if (Debug)
            alert("加载 User_Info_Page");
        $("#info_name").append("abc");
        $("#info_save").click(function () {
            Log(0);
        });
    }
};
//#endregion

//#region 页面函数对象
function PageFun() {

}
//#endregion

//#region 获取函数
PageFun.GetFun = function (pageId) {

    var fun;

    $.each(PageFuns, function (key, val) {
        var regexp = new RegExp(key, "ig");
        if (regexp.test(pageId) && typeof val == "function")
            fun = val;
    });

    if (typeof fun != "function")
        fun = PageFuns.Index_Page;

    return fun;
};
//#endregion

//#region 页面函数初始化
//如果页面是手动打开，无需传入 pageid
PageFun.Init = function (pageId) {
    if (typeof pageId == "undefined") {
        pageId = $(document.body).find("div[data-role = 'page']").eq(0).attr("id");
    }
    var fun = PageFun.GetFun(pageId);
    var cacheFun = window.mobile.pages[pageId];
    if (Debug)
        alert("跳转到" + pageId);
    if (typeof cacheFun == "undefined") {
        if (typeof fun == "function") {
            fun();
            window.mobile.pages[pageId] = fun;
        }
    } else {

    }
};
//#endregion

//#endregion

function marketNavbar(index) {
    $(".navbar-link").removeClass("ui-btn-active ui-state-persist")
        .eq(index).addClass("ui-btn-active ui-state-persist");
}

function Log(data) {
    if (typeof (window.console) != "undefined") {
        window.console.log(data);
    } else {
        alert(data);
    }
}

