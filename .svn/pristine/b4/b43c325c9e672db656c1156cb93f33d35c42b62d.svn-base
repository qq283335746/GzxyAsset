var AddPanYing = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        var container = AddPanYing.Container;
        this.InitForm();
        AddPanYing.CbtCategory("cbtCategory", $.trim(container.find("[name=hCategoryId]").val()));
        AddPanYing.CbbCompany("cbbOwnedCompany", $.trim(container.find("[name=hOwnedCompanyId]").val()),false);
        AddPanYing.CbbCompany("cbbUseCompany", $.trim(container.find("[name=hUseCompanyId]").val()),true);
        AddPanYing.CbtOrgDepmt($.trim(container.find("[name=hUseCompanyId]").val()), "cbtUseDepmt", $.trim(container.find("#hUseDepmtId").val()));
        AddPanYing.CbtRegion("cbtRegion", $.trim(container.find("[name=hRegionId]").val()));
    },
    Container:$('#dlgAddPanYing'),
    InitForm: function () {
        var container = AddPanYing.Container;
        var pcontainer = ListPandianAsset.Container;
        if (ListPandianAsset.PanYingBarcode != "") {
            var li;
            pcontainer.find('#listPanYing').find('li').each(function () {
                var currLi = $(this);
                if ($.trim(currLi.find('[code=Barcode]').text()) == ListPandianAsset.PanYingBarcode) {
                    li = currLi;
                    return false;
                }
            })
            if (!li || li.length == 0) {
                //$.messager.alert('提示', "当前盘点单中不存在条形码“" + ListPandianAsset.PanYingBarcode + "”", 'error');
                AddPanYing.SetPanYingBarcode();
                return false;
            }
            var sData = $.trim(li.find('[code=lbSureItem]').text());
            var jsonData = eval("(" + sData + ")");
            container.find('#lbBarcode').text(jsonData.Barcode);
            container.find('#txtAssetName').val(jsonData.AssetName);
            container.find('#txtSpecModel').val(jsonData.SpecModel);
            container.find('#txtUnit').val(jsonData.Unit);
            container.find('#txtStoreLocation').val(jsonData.StoreLocation);
            container.find('#txtUsePerson').val(jsonData.UsePerson);
            container.find('#txtRemark').val(jsonData.Remark);
            container.find('[name=hCategoryId]').val(jsonData.Category);
            container.find('[name=hOwnedCompanyId]').val(jsonData.OwnedCompany);
            container.find('[name=hUseCompanyId]').val(jsonData.UseCompany);
            container.find('[name=hUseDepmtId]').val(jsonData.UseDepmt);
            container.find('[name=hRegionId]').val(jsonData.Region);
        }
        else {
            AddPanYing.SetPanYingBarcode();
        }
    },
    CbtCategory: function (cbtId, v) {
        var container = AddPanYing.Container;
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
                var cbt = container.find('#' + cbtId + '');
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
    CbbCompany: function (cbbId, v, isOnSelect) {
        var container = AddPanYing.Container;
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
                    },
                    onSelect: function (record) {
                        if (isOnSelect) AddPanYing.CbtOrgDepmt(record.Id, "cbtUseDepmt", $.trim($("#hUseDepmtId").val()));
                    }
                });
            }
        });
    },
    CbtOrgDepmt: function (companyId, cbtId, v) {
        var container = AddPanYing.Container;
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
                        }
                    }
                });
            }
        });
    },
    CbtRegion: function (cbtId, v) {
        var container = AddPanYing.Container;
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
                    }
                });
            }
        });
    },
    SetPanYingBarcode: function () {
        $.ajax({
            url: "" + All.ServerUrl() + "/Services/PdaService.svc/GetPanYingBarcode",
            type: "GET",
            data: '',
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
                AddPanYing.Container.find('#lbBarcode').text(result.Data);
            }
        });
    },
    OnSure: function () {
        var container = this.Container;
        var isValid = container.find('#dlgAddPanYingFm').form('validate');
        if (!isValid) return false;
        var barcode = $.trim(container.find('#lbBarcode').text());
        var sAssetName = $.trim(container.find('#txtAssetName').textbox('getValue'));
        var sSpecModel = $.trim(container.find('#txtSpecModel').textbox('getValue'));
        var sUnit = $.trim(container.find('#txtUnit').textbox('getValue'));
        var sCategory = container.find('#cbtCategory').combotree('getValue').replace('请选择', '');
        var sOwnedCompany = container.find('#cbbOwnedCompany').combobox('getValue').replace('请选择', '');
        var sRegion = container.find('#cbtRegion').combotree('getValue').replace('请选择', '');
        var sStoreLocation = container.find('#txtStoreLocation').textbox('getValue');
        var sUseCompany = container.find('#cbbUseCompany').combobox('getValue').replace('请选择', '');
        var sUseDepmt = container.find('#cbtUseDepmt').combotree('getValue').replace('请选择', '');
        var sUsePerson = container.find('#txtUsePerson').textbox('getValue');
        var sRemark = container.find('#txtRemark').val();
        var sAppend = '{"AssetId":"' + ListPandianAsset.PanYingAssetId + '","Status":"盘盈","Barcode":"' + barcode + '","AssetName":"' + sAssetName + '","SpecModel":"' + sSpecModel + '","Unit":"' + sUnit + '","Category":"' + sCategory + '","OwnedCompany":"' + sOwnedCompany + '","Region":"' + sRegion + '","StoreLocation":"' + sStoreLocation + '","UseCompany":"' + sUseCompany + '","UseDepmt":"' + sUseDepmt + '","UsePerson":"' + sUsePerson + '","Remark":"' + sRemark + '"}';
        var listPanel = $("#listPanYing");

        if (ListPandianAsset.PanYingBarcode == "") {
            var maxIndex = listPanel.find('li').length + 1;
            var newItem = "<li>" + maxIndex + " - <span code=\"Barcode\">" + barcode + "</span><div class=\"fr\"><a class=\"abtn_r\" onclick=\"ListPandianAsset.OnClickPanyingRow(this)\">盘盈</a></div><span code=\"lbSureItem\" style=\"display:none;\">" + sAppend + "</span>";
            $(newItem).prependTo(listPanel.find("ul:first"));

            $("#lbTotalYpan").text(parseInt($("#lbTotalYpan").text()) + 1);
        }
        else {
            ListPandianAsset.PanYingBarcode = "";
        }
        $('#tabPandianAsset').tabs('select', 2);
        AddPanYing.Container.dialog('close');
    }
}