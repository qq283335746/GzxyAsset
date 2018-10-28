var Login = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    Container: $("#dlgLogin"),
    InitEvent: function () {
        var container = this.Container;
        container.find("[name=abtnLogin]").click(function () {
            Login.OnLogin();
            return false;
        })
        //container.find("#imgLoginCode").click(function () {
        //    $(this).attr("src", All.ServerUrl() + "/Handlers/ValidateCode.ashx?vcType=login&abc=" + Math.random());
        //    return false;
        //})
        container.find("[name=abtnToRegister]").click(function () {
            if ($("body").find("#dlgRegister").length == 0) {
                $("body").append("<div id=\"dlgRegister\"></div>");
            }
            container.dialog('close');
            $("#dlgRegister").dialog('refresh', 'Register.html').dialog('open');
            return false;
        })
    },
    InitData: function () {
        var container = Login.Container;
        //container.find("#imgLoginCode").attr("src", All.ServerUrl() + "/Handlers/ValidateCode.ashx?vcType=login");
    },
    ToLoginHref:function(){
        //window.location = "Login.html";
        if ($("body").find("#dlgLogin").length == 0) {
            $("body").append("<div id=\"dlgLogin\"></div>");
        }
        $("#dlgLogin").dialog({
            title: '',
            width: $(window).width(),
            height: $(window).height(),
            closed: false,
            cache: false,
            href: '/asset/Mobile/www/Login.htm',
            modal: true
        })
    },
    IsLogin:function(){
        return $.trim(Login.UserName) != "";
    },
    OnLogin: function () {
        var container = this.Container;
        var isValid = container.find("#loginFm").form('validate');
        if (!isValid) {
            return false;
        }
        var userName = $.trim(container.find("[name=UserName]").val());
        var psw = $.trim(container.find("[name=Psw]").val());
        var validCode = "0000";
        //var validCode = $.trim(container.find("[name=ValidCode]").val());
        //if (validCode.length != 4) {
        //    $.messager.alert('错误提示', "请正确输入验证码", 'info');
        //    return false;
        //}
        var url = All.ServerUrl() + "/Services/SecurityService.svc/Login";
        var postData = '{"appKey":"' + Login.AppKey + '","userName":"' + userName + '","password":"' + psw + '","validateCode":"' + validCode + '"}';

        $.ajax({
            url: url,
            type: "POST",
            contentType: "application/json",
            data: postData,
            dataType: 'json',
            timeout: 180000,
            beforeSend: function () {
                $.messager.progress({ title: '请稍等', msg: '正在执行...' });
            },
            complete: function () {
                $.messager.progress('close');
            },
            success: function (result) {

                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                Login.UserName = userName;

                var menuTabs = $("#menuTab");
                var roleAppend = result.Data;

                if (roleAppend.indexOf('Thailand') != -1) {
                    menuTabs.tabs('select', 2);
                }
                else {
                    menuTabs.tabs('disableTab', 2);
                    menuTabs.tabs('disableTab', 3);
                }
                if (roleAppend.indexOf('China') == -1) {
                    $("#menuChinaTake").hide();
                    menuTabs.tabs('disableTab', 0);
                    menuTabs.tabs('disableTab', 1);
                }

                Login.Container.dialog('close');
            }
        });
    },
    UserName: "",
    AppKey: "DFE95D05-E044-4E12-BEC8-278ADE2BC708"
}