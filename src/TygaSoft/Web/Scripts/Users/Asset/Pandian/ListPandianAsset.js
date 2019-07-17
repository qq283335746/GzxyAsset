var ListPandianAsset = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        $("[name=abtnSave]").click(function () {
            ListPandianAsset.OnSave();
        })
        $("[name=abtnDel]").click(function () {
            ListPandianAsset.OnDel();
        })
    },
    InitData: function () {
        this.LoadDg(1, 50);
    },
    Container: $("#dlgListPandianAsset"),
    LoadDg: function (pageIndex, pageSize) {
        var dg = $("#dgT");
        var Id = Common.GetRequestQueryStrByKey("Id");
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","PandianId":"' + Id + '"}';
        console.log(sData);
        $.ajax({
            url: "/asset/Services/AssetService.svc/GetPandianAssetList",
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

                var jsonData = eval("(" + result.Data + ")");
                if (jsonData.total == 0) return;

                dg.datagrid('loadData', jsonData);
                var pager = dg.datagrid('getPager');
                pager.pagination({
                    onSelectPage: function (pageNumber, pageSize) {
                        ListPandianAsset.LoadDg(pageNumber, pageSize);
                    }
                })
                var footerData = jsonData.footer;
                var lis = $('#toolbar>ul>li');
                lis.eq(0).text("已盘（" + footerData[0].TotalPan + "）");
                lis.eq(1).text("盘盈（" + footerData[0].TotalYpan + "）");
                lis.eq(2).text("未盘（" + footerData[0].TotalNotPan + "）");
            }
        });
    },
    OnSave: function () {
        var Id = Common.GetRequestQueryStrByKey("Id");
        var sData = '{"pandianId":"' + Id + '"}';
        $.ajax({
            url: "/asset/Services/AssetService.svc/SavePandianAssetResult",
            type: "POST",
            data: sData,
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

                $.messager.alert('温馨提示', "操作成功", 'info');
            }
        });
    },
    OnDel: function () {
        var rows = $("#dgT").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var pandianId = Common.GetRequestQueryStrByKey("Id");
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += "|";
            itemAppend += rows[i].AssetId;
        }
        if (!pandianId || pandianId == "") {
            $.messager.alert('错误提示', "非法操作，已终止执行", 'error');
            return false;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/asset/Services/AssetService.svc/DeletePandianAsset",
                    type: "post",
                    data: '{"pandianId":"' + pandianId + '", "itemAppend":"' + itemAppend + '"}',
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

                        ListPandianAsset.LoadDg(1, 50);
                        jeasyuiFun.show("温馨提示", "保存成功！");
                    }
                });
            }
        });
    }
}