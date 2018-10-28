var ListBarcodeRfid = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {

    },
    Container: $("#tabListBarcodeRfid"),
    InsertRow: function (rowData) {
        var isExist = false;
        var dg = ListBarcodeRfid.Container.find("#dgRecord");
        var rows = dg.datagrid('getRows');
        for (var i = 0; i < rows.length; i++) {
            if ($.trim(rows[i].Barcode) == $.trim(rowData.Barcode)) {
                isExist = true;
            }
        }
        if (isExist) {
            $.messager.alert('错误提示', '列表中已存在相同数据', 'error');
            return false;
        }
        dg.datagrid('insertRow', {
            index: 0,
            row: {
                Id: 0,
                Barcode: rowData.Barcode,
                RFID: rowData.RFID
            }
        });
        return true;
    }
}