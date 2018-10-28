var AddUseRefund = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        $("[name=abtnCommit]").click(function () {
            AddUseRefund.OnSave();
        })
        $("[name=abtnCancel]").click(function () {
            $('#dlgAddUseRefund').dialog('close');
        })
        $("[name=abtnReset]").click(function () {
            $('#dlgFm').form('reset');
        })
        $("[name=abtnAddAsset]").click(function () {
            DlgAssetInStore.OnDlg();
        })
        $("[name=abtnDelAsset]").click(function () {
            AddUseRefund.OnDelSelectAsset();
        })
    },
    InitData: function () {
        this.InitForm();
        var pcontainer = ListUseRefund.Container;
        var action = pcontainer.find('[name=hAction]').val();
        switch (action) {
            case "OnEdit":
                setTimeout(function () {
                    AddUseRefund.SetEdit();
                }, 1)
                break;
            default:
                break;
        }
    },
    Container: $("#dlgAddUseRefund"),
    InitForm:function(){
        var container = AddUseRefund.Container;
        container.find('#txtUseTime').datebox();
        container.find('#txtEstimateRefundTime').datebox();
        container.find('#dgAsset').datagrid();
    },
    SetEdit: function () {
        var container = AddUseRefund.Container;
        var pcontainer = ListUseRefund.Container;
        var row = pcontainer.find("#dgT").datagrid('getSelections')[0];
        container.find('[name=Id]').val(row.UseRefundId);
        container.find('[name=UsePerson]').val(row.UsePerson);
        container.find('#txtUseTime').datebox('setValue', row.SUseTime);
        container.find('#txtEstimateRefundTime').datebox('setValue', row.SEstimateRefundTime);
        container.find('[name=UseUserName]').val(row.UseUserName);
        container.find('[name=SRealRefundTime]').val(row.SRealRefundTime);
        container.find('[name=RefundDealUserName]').val(row.RefundDealUserName);
        container.find('[name=Remark]').val(row.Remark);
        AddUseRefund.GetAssetListBySearch(1, 10, row.UseRefundId);
    },
    GetAssetListBySearch: function (pageIndex, pageSize, useRefundId) {
        var container = AddUseRefund.Container;
        var dg = container.find("#dgAsset");
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","UseRefundId":"' + useRefundId + '"}';

        $.ajax({
            url: "../Services/AssetService.svc/GetAssetUseRefundList",
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
    OnSave: function () {
        var container = AddUseRefund.Container;
        var isValid = container.find('#dlgFm').form('validate');
        if (!isValid) return false;

        var assetIdAppend = "";
        var rows = container.find('#dgAsset').datagrid('getRows');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "选择的资产不能为空", 'error');
            return false;
        }
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) assetIdAppend += "|";
            assetIdAppend += rows[i].AssetId;
        }

        var usePerson = $.trim(container.find('[name=UsePerson]').val());
        var useTime = container.find('#txtUseTime').datebox('getValue');
        var estimateRefundTime = container.find('#txtEstimateRefundTime').datebox('getValue');
        var remark = $.trim(container.find('[name=Remark]').val());
        var Id = container.find('[name=Id]').val();

        var sData = '{"Id":"' + Id + '","UsePerson":"' + usePerson + '","SUseTime":"' + useTime + '","SEstimateRefundTime":"' + estimateRefundTime + '","Remark":"' + remark + '","AssetIdAppend":"' + assetIdAppend + '"}';

        $.ajax({
            url: "../Services/AssetService.svc/SaveAssetUseRefund",
            type: "post",
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
                    return false;
                }
                ListUseRefund.LoadDg(1, 10);
                $("#dlgAddUseRefund").dialog('close');
                jeasyuiFun.show("温馨提示", "保存成功！");
            }
        });
    },
    OnDelSelectAsset: function () {
        var container = this.Container;
        var dg = container.find("#dgAsset");
        var rows = dg.datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请至少选择一行数据再进行操作", 'error');
            return false;
        }
        $.messager.confirm('温馨提醒', '确定要从选择列表中删除吗？', function (r) {
            if (r) {
                for (var i = 0; i < rows.length; i++) {
                    var index = dg.datagrid('getRowIndex', rows[i]);
                    dg.datagrid('deleteRow', index);
                }
            }
        });
    }
}