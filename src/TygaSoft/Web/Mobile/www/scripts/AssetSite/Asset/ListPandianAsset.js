var ListPandianAsset = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
    },
    InitData: function () {
        this.InitForm();
        this.LoadPandianInfo($('#hPandianId').val());
        this.LoadListPandianAsset(1, 0, 0, "listNotPan");
        this.LoadListPandianAsset(1, 0, 1, "listAlreadyPan");
        this.LoadListPandianAsset(1, 0, 2, "listPanYing");
    },
    Container: $('#dlgListPandianAsset'),
    InitForm:function(){
        $('#tabPandianAsset').tabs();
        $('#mbOpList').menubutton();
    },
    PageIndex:1,
    LoadPandianInfo: function (pandianId) {
        try {
            var sData = '{"AppKey":"' + Login.AppKey + '","UserName":"' + Login.UserName + '","PageIndex":"' + 1 + '","PageSize":"' + 1 + '","PandianId":"' + pandianId + '"}';
            $.ajax({
                url: "" + All.ServerUrl() + "/Services/PdaService.svc/GetPandianList",
                type: "POST",
                data: '{"model":' + sData + '}',
                contentType: "application/json; charset=utf-8",
                timeout: 180000,
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
                    //console.log("result.Data---1--" + result.Data);
                    var jsonData = eval("(" + result.Data + ")");
                    if (jsonData.total == 0) return;
                    var rowData = jsonData.rows[0];
                    $('#lbPandianName').text(rowData.Name);
                    $('#lbPandianUser').text(rowData.CreateUserName);
                    $('#lbTotalQty').text(rowData.TotalQty);
                }
            });
        }
        catch (e) {
            $.messager.alert('异常提示', e.name + ": " + e.message, 'error');
        }
    },
    OnClickTab: function (title, index) {
        var panelId = "";
        switch (index) {
            case 0:
                panelId = "listNotPan";
                break;
            case 1:
                panelId = "listAlreadyPan";
                break;
            case 2:
                panelId = "listPanYing";
                break;
            default:
                break;
        }
        ListPandianAsset.LoadListPandianAsset(1, 0, index, panelId);
    },
    LoadListPandianAsset: function (pageIndex, pageSize, status, panelId) {
        try {
            var sData = '{"AppKey":"' + Login.AppKey + '","UserName":"' + Login.UserName + '","PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '","PandianId":"' + $('#hPandianId').val() + '","Status":"' + status + '"}';
            $.ajax({
                url: "" + All.ServerUrl() + "/Services/PdaService.svc/GetPandianAssetList",
                type: "POST",
                data: '{"model":' + sData + '}',
                contentType: "application/json; charset=utf-8",
                timeout: 180000,
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
                    //console.log("result.Data--" + result.Data);
                    var jsonData = eval("(" + result.Data + ")");
                    ListPandianAsset.SetListPandianAsset(jsonData, panelId);

                    ListPandianAsset.PageIndex++;
                }
            });
        }
        catch (e) {
            $.messager.alert('异常提示', e.name + ": " + e.message, 'error');
        }
    },
    SetListPandianAsset: function (jsonData, panelId) {
        if (jsonData.total < 1) return;

        var footerData = jsonData.footer[0];
        $('#lbTotalNotPan').text(footerData.TotalNotPan);
        $('#lbTotalpan').text(footerData.TotalPan);
        $('#lbTotalYpan').text(footerData.TotalYpan);

        var pandianAssetData = jsonData.rows;

        if (panelId == "listPanYing") {
            ListPandianAsset.SetListPanYing(pandianAssetData, panelId);
        }
        else {
            ListPandianAsset.SetListPandian(pandianAssetData, panelId);
        }
    },
    SetListPandian: function (data, panelId) {

        var index = 0;
        var htmlAppend = "";
        $.map(data, function (item) {
            var pictureUrl = "/asset/Mobile/www/images/pic_empty.jpg";
            if (item.PictureUrl != "") {
                pictureUrl = item.PictureUrl;
            }
            index++;
            var className = "b-c-odd";
            if (index % 2 == 0) {
                className = "b-c-even";
            }
            htmlAppend += "<ul class=\"m-list dg-list " + className + "\">";
            htmlAppend += "<li><input type=\"hidden\" name=\"AssetId\" value=\"" + item.AssetId + "\" /><img class=\"list-image\" src=\"" + pictureUrl + "\" /><div class=\"list-header\">" + index + " - " + item.AssetName + "</div><div class=\"list-content\"><a class=\"abtn_r\" onclick=\"ListPandianAsset.OnClickRow(this)\">" + item.PandianAssetStatus + "</a></div><span code=\"lbSureItem\" style=\"display:none;\"></span></li>";
            htmlAppend += "<li><span class=\"rl\">条形码</span><span class=\"dark\">" + item.Barcode + "</span><span class=\"clr\"></span></li>";
            htmlAppend += "<li><span class=\"rl\">资产分类</span><span class=\"dark\">" + item.Category + "</span><span class=\"clr\"></span></li>";
            htmlAppend += "<li><span class=\"rl\">规格型号</span><span class=\"dark\">" + item.SpecModel + "</span><span class=\"clr\"></span></li>";
            htmlAppend += "<li><span class=\"rl\">所属公司</span><span class=\"dark\">" + item.OwnedCompany + "</span><span class=\"clr\"></span></li>";
            htmlAppend += "<li><span class=\"rl\">使用公司</span><span class=\"dark\">" + item.UseCompany + "</span><span class=\"clr\"></span></li>";
            htmlAppend += "<li><span class=\"rl\">使用部门</span><span class=\"dark\">" + item.UseDepmt + "</span><span class=\"clr\"></span></li>";
            htmlAppend += "<li><span class=\"rl\">区域</span><span class=\"dark\">" + item.Region + "</span><span class=\"clr\"></span></li>";
            htmlAppend += "<li><span class=\"rl\">存放地点</span><span class=\"dark\">" + item.StoreLocation + "</span><span class=\"clr\"></span></li>";
            htmlAppend += "<li><span class=\"rl\">使用人</span><span class=\"dark\">" + item.UsePerson + "</span><span class=\"clr\"></span></li>";
            htmlAppend += "</ul>";
        })
        $("#" + panelId + "").html(htmlAppend);
    },
    SetListPanYing: function (data, panelId) {

        var htmlAppend = "";
        var index = 0;
        $.map(data, function (item) {
            index++;
            var sAppend = '{"AssetId":"' + item.AssetId + '","Status":"盘盈","Barcode":"' + item.Barcode + '","AssetName":"' + item.AssetName + '","SpecModel":"' + item.SpecModel + '","Unit":"' + item.Unit + '","Category":"' + item.Category + '","OwnedCompany":"' + item.OwnedCompany + '","Region":"' + item.Region + '","StoreLocation":"' + item.StoreLocation + '","UseCompany":"' + item.UseCompany + '","UseDepmt":"' + item.UseDepmt + '","UsePerson":"' + item.UsePerson + '","Remark":"' + item.Remark + '"}';
            htmlAppend += "<li>" + index + " - <span code=\"Barcode\">" + item.Barcode + "</span><div class=\"fr\"><a class=\"abtn_r\" onclick=\"ListPandianAsset.OnClickPanyingRow(this)\">盘盈</a></div><span code=\"lbSureItem\" style=\"display:none;\">" + sAppend + "</span>";
        })
        //console.log("htmlAppend---" + htmlAppend);
        $("#" + panelId + "").find("ul:first").html(htmlAppend);
    },
    OnClickRow: function (t) {
        var $_this = $(t);
        ListPandianAsset.AssetId = $_this.parent().parent().find('[name=AssetId]').val();
        $('#hSelectAssetId').val(ListPandianAsset.AssetId);
        Default.OnDialog("dlgAddPandianAsset", "/asset/Mobile/www/AssetSite/Pandian/AddPandianAsset.html", "");
    },
    OnClickPanyingRow: function (t) {
        var $_this = $(t);
        var li = $_this.parent().parent();
        ListPandianAsset.PanYingAssetId = li.find('[name=AssetId]').val();
        ListPandianAsset.PanYingBarcode = li.find('[code=Barcode]').text();
        Default.OnDialog("dlgAddPanYing", "/asset/Mobile/www/AssetSite/Pandian/AddPanYing.html", "");
    },
    OnSavePandian: function () {
        try {
            var container = ListPandianAsset.Container;
            var sJson = "";
            var index = 0;
            container.find('#listAlreadyPan').find("[code=lbSureItem]").each(function () {
                var currJson = $.trim($(this).text());
                if (currJson != "") {
                    if (index > 0) sJson += ",";
                    sJson += currJson;

                    index++;
                }
            })
            container.find('#listPanYing').find("[code=lbSureItem]").each(function () {
                var currJson = $.trim($(this).text());
                if (currJson != "") {
                    if (index > 0) sJson += ",";
                    sJson += currJson;

                    index++;
                }
            })
            if (sJson == "") {
                $.messager.alert('错误提示', "无任何可保存的数据", 'info');
                return;
            }
            sJson = "[" + sJson + "]";
            var pandianId = ListPandian.Container.find('#hPandianId').val();

            var sData = '{"AppKey":"' + Login.AppKey + '","UserName":"' + Login.UserName + '","PandianId":"' + pandianId + '","ItemList":' + sJson + '}';
            //console.log("sData--" + sData);
            //return false;
            $.ajax({
                url: "" + All.ServerUrl() + "/Services/PdaService.svc/SavePandianAsset",
                type: "POST",
                data: '{"model":' + sData + '}',
                contentType: "application/json; charset=utf-8",
                timeout: 180000,
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

                    ListPandianAsset.LoadListPandianAsset(1, 0, 0, "listNotPan");
                    ListPandianAsset.LoadListPandianAsset(1, 0, 1, "listAlreadyPan");
                    ListPandianAsset.LoadListPandianAsset(1, 0, 2, "listPanYing");
                    $.messager.alert('温馨提示', "操作成功", 'info');
                }
            });
        }
        catch (e) {
            $.messager.alert('异常提示', e.name + ": " + e.message, 'error');
        }
    },
    OnScanBarcode: function () {
        var w = $(window).width();
        var h = $(window).height();
        cordova.plugins.barcodeScanner.scan(
              function (result) {
                  var barcode = result.text;
                  if (barcode == "") return false;
                  MediaAudio.PlayAudio('/asset/Mobile/www/SysMedia/Audio/info.wav');
                  setTimeout(function () {
                      ListPandianAsset.OnAfterScanBarcode(barcode,w,h);
                  }, 100);
              },
              function (error) {
                  $.messager.alert('异常', '' + error + '', 'error');
              }
           );
    },
    OnPanY: function () {
        this.PanYingBarcode = '';
        Default.OnDialog("dlgAddPanYing", "/asset/Mobile/www/AssetSite/Pandian/AddPanYing.html", "");
    },
    OnAfterScanBarcode:function(barcode,w,h){
        
        var sData = '{"appKey":"' + Login.AppKey + '","userName":"' + Login.UserName + '","pandianId":"' + $('#hPandianId').val() + '","barcode":"' + barcode + '"}';
        $.ajax({
            url: "" + All.ServerUrl() + "/Services/PdaService.svc/GetPandianAssetByBarcode",
            type: "POST",
            data: sData,
            contentType: "application/json; charset=utf-8",
            timeout: 180000,
            beforeSend: function () {
                //$.messager.progress({ title: '请稍等', msg: '正在执行...' });
            },
            complete: function () {
                //$.messager.progress('close');
            },
            success: function (result) {
                //console.log("OnScanBarcode--"+result.Data);
                if (result.ResCode != 1000) {
                    ListPandianAsset.PanYingBarcode = barcode;
                    Default.OnDialogFull("dlgAddPanYing", "/asset/Mobile/www/AssetSite/Pandian/AddPanYing.html", "", w, h);
                    return false;
                }

                //$.messager.alert('温馨提示', "操作成功", 'info');
            }
        });
    },
    PanYingBarcode: '',
    AssetId: '',
    PanYingAssetId: ''
}