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
    LoadDg: function (pageIndex,pageSize) {
        var dg = $("#dgT");
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}';

        $.ajax({
            url: "../Services/AssetService.svc/GetAssetInStore",
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
        var h = $(window).height();
        if (h > 580) h = 580;
        else h = h * 0.95;
        var w = $(window).width();
        if (w > 750) w = 750;
        else w = w * 0.95;
        $("#dlgAddAssetInStore").dialog({
            title: '新增资产信息',
            width: w,
            height: h,
            closed: false,
            href: '../u/yinstore.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddAssetInStore.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddAssetInStore').dialog('close');
                }
            }]
        })
        return false;
    },
    OnEdit: function () {
        var rows = $("#dgT").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
            return false;
        }
        var h = $(window).height();
        if (h > 580) h = 580;
        else h = h * 0.95;
        var w = $(window).width();
        if (w > 750) w = 750;
        else w = w * 0.95;
        $("#dlgAddAssetInStore").dialog({
            title: '新增资产信息',
            width: w,
            height: h,
            closed: false,
            href: '../u/yinstore.html?Id=' + rows[0].Id + '',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddAssetInStore.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddAssetInStore').dialog('close');
                }
            }]
        })
        return false;
    },
    OnDel: function () {
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
                    url: "../Services/AssetService.svc/DeleteAssetInStore",
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
    },
    OnImport: function () {
        DlgUpload.TableName = "AssetInStore";
        DlgUpload.CallBack = "ListAssetInStore.OnImportCallBack()";
        DlgUpload.OnDlgUpload();
    },
    OnImportCallBack: function () {
        setTimeout(function () {
            window.location.reload();
        }, 1000);
    },
    OnExport: function () {

        var sData = { reqName: "ExportAssetInStore" };
        $.ajax({
            url: "/asset/h/upload.html",
            type: "post",
            data: sData,
            contentType: "application/x-www-form-urlencoded; charset=utf-8",
            beforeSend: function () {
                $.messager.progress({ title: '请稍等', msg: '正在执行...' });
            },
            complete: function () {
                $.messager.progress('close');
            },
            success: function (result) {
                console.log(result);
                var jsonData = eval("(" + result + ")");
                if (jsonData.ResCode != 1000) {
                    $.messager.alert('系统提示', jsonData.Msg, 'info');
                    return false;
                }

                var content = '<div style="width:150px; margin:10px auto;">导出成功，<a href="/asset/' + jsonData.Data + '" style="color:#FF0000;">下载文件</a></div>';
                $("#dlgAddAssetInStore").dialog({
                    title: '导出文件',
                    width: 300,
                    height: 100,
                    closed: false,
                    modal: true,
                    content: content
                })
            }
        });
    },
    FPicture: function (value, row, index) {
        if (value && value != '') return '<img src="/asset/' + value + '" alt="暂无图片" width="50" height="30" />';
        return '<img src="/asset/Images/nopicture.jpg" alt="暂无图片" width="50" height="30" />';
    }
}