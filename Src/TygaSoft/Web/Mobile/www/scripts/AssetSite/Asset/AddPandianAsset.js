var AddPandianAsset = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var container = AddPandianAsset.Container;
        this.SetSelectAssetInfo();
        AddPandianAsset.CbbCompany("cbbUseCompany", $.trim(container.find("[name=hUseCompanyId]").val()),true);
        AddPandianAsset.CbtOrgDepmt($.trim(container.find("[name=hUseCompanyId]").val()), "cbtUseDepmt", $.trim(container.find("#hUseDepmtId").val()));
        AddPandianAsset.CbtRegion("cbtRegion", $.trim(container.find("[name=hRegionId]").val()));
    },
    Container: $("#dlgAddPandianAsset"),
    SelectRow: null,
    CbbCompany: function (cbbId, v, isOnSelect) {
        var container = AddPandianAsset.Container;
        $.ajax({
            url: "" + All.ServerUrl() + "/Services/PdaService.svc/GetCbbCompany",
            type: "GET",
            data: '',
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
            },
            complete: function () {
            },
            success: function (result) {
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                var jsonData = eval("(" + result.Data + ")");
                var cbb = container.find('#' + cbbId + '');
                cbb.combobox({
                    valueField: 'Id',
                    textField: 'Text',
                    data: jsonData,
                    onLoadSuccess: function () {
                        if (v != "") {
                            cbb.combobox('select', v);
                        }
                        else {
                            cbb.combobox('setValue', "请选择");
                        }
                    },
                    onSelect: function (record) {
                        if (isOnSelect) AddPandianAsset.CbtOrgDepmt(record.Id, "cbtUseDepmt", $.trim($("#hUseDepmtId").val()));
                    }
                });
            }
        });
    },
    CbtOrgDepmt: function (companyId, cbtId, v) {
        var container = AddPandianAsset.Container;
        var cbt = container.find('#' + cbtId + '');
        companyId = companyId.replace("请选择", "");
        if ($.trim(companyId) == "") {
            cbt.combotree();
            return false;
        }
        $.ajax({
            url: "" + All.ServerUrl() + "/Services/AssetService.svc/GetOrgDepmtTree",
            type: "POST",
            data: '{"companyId":"' + companyId + '"}',
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
            },
            complete: function () {
            },
            success: function (result) {
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                //console.log(result.Data);
                //return false;
                var jsonData = eval("(" + result.Data + ")");

                cbt.combotree({
                    data: jsonData,
                    readonly: false,
                    onLoadSuccess: function () {
                        if (v != "") {
                            cbt.combotree('setValue', v);
                        }
                        else {
                            if (jsonData.length > 0) cbt.combotree('setValue', jsonData[0].id);
                            else cbt.combotree('setValue', "请选择");
                        }
                    }
                });
            }
        });
    },
    CbtRegion: function (cbtId, v) {
        var container = AddPandianAsset.Container;
        $.ajax({
            url: "" + All.ServerUrl() + "/Services/PdaService.svc/GetRegionTree",
            type: "GET",
            data: '',
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
            },
            complete: function () {
            },
            success: function (result) {
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                var jsonData = eval("(" + result.Data + ")");
                var cbt = container.find('#' + cbtId + '');
                cbt.combotree({
                    data: jsonData,
                    onLoadSuccess: function () {
                        if (v != "") {
                            cbt.combotree('setValue', v);
                        }
                        else {
                            cbt.combotree('setValue', "请选择");
                        }
                    }
                });
            }
        });
    },
    SetSelectAssetInfo: function () {
        var container = AddPandianAsset.Container;
        var pcontainer = $('#dlgListPandianAsset');
        var lis;
        pcontainer.find("[name=AssetId]").each(function () {
            if ($(this).val() == ListPandianAsset.AssetId) {
                AddPandianAsset.SelectRow = $(this).parent().parent();
                lis = AddPandianAsset.SelectRow.children("li");
                return false;
            }
        })
        container.find('#lbBarcode').text(lis.eq(1).find('.dark').text());
        container.find(".abtn_r").text(lis.eq(0).find(".abtn_r").text());
        container.find("#lbAssetName").text(lis.eq(2).find(".dark").text());
        container.find("#lbSpecModel").text(lis.eq(3).find(".dark").text());
        var oldLis = container.find("#oldAssetInfo>li");
        oldLis.eq(0).find(".dark").text(lis.eq(7).find(".dark").text());
        oldLis.eq(1).find(".dark").text(lis.eq(8).find(".dark").text());
        oldLis.eq(2).find(".dark").text(lis.eq(5).find(".dark").text());
        oldLis.eq(3).find(".dark").text(lis.eq(6).find(".dark").text());
        oldLis.eq(4).find(".dark").text(lis.eq(9).find(".dark").text());
        var newData = $.trim(lis.find("[code=lbSureItem]").text());
        if (newData != "") {
            var jsonData = eval("(" + newData + ")");
            container.find('[name=hUseCompanyId]').val(jsonData.UseCompany);
            container.find('[name=hUseDepmtId]').val(jsonData.UseDepmt);
            container.find('[name=hRegionId]').val(jsonData.Region);
            container.find('[name=txtStoreLocation]').val(jsonData.StoreLocation);
            container.find('[name=txtUsePerson]').val(jsonData.UsePerson);
            container.find('[name=txtRemark]').val(jsonData.Remark);
        }
    },
    OnSure: function () {
        var container = this.Container;
        var sRegion = container.find('#cbtRegion').combotree('getValue').replace('请选择','');
        var sStoreLocation = container.find('#txtStoreLocation').textbox('getValue');
        var sUseCompany = container.find('#cbbUseCompany').combobox('getValue').replace('请选择', '');
        var sUseDepmt = container.find('#cbtUseDepmt').combotree('getValue').replace('请选择', '');
        var sUsePerson = container.find('#txtUsePerson').textbox('getValue');
        var sRemark = container.find('#txtRemark').textbox('getValue');
        var sAppend = '{"AssetId":"' + ListPandianAsset.AssetId + '","Status":"已盘点","AssetName":"","Barcode":"","SpecModel":"","Unit":"","Category":"","OwnedCompany":"","Region":"' + sRegion + '","StoreLocation":"' + sStoreLocation + '","UseCompany":"' + sUseCompany + '","UseDepmt":"' + sUseDepmt + '","UsePerson":"' + sUsePerson + '","Remark":"' + sRemark + '"}';
        AddPandianAsset.SelectRow.find("[code=lbSureItem]").text(sAppend);
        var abtn = AddPandianAsset.SelectRow.find(".abtn_r");
        var listAlreadyPan = $("#listAlreadyPan");
        var isExist = false;
        listAlreadyPan.find("[name=AssetId]").each(function () {
            if ($(this).val() == ListPandianAsset.AssetId) {
                isExist = true;
                return false;
            }
        })

        if (!isExist) {
            abtn.text("已盘点").addClass("f-c1");
            var totalNotPan = parseInt($("#lbTotalNotPan").text()) - 1;
            if (totalNotPan < 0) totalNotPan = 0;
            $("#lbTotalNotPan").text(totalNotPan);
            $("#lbTotalpan").text(parseInt($("#lbTotalpan").text()) + 1);

            AddPandianAsset.SelectRow.clone(true).prependTo(listAlreadyPan);
            AddPandianAsset.SelectRow.remove();
        }
        AddPandianAsset.Container.dialog('close');
    }
}