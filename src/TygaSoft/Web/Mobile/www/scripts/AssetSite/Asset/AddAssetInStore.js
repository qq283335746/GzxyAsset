var AddAssetInStore = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        $("[name=abtnCommit]").click(function () {
            AddAssetInStore.OnSave();
        })
        $("[name=abtnCancel]").click(function () {
            $('#dlgAddAssetInStore').dialog('close');
        })
        $("[name=abtnReset]").click(function () {
            $('#dlgFm').form('reset');
        })
    },
    InitData: function () {
        var pcontainer = ListAssetInStore.Container;
        var action = pcontainer.find('[name=hAction]').val();
        switch (action) {
            case "OnEdit":
                AddAssetInStore.SetEdit();
                break;
            default:
                break;
        }
        AddAssetInStore.CbtCategory("cbtCategory", $.trim($("#hCategoryId").val()));
        AddAssetInStore.CbtOrgDepmt("cbtUseCompany", $.trim($("#hUseCompanyId").val()));
        AddAssetInStore.CbtRegion("cbtRegion", $.trim($("#hRegionId").val()));
        AddAssetInStore.CbbCompany("cbbOwnedCompany", $.trim($("#hOwnedCompanyId").val()));
    },
    CbtCategory: function (cbtId,v) {
        $.ajax({
            url: "" + All.ServerUrl() + "/Services/PdaService.svc/GetCategoryTree",
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
                            //var t = cbt.combotree('tree');
                            //var node = t.tree('find', v);
                            //if (node) t.tree('select', node.target);
                        }
                        else {
                            cbt.combotree('setValue', "请选择");
                        }
                    }
                });
            }
        });
    },
    CbtOrgDepmt: function (cbtId,v) {
        $.ajax({
            url: "" + All.ServerUrl() + "/Services/PdaService.svc/GetOrgDepmtTree",
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
                            //var t = cbt.combotree('tree');
                            //var node = t.tree('find', v);
                            //if (node) t.tree('select', node.target);
                        }
                        else {
                            cbt.combotree('setValue', "请选择");
                        }
                    }
                });
            }
        });
    },
    CbtRegion: function (cbtId, v) {
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
                var cbt = $('#' + cbtId + '');
                cbt.combotree({
                    data: jsonData,
                    onLoadSuccess: function () {
                        if (v != "") {
                            cbt.combotree('setValue', v);
                            //var t = cbt.combotree('tree');
                            //var node = t.tree('find', v);
                            //if (node) t.tree('select', node.target);
                        }
                        else {
                            cbt.combotree('setValue', "请选择");
                        }
                    }
                });
            }
        });
    },
    CbbCompany: function (cbbId, v) {
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
                var cbb = $('#' + cbbId + '');
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
                    }
                });
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
        
    },
    SetEdit: function () {
        var row = $("#dgT").datagrid('getSelections')[0];
        $('#hId').val(row.Id);
        $('#hCategoryId').val(row.CategoryId);
        $('#hUseCompanyId').val(row.UseCompanyId);
        $('#hOwnedCompanyId').val(row.OwnedCompanyId);
        $('#hRegionId').val(row.RegionId);
        $('#hPictureId').val(row.PictureId);
        $('#txtBarcode').val(row.Barcode);
        $('#txtName').val(row.Named);
        $('#txtSpecModel').val(row.SpecModel);
        $('#txtSNCode').val(row.SNCode);
        $('#txtUnit').val(row.Unit);
        $('#txtPrice').val(row.Price);
        $('#txtBuyDate').val(row.BuyDate);
        $('#txtUsePerson').val(row.UsePerson);
        $('#txtManager').val(row.Manager);
        $('#txtStoreLocation').val(row.StoreLocation);
        $('#txtUseExpireMonth').val(row.UseExpireMonth);
        $('#txtSupplier').val(row.Supplier);
        $('#txtRemark').val(row.Remark);
    },
    OnSave: function () {

        var isValid = $('#dlgFm').form('validate');
        if (!isValid) return false;

        var tCategory = $('#cbtCategory').combotree('tree');
        var tUseCompany = $('#cbtUseCompany').combotree('tree');
        var tRegion = $('#cbtRegion').combotree('tree');

        var selectNode = tCategory.tree('getSelected');
        if (!selectNode || selectNode.parentId == "00000000-0000-0000-0000-000000000000") {
            $('#cbtCategory').combotree('setValue', "");
            return false;
        }
        var categoryId = selectNode.id;
        AddAssetInStore.CreateCbtText(tCategory, selectNode);
        var categoryText = AddAssetInStore.GetCbtText();

        selectNode = tUseCompany.tree('getSelected');
        if (selectNode && selectNode.parentId == "00000000-0000-0000-0000-000000000000") {
            $('#cbtUseCompany').combotree('setValue', "");
            return false;
        }
        var useCompanyId = selectNode.id;
        AddAssetInStore.CreateCbtText(tUseCompany, selectNode);
        var useCompanyText = AddAssetInStore.GetCbtText();

        selectNode = tRegion.tree('getSelected');
        if (!selectNode || selectNode.parentId == "00000000-0000-0000-0000-000000000000") {
            $('#cbtRegion').combotree('setValue', "");
            return false;
        }
        var regionId = selectNode.id;
        AddAssetInStore.CreateCbtText(tRegion, selectNode);
        var regionText = AddAssetInStore.GetCbtText();

        var ownedCompanyId = $('#cbbOwnedCompany').combobox('getValue');
        var ownedCompanyText = $('#cbbOwnedCompany').combobox('getText');

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
        var pictureId = $.trim($('#hPictureId').val());
        var remark = $.trim($('#txtRemark').val());

        var sData = '{"Id":"' + $('#hId').val() + '","Barcode":"' + barcode + '","CategoryId":"' + categoryId + '","CategoryText":"' + categoryText + '","Named":"' + name + '","SpecModel":"' + specModel + '","SNCode":"' + sSNCode + '","Unit":"' + unit + '","Price":"' + price + '","UseCompanyId":"' + useCompanyId + '","UseCompanyText":"' + useCompanyText + '","SBuyDate":"' + buyDate + '","UsePerson":"' + usePerson + '","Manager":"' + manager + '","OwnedCompanyId":"' + ownedCompanyId + '","OwnedCompanyText":"' + ownedCompanyText + '","RegionId":"' + regionId + '","RegionText":"' + regionText + '","StoreLocation":"' + storeLocation + '","UseExpireMonth":"' + useExpireMonth + '","Supplier":"' + supplier + '","PictureId":"' + pictureId + '","Remark":"' + remark + '"}';
        $.ajax({
            url: "" + All.ServerUrl() + "/Services/PdaService.svc/SaveAssetInStore",
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
    }
}