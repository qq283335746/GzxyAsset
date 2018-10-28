var Menu = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        this.OnClickByAsset();
        this.OnClickByBase();
    },
    InitData: function () {
        this.InitDlMenu();
    },
    InitDlMenu: function () {
        var data = [
                { "group": "资产管理", "item": "资产入库" },
                { "group": "资产管理", "item": "领用退库" },
                { "group": "资产管理", "item": "借用归还" },
                { "group": "资产管理", "item": "实物信息变更" },
                { "group": "资产管理", "item": "财务信息变更" },
                { "group": "资产管理", "item": "维修信息登记" },
                { "group": "资产管理", "item": "清理报废" },
                { "group": "盘点管理", "item": "盘点管理" },
                { "group": "基础数据", "item": "组织结构" },
                { "group": "基础数据", "item": "资产分类" },
                { "group": "基础数据", "item": "区域" },
                { "group": "", "item": "注销" }
        ];
        $('#dlAllMenu').datalist({
            data: data,
            textField: 'item',
            groupField: 'group',
            textFormatter: function (value) {
                return '<a class="datalist-link">' + value + '</a>';
            },
            onClickRow: function (index, row) {
                var text = row.item;
                Menu.MenuGo(text);
            }
        })
        $('#mmbAsset').menubutton();
    },
    OnClickByAsset: function () {
        $('#dlgViewAsset .m-list a').click(function () {
            var text = $(this).text();
            Menu.MenuGo(text);
            return false;
        })
    },
    OnClickByBase: function () {
        $('#dlgViewBase .m-list a').click(function () {
            var text = $(this).text();
            Menu.MenuGo(text);
            return false;
        })
    },
    MenuGo: function (text) {
        var url = "";
        var dlgId = "";
        switch (text) {
            case "资产入库":
                url = '/asset/Mobile/www/AssetSite/Asset/ListAssetInStore.html';
                dlgId = "dlgListAssetInStore";
                break;
            case "领用退库":
                url = '/asset/Mobile/www/AssetSite/Asset/ListUseRefund.html';
                dlgId = "dlgListUseRefund";
                break;
            case "盘点管理":
                url = '/asset/Mobile/www/AssetSite/Pandian/ListPandian.html';
                dlgId = "dlgListPandian";
                break;
            default: break;
        }
        if ($("body").find("#" + dlgId + "").length == 0) {
            $("body").append("<div id=\"" + dlgId + "\"></div>");
        }
        $("#" + dlgId + "").dialog({
            title: '',
            width: $(window).width(),
            height: $(window).height(),
            closed: false,
            cache: false,
            href: url,
            modal: true
        })
        $("#abtnBack").click();
    }
}