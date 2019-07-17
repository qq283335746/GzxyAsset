
var Default = {
    Init: function () {
        this.InitEvent();
        if (!Login.IsLogin()) {
            Login.ToLoginHref();
            return;
        }
    },
    InitEvent: function () {
        $("#abtnAsset").click(function () {
            if ($("body").find("#dlgViewAsset").length == 0) {
                $("body").append("<div id=\"dlgViewAsset\"></div>");
            }
            $("#dlgViewAsset").dialog({
                title: '',
                width: $(window).width(),
                height: $(window).height(),
                closed: false,
                cache: false,
                href: '/asset/Mobile/www/AssetSite/Asset/Menu.html',
                modal: true
            });
        });
        $("#abtnPandian").click(function () {
            if ($("body").find("#dlgListPandian").length == 0) {
                $("body").append("<div id=\"dlgListPandian\"></div>");
            }
            $("#dlgListPandian").dialog({
                title: '',
                width: $(window).width(),
                height: $(window).height(),
                closed: false,
                cache: false,
                href: '/asset/Mobile/www/AssetSite/Pandian/ListPandian.html',
                modal: true
            });
        });
        $("#abtnBase").click(function () {
            if ($("body").find("#dlgViewBase").length == 0) {
                $("body").append("<div id=\"dlgViewBase\"></div>");
            }
            $("#dlgViewBase").dialog({
                title: '',
                width: $(window).width(),
                height: $(window).height(),
                closed: false,
                cache: false,
                href: '/asset/Mobile/www/AssetSite/Base/Menu.html',
                modal: true
            })
        });
        $("#abtnTabTakeCargo").click(function () {
            if ($("body").find("#dlgTabTakeCargo").length == 0) {
                $("body").append("<div id=\"dlgTabTakeCargo\"></div>");
            }
            $("#dlgTabTakeCargo").dialog({
                title: '',
                width: $(window).width(),
                height: $(window).height(),
                closed: false,
                cache: false,
                href: '/asset/Mobile/www/AssetSite/Take/TabTakeCargo.html',
                modal: true
            })
        });
        $("#abtnExitApp").click(function () {
            $.messager.confirm('温馨提示', '确定要退出吗？', function (r) {
                if (r) {
                    //navigator.app.exitApp();
                    Login.UserName = "";
                    window.location = 'http://www.tygaweb.com/asset/m/index.html';
                }
            });
            return false;
        })
        $("#abtnBarcodeScan").click(function () {
            if ($("body").find("#dlgBarcodeScan").length == 0) {
                $("body").append("<div id=\"dlgBarcodeScan\"></div>");
            }
            $("#dlgBarcodeScan").dialog({
                title: '',
                width: $(window).width(),
                height: $(window).height(),
                closed: false,
                cache: false,
                href: '/asset/Mobile/www/Scan/BarcodeScan.html',
                modal: true
            })
        });
    },
    OnDialog: function (dlgId, href, title) {
        if ($("body").find("#" + dlgId + "").length == 0) {
            $("body").append("<div id=\"" + dlgId + "\"></div>");
        }
        $("#" + dlgId + "").dialog({
            title: title,
            width: $(window).width(),
            height: $(window).height(),
            closed: false,
            cache: false,
            href: href,
            modal: true
        })
    },
    OnDialogFull: function (dlgId, href, title,w,h) {
        if ($("body").find("#" + dlgId + "").length == 0) {
            $("body").append("<div id=\"" + dlgId + "\"></div>");
        }
        $("#" + dlgId + "").dialog({
            title: title,
            width: w,
            height: h,
            closed: false,
            cache: false,
            href: href,
            modal: true
        })
    }
}