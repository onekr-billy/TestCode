

$(function () {

    $("#btn_exec_cmd").click(function () {
        $.ajax({
            url: "/Console/ConsoleInput",
            data: {
                args: $("#txt_cmd").val().trim()
            },
            dataType: "html",
            type: "get",
            cache: false,
            success: function (html) {
                WriteLog(html);
            }
        });
    });

    $("#btn_start").click(function () {
        DoExecAction("start");
    });

    $("#btn_stop").click(function () {
        DoExecAction("stop");
    });

    setInterval(function () {

        $.ajax({
            url: "/Console/ConsoleOutput",
            dataType: "html",
            type: "get",
            cache: false,
            success: function (html) {
                WriteLog(html);
            }
        });

    }, 3000);

});

var DoExecAction = function (act) {

    $.ajax({
        url: "/Console/ConsoleAction",
        data: {
            act: act
        },
        dataType: "html",
        type: "GET",
        cache: false,
        success: function (html) {
            WriteLog(html);
        }
    });

};

var WriteLog = function (log) {
    if ($.trim(log).length > 0) {
        $("#txt_log").text(($("#txt_log").text() + log) + "\r\n");
        var scrollTop = $("#txt_log").scrollTop();
        $("#txt_log").scrollTop(scrollTop);
    }
};