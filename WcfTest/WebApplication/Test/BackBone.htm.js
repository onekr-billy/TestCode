

$(function () {

    var User = {
        Model: {},
        View: {},
        Router: {},
        initialize: function () {
            var uRouter = new User.Router();
            Backbone.history.start();
        }
    };

    //#region 初始化模型
    User.Model = Backbone.Model.extend({
        defaults: {
            id: 0,
            name: "empty",
            imgs: []
        },
        initialize: function () {
            console.log("模型初始化");

            //属性事件监控
            //监控事件名称：
            //1.change:name change事件名 name监控属性
            //2.all 监控所有事件
            this.bind("change:name", function (e) {
                console.log("您修改了name的值为" + this.get("name"));
            });
        },
        t_img_append: function (img) {
            var _imgs = this.get("imgs");
            _imgs.push(img);
            this.set({ imgs: _imgs });
        },
        t_name_edit: function (name) {
            var _name = this.get("name");
            this.set({ name: _name });
        }
    });

    //设置属性 
    //1
    //var uModel = new User.Model({ id: 100, name: "张三" });
    //2
    var uModel = new User.Model();
    uModel.set({ id: 101, name: "李斯1" });

    //获取属性
    var _name = uModel.get("name");

    //自定义函数
    uModel.t_img_append("001.jpg");
    uModel.t_img_append("002.jpg");
    var _imgs = uModel.get("imgs");

    //事件绑定
    uModel.t_name_edit("测试名称");
    console.log(uModel.get("name"));

    //console.log(uModel);
    //#endregion

    //#region 初始化视图
    User.View = Backbone.View.extend({
        initialize: function () {
            console.log("视图初始化");
            this.render();
        },
        //el:$("#search_container"),
        render: function () {
            var template = _.template($("#search_template").html(), {
                name: "search name",
                ids: [1, 2, 3]
            });
            $(this.el).html(template);
            return this;
        },
        events: {
            "click input[type='button']": "doSearch",
            "focus #search_input": "doSearch",
            "blur #search_input": "doSearch"
        },
        doSearch: function (e) {
            console.log("Search for " + $("#search_input").val());
        }
    });

    //初始化View 并加载模板和绑定事件 基于 dom 的事件
    var uView = new User.View({ el: $("#search_container") });
    //#endregion

    //#region 初始化路由
    User.Router = Backbone.Router.extend({
        initialize: function () {
            console.log("路由初始化");
        },
        routes: {
            "help/:page": "help",
            "download/*path": "download",
            "folder/:name": "openFolder",
            "folder/:name-:mode": "openFolder",
            "*actions": "defaultRoute"
        },
        help: function (page) {
            console.log(page);
        },
        download: function (path) {
            console.log(path);
        },
        defaultRoute: function (actions) {
            console.log(actions);
        }
    });

    //var uRouter = new User.Router();
    //Backbone.history.start();
    //#endregion

    User.initialize();

    var store = new Store();
    for (var i = 0; i < 10; i++)
        console.log(store.guid());


});