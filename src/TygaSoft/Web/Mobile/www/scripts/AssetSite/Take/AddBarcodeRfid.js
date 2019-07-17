var AddBarcodeRfid = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        var container = AddBarcodeRfid.Container;
        container.find("[name=abtnCommit]").click(function () {
            AddBarcodeRfid.OnSave();
        })
        container.find("[name=abtnReset]").click(function () {
            $('#AddBarcodeRfidFm').form('reset');
        })
        AddBarcodeRfid.AutoRun();
    },
    InitData: function () {

    },
    Container: $("#AddBarcodeRfidFm"),
    AutoRun:function(){
        setInterval(AddBarcodeRfid.AutoFocus, 1000);
    },
    AutoFocus: function () {
        var container = AddBarcodeRfid.Container;
        var txtBarcode = container.find("[name=Barcode]");
        if ($.trim(txtBarcode.val()) == "") {
            if (!txtBarcode.is(":focus")) {
                txtBarcode.focus();
            }
        }
    },
    OnInputByBarcode: function () {
        var container = AddBarcodeRfid.Container;
        var barcode = $.trim(container.find("[name=Barcode]").val());
        var data = AddBarcodeRfid.GetBarcodeRfid();
        var rfid = data[barcode];
        container.find("[name=rfid]").val(rfid);
    },
    OnSave: function () {
        var container = AddBarcodeRfid.Container;
        var barcode = $.trim(container.find("[name=Barcode]").val());
        var rfid = $.trim(container.find("[name=rfid]").val());
        var rowData = {
            "Barcode": barcode,
            "RFID": rfid
        };
        if (!ListBarcodeRfid.InsertRow(rowData)) {
            return false;
        }
        $.messager.alert('温馨提示', '操作成功', 'info', function () {
            $('#AddBarcodeRfidFm').form('reset');
        });
    },
    GetBarcodeRfid: function () {
        var data = {
            "100001031100113": "10023110042001",
            "100001031100003": "10031110042002",
            "100001030500020": "10161211004203",
            "100001030500019": "13653110042004"
        };
        return data;
    }
}