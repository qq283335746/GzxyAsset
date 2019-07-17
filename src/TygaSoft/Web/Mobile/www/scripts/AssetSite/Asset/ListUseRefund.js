var ListUseRefund = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        $("#abtnAdd").click(function () {
            ListUseRefund.OnAdd();
        })
        $("#abtnEdit").click(function () {
            ListUseRefund.OnEdit();
        })
        $("#abtnDel").click(function () {
            ListUseRefund.OnDel();
        })
    },
    InitData: function () {
        this.LoadDg(1, 10);
    },
    Container: $("#dlgListUseRefund"),
    LoadDg: function (pageIndex, pageSize) {
        var container = ListUseRefund.Container;
        var dg = container.find("#dgT");
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}';

        $.ajax({
            url: "" + All.ServerUrl() + "/Services/AssetService.svc/GetAssetUseRefundList",
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
                dg.datagrid('loadData', jsonData);

                var pager = dg.datagrid('getPager');
                pager.pagination({
                    onSelectPage: function (pageNumber, pageSize) {
                        ListUseRefund.LoadDg(pageNumber, pageSize);
                    }
                })
            }
        });
    },
    OnAdd: function () {
        var h = $(window).height();
        if (h > 600) h = 600;
        else h = h * 0.95;
        var w = $(window).width();
        if (w > 953) w = 953;
        else w = w * 0.95;
        var container = this.Container;
        container.find('[name=hAction]').val('OnAdd');
        $("#dlgAddUseRefund").dialog({
            title: '领出单',
            width: w,
            height: h,
            closed: false,
            href: '/asset/Mobile/www/AssetSite/Asset/AddUseRefund.html',
            modal: true,
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddUseRefund.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddUseRefund').dialog('close');
                }
            }]
        })
        return false;
    },
    OnEdit: function () {
        var container = this.Container;
        container.find('[name=hAction]').val('OnEdit');
        var rows = container.find("#dgT").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        if (rows[0].Status != '领用') {
            $.messager.alert('错误提示', "当前领用单已被处理，不能再编辑", 'error');
            return false;
        }
        var h = $(window).height();
        if (h > 600) h = 600;
        else h = h * 0.95;
        var w = $(window).width();
        if (w > 953) w = 953;
        else w = w * 0.95;
        $("#dlgAddUseRefund").dialog({
            title: '领出单',
            width: w,
            height: h,
            closed: false,
            href: '/asset/Mobile/www/AssetSite/Asset/AddUseRefund.html',
            modal: true,
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddUseRefund.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddUseRefund').dialog('close');
                }
            }]
        })
        return false;
    },
    OnDel: function () {
        var container = this.Container;
        container.find('[name=hAction]').val('OnDel');
        var rows = $("#dgT").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请至少选择一行数据再进行操作", 'error');
            return false;
        }
        var isCanDel = true;
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (rows[i].Status != "领用") {
                isCanDel = false;
                return false;
            }
            if (i > 0) itemAppend += "|";
            itemAppend += rows[i].UseRefundId + "," + rows[i].AssetId;
        }
        if (!isCanDel) {
            $.messager.alert('错误提示', "删除失败，原因：只有状态为“领用”的数据才允许删除，选中的数据行中存在状态不是为“领用”的数据，不能删除!", 'error');
            return false;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "" + All.ServerUrl() + "/Services/AssetService.svc/DeleteAssetUseRefund",
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

                        ListUseRefund.LoadDg(1, 10);
                        jeasyuiFun.show("温馨提示", "保存成功！");
                    }
                });
            }
        });
    }
}