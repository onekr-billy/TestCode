

var PageReady = function () {

    $("#t1").click(function () {
        $.ajax({
            url: "http://localhost:2524/Service.svc/GetData/1245",
            //contentType: "application/json",
            //data: '{"value": 1245}',
            type: 'GET',
            dataType: 'jsonp',
            success: function (html) {
                console.log(html);
                alert(html.StringValue);
            }
        });
    });

    $("#t2").click(function () {
        $.ajax({
            url: "WebApi.ashx",
            data: { method: "GetData", str: 1245 },
            type: 'GET',
            dataType: 'jsonp',
            success: function (html) {
                console.log(html);
                alert(html.StringValue);
            }
        });
    });

    $("#t3").click(function () {
        $.ajax({
            url: "RequestProxy.ashx?a=b",
            data: {
                _api: "test2",
                _type: 'GET',
                _params: '/123',
                //_url: "http://localhost:2524/Service.svc/GetData",
                //_contentType: "application/json",
                str: 1245,
                value: 1245
            },
            type: 'GET',
            dataType: 'jsonp',
            success: function (html) {
                console.log(html);
                alert(html.StringValue);
            }
        });
    });

    $("#t4").click(function () {
        $.ajax({
            url: "RequestProxy.ashx?a=b",
            data: {
                //_api: "test2",
                _type: 'POST',
                //_params: '/333',
                _data: '{"BoolValue":true,"StringValue":"abc"}',
                //entity: '{"BoolValue":true,"StringValue":"abc"}',
                _url: "http://localhost:2524/Service.svc/PostDatae"
                //_contentType: "application/json"
            },
            type: 'POST',
            dataType: 'json',
            success: function (html) {
                console.log(html);
                alert(html);
            }
        });
    });

};

var requires = [
  'jquery',
  'jquerymobile'
];

LoadJs(requires, PageReady);

