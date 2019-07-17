var ListAssetInStore = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        $("#abtnAdd").click(function () {
            ListAssetInStore.OnAdd();
        })
        $("#abtnEdit").click(function () {
            ListAssetInStore.OnEdit();
        })
        $("#abtnDel").click(function () {
            ListAssetInStore.OnDel();
        })
    },
    InitData: function () {
        this.LoadDg(1,10);
    },
    Container: $("#dlgListAssetInStore"),
    LoadDg: function (pageIndex, pageSize) {
        var dg = $("#dgT");
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}';

        $.ajax({
            url: "" + All.ServerUrl() + "/Services/PdaService.svc/GetAssetInStore",
            type: "POST",
            data: '{"model":' + sData + '}',
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                $.messager.progress({ title: '请稍等', msg: '正在执行...' });
            },
            complete: function () {
                $.messager.progress('close');
            },
            success: function (result) {
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return;
                }
                var jsonData = eval("("+result.Data+")");
                dg.datagrid('loadData', jsonData);
                var pager = dg.datagrid('getPager');
                pager.pagination({
                    onSelectPage: function (pageNumber, pageSize) {
                        ListAssetInStore.LoadDg(pageNumber, pageSize);
                    }
                })
            }
        });
    },
    OnAdd: function () {
        var container = this.Container;
        container.find('[name=hAction]').val('OnAdd');
        var h = $(window).height();
        var w = $(window).width();
        $("#dlgAddAssetInStore").dialog({
            title: '新增资产信息',
            width: w,
            height: h,
            closed: false,
            href: '/asset/Mobile/www/AssetSite/Asset/AddAssetInStore.html',
            modal: true
        })
        return false;
    },
    OnEdit: function () {
        var container = this.Container;
        container.find('[name=hAction]').val('OnEdit');
        var rows = $("#dgT").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var h = $(window).height();
        var w = $(window).width();
        $("#dlgAddAssetInStore").dialog({
            title: '新增资产信息',
            width: w,
            height: h,
            closed: false,
            href: '/asset/Mobile/www/AssetSite/Asset/AddAssetInStore.html',
            modal: true
        })
        return false;
    },
    OnDel: function () {
        var container = this.Container;
        container.find('[name=hAction]').val('OnDel');
        var rows = $("#dgT").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "" + All.ServerUrl() + "/Services/PdaService.svc/DeleteAssetInStore",
                    type: "post",
                    data: '{"itemAppend":"' + itemAppend + '"}',
                    contentType: "application/json; charset=utf-8",
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

                        ListAssetInStore.LoadDg(1, 10);
                        jeasyuiFun.show("温馨提示", "保存成功！");
                    }
                });
            }
        });
    }
}