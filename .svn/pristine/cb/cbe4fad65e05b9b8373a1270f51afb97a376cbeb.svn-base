var DlgAssetInStore = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.LoadDg(1, 10);
    },
    LoadDg: function (pageIndex, pageSize) {
        var dg = $("#dgDlgAssetInStore");
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}';
        $.ajax({
            url: "../Services/AssetService.svc/GetAssetInStore",
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
                dg.datagrid('loadData', eval("(" + result.Data + ")"));
            }
        });
    },
    OnDlg: function () {
        if ($("body").find("#dlgAssetInStore").length == 0) {
            $("body").append("<div id=\"dlgAssetInStore\"></div>");
        }
        var h = $(window).height();
        if (h > 580) h = 580;
        else h = h * 0.95;
        var w = $(window).width();
        if (w > 953) w = 953;
        else w = w * 0.95;
        $("#dlgAssetInStore").dialog({
            title: '选择资产',
            width: w,
            height: h,
            closed: false,
            cache: false,
            href: '../u/goutstore.html',
            modal: true,
            iconCls: 'icon-ok',
            buttons: [{
                id: 'btnSaveAssetInStore', text: '确定', iconCls: 'icon-ok', handler: function () {
                    var rows = $("#dgDlgAssetInStore").datagrid('getSelections');
                    if (!rows || rows.length < 1) {
                        $.messager.alert('错误提示', "请至少选择一行数据进行操作", 'error');
                        return false;
                    }
                    DlgAssetInStore.SetDatagrid(rows);
                    $('#dlgAssetInStore').dialog('close');
                }
            }, {
                id: 'btnCancelSaveAssetInStore', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAssetInStore').dialog('close');
                }
            }]
        })
    },
    SetDatagrid: function (rows) {
        var dg = $('#dgAsset');
        var oldRows = dg.datagrid('getRows');
        if (!oldRows || oldRows.length < 1) {
            dg.datagrid('loadData', DlgAssetInStore.GetJsonData(rows));
        }
        else {
            for (var i = 0; i < rows.length; i++) {
                var isExist = false;
                for (var j = 0; j < oldRows.length; j++) {
                    if (oldRows[j].Id == rows[i].Id) isExist = true;
                }
                if (!isExist) {
                    var currRow = rows[i];
                    dg.datagrid('insertRow', {
                        index: 0,	// index start with 0
                        row: {
                            AssetId: currRow.Id,
                            PictureUrl: currRow.PictureUrl,
                            Barcode: currRow.Barcode,
                            AssetName: currRow.Named,
                            OwnedCompany: currRow.OwnedCompany,
                            UseCompany: currRow.UseCompany,
                            UseCompanyDepmt: currRow.UseCompanyDepmt,
                            AssetUsePerson: currRow.UsePerson,
                            StoreLocation: currRow.StoreLocation
                        }
                    });
                }
            }
        }
    },
    GetJsonData: function (rows) {
        var sJson = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) sJson += ",";
            sJson += '{"AssetId":"' + rows[i].Id + '","PictureUrl":"' + rows[i].PictureUrl + '","Barcode":"' + rows[i].Barcode + '","AssetName":"' + rows[i].Named + '","OwnedCompany":"' + rows[i].OwnedCompany + '","UseCompany":"' + rows[i].UseCompany + '","UseCompanyDepmt":"' + rows[i].UseCompanyDepmt + '","AssetUsePerson":"' + rows[i].UsePerson + '","StoreLocation":"' + rows[i].StoreLocation + '"}';
        }
        sJson = "[" + sJson + "]";
        return eval("(" + sJson + ")");
    }
}