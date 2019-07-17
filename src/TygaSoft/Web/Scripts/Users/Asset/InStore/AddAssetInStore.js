var AddAssetInStore = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.InitForm();
        AddAssetInStore.CbtCategory("cbtCategory", $.trim($("#hCategoryId").val()));
        AddAssetInStore.CbbCompany("cbbUseCompany", $.trim($("#hUseCompanyId").val()),true);
        AddAssetInStore.CbtOrgDepmt($.trim($("#hUseCompanyId").val()), "cbtUseDepmt", $.trim($("#hUseDepmtId").val()));
        AddAssetInStore.CbtRegion("cbtRegion", $.trim($("#hRegionId").val()));
        AddAssetInStore.CbbCompany("cbbOwnedCompany", $.trim($("#hOwnedCompanyId").val()),false);
    },
    InitForm: function () {
        //$('#' + cbtUseDepmt + '').combotree();
    },
    CbtCategory: function (cbtId,v) {
        $.ajax({
            url: "../Services/AssetService.svc/GetCategoryTree",
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
                var cbt = $('#' + cbtId + '');
                cbt.combotree({
                    data: jsonData,
                    onLoadSuccess: function () {
                        if (v != "") {
                            cbt.combotree('setValue', v);
                        }
                    }
                });
            }
        });
    },
    CbtOrgDepmt: function (companyId, cbtId, v) {
        var cbt = $('#' + cbtId + '');
        companyId = companyId.replace("请选择", "");
        if ($.trim(companyId) == "") {
            cbt.combotree();
            return false;
        }
        $.ajax({
            url: "../Services/AssetService.svc/GetOrgDepmtTree",
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
                var jsonData = eval("(" + result.Data + ")");

                cbt.combotree({
                    data: jsonData,
                    readonly:false,
                    onLoadSuccess: function () {
                        if (v != "") {
                            cbt.combotree('setValue', v);
                        }
                    }
                });
            }
        });
    },
    CbtRegion: function (cbtId, v) {
        $.ajax({
            url: "../Services/AssetService.svc/GetRegionTree",
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
                var cbt = $('#' + cbtId + '');
                cbt.combotree({
                    data: jsonData,
                    onLoadSuccess: function () {
                        if (v != "") {
                            cbt.combotree('setValue', v);
                        }
                    }
                });
            }
        });
    },
    CbbCompany: function (cbbId, v,isOnSelect) {
        $.ajax({
            url: "../Services/AssetService.svc/GetCbbCompany",
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
                var cbb = $('#' + cbbId + '');
                cbb.combobox({
                    valueField: 'Id',
                    textField: 'Text',
                    data: jsonData,
                    onLoadSuccess: function () {
                        if (v != "") {
                            cbb.combobox('select', v);
                        }
                    },
                    onSelect: function (record) {
                        if (isOnSelect) AddAssetInStore.CbtOrgDepmt(record.Id, "cbtUseDepmt", $.trim($("#hUseDepmtId").val()));
                    }
                });
            }
        });
    },
    CbbOrgDepmt: function (cbbId, v) {
        $.ajax({
            url: "../Services/AssetService.svc/GetCbbOrgDepmt",
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
                var cbb = $('#' + cbbId + '');
                cbb.combobox({
                    valueField: 'Id',
                    textField: 'Text',
                    data: jsonData,
                    onLoadSuccess: function () {
                        if (v != "") {
                            cbb.combobox('select', v);
                        }
                    }
                });
            }
        });
    },
    OnSave: function () {
        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;

        var categoryId = $('#cbtCategory').combotree('getValue');
        var useCompanyId = $('#cbbUseCompany').combobox('getValue');
        var useDepmtId = $('#cbtUseDepmt').combotree('getValue').replace("请选择","");
        var regionId = $('#cbtRegion').combotree('getValue');
        var ownedCompanyId = $('#cbbOwnedCompany').combobox('getValue');

        //var tCategory = $('#cbtCategory').combotree('tree');
        //var tUseCompany = $('#cbtUseCompany').combotree('tree');
        //var tRegion = $('#cbtRegion').combotree('tree');

        //var selectNode = tCategory.tree('getSelected');
        //if (!selectNode || selectNode.parentId == "00000000-0000-0000-0000-000000000000") {
        //    $('#cbtCategory').combotree('setValue', "");
        //    return false;
        //}
        //var categoryId = selectNode.id;
        //AddAssetInStore.CreateCbtText(tCategory, selectNode);
        //var categoryText = AddAssetInStore.GetCbtText();

        //selectNode = tUseCompany.tree('getSelected');
        //if (selectNode && selectNode.parentId == "00000000-0000-0000-0000-000000000000") {
        //    $('#cbtUseCompany').combotree('setValue', "");
        //    return false;
        //}
        //var useCompanyId = selectNode.id;
        //AddAssetInStore.CreateCbtText(tUseCompany, selectNode);
        //var useCompanyText = AddAssetInStore.GetCbtText();

        //selectNode = tRegion.tree('getSelected');
        //if (!selectNode || selectNode.parentId == "00000000-0000-0000-0000-000000000000") {
        //    $('#cbtRegion').combotree('setValue', "");
        //    return false;
        //}
        //var regionId = selectNode.id;
        //AddAssetInStore.CreateCbtText(tRegion, selectNode);
        //var regionText = AddAssetInStore.GetCbtText();

        //var ownedCompanyId = $('#cbbOwnedCompany').combobox('getValue');
        //var ownedCompanyText = $('#cbbOwnedCompany').combobox('getText');

        var barcode = $.trim($('#txtBarcode').val());
        var name = $.trim($('#txtName').val());
        var specModel = $.trim($('#txtSpecModel').val());
        var sSNCode = $.trim($('#txtSNCode').val());
        var unit = $.trim($('#txtUnit').val());
        var price = $.trim($('#txtPrice').val());
        if (price == "") price = 0;
        var buyDate = $('#txtBuyDate').datebox('getValue');
        var usePerson = $.trim($('#txtUsePerson').val());
        var manager = $.trim($('#txtManager').val());
        var storeLocation = $.trim($('#txtStoreLocation').val());
        var useExpireMonth = $.trim($('#txtUseExpireMonth').val());
        if (useExpireMonth == "") useExpireMonth = 0;
        var supplier = $.trim($('#txtSupplier').val());
        var rfid = $.trim($('#txtRFID').val());
        var pictureId = $.trim($('#hPictureId').val());
        var remark = $.trim($('#txtRemark').val());

        var sData = '{"Id":"' + $('#hId').val() + '","Barcode":"' + barcode + '","CategoryId":"' + categoryId + '","Named":"' + name + '","SpecModel":"' + specModel + '","SNCode":"' + sSNCode + '","Unit":"' + unit + '","Price":"' + price + '","UseCompanyId":"' + useCompanyId + '","UseDepmtId":"' + useDepmtId + '","SBuyDate":"' + buyDate + '","UsePerson":"' + usePerson + '","Manager":"' + manager + '","OwnedCompanyId":"' + ownedCompanyId + '","RegionId":"' + regionId + '","StoreLocation":"' + storeLocation + '","UseExpireMonth":"' + useExpireMonth + '","Supplier":"' + supplier + '","RFID":"' + rfid + '","PictureId":"' + pictureId + '","Remark":"' + remark + '"}';
        $.ajax({
            url: "/asset/Services/AssetService.svc/SaveAssetInStore",
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
                ListAssetInStore.LoadDg(1, 10);
                $("#dlgAddAssetInStore").dialog('close');
                jeasyuiFun.show("温馨提示", "保存成功！");
            }
        });
    },
    GetCbtText:function(){
        var s = AddAssetInStore.CbtText;
        AddAssetInStore.CbtText = "";
        return s;
    },
    CbtText:"",
    CreateCbtText: function (t, node) {
        var root = t.tree('getRoot');
        var parentNode = t.tree('getParent', node.target);
        if (node.id != root.id && AddAssetInStore.CbtText.indexOf(node.text) == -1) AddAssetInStore.CbtText += "|" + node.text;
        if (parentNode && parentNode.id != root.id) {
            AddAssetInStore.CbtText += "|" + parentNode.text;
            AddAssetInStore.CreateCbtText(t, parentNode);
        }
        
    }
}