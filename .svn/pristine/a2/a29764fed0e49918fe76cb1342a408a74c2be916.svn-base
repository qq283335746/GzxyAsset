var AddPandian = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        //var container = this.Container;
        //container.find("#btnSave").click(function () {
        //    AddPandian.OnSave();
        //})
    },
    InitData: function () {
        this.InitForm();
        AddPandian.CbtCategory("cbtCategory", $.trim($("#hCategoryId").val()));
        AddPandian.CbbCompany("cbbUseCompany", $.trim($("#hUseCompanyId").val()), true);
        AddPandian.CbbCompany("cbbOwnedCompany", $.trim($("#hOwnedCompanyId").val()), false);
        //AddPandian.CbtOrgDepmt("cbtUseDepmt", $.trim($("#hUseDepmtId").val()));
        AddPandian.CbtOrgDepmt($.trim($("#hUseCompanyId").val()), "cbtUseDepmt", $.trim($("#hUseDepmtId").val()));
        AddPandian.CbtRegion("cbtRegion", $.trim($("#hRegionId").val()));
    },
    Container: $("#dlgAddPandian"),
    InitForm:function(){
        var container = this.Container;
        container.find('#txtBuyStartDate').datebox();
        container.find('#txtBuyEndDate').datebox();
    },
    CbtCategory: function (cbtId, v) {
        $.ajax({
            url: "/asset/Services/AssetService.svc/GetCategoryTree",
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
                        else {
                            cbt.combotree('setValue', "请选择");
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
            url: "/asset/Services/AssetService.svc/GetOrgDepmtTree",
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
        $.ajax({
            url: "/asset/Services/AssetService.svc/GetRegionTree",
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
                        else {
                            cbt.combotree('setValue', jsonData[0].id);
                        }
                    }
                });
            }
        });
    },
    CbbCompany: function (cbbId, v, isOnSelect) {
        $.ajax({
            url: "/asset/Services/AssetService.svc/GetCbbCompany",
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
                            cbb.combobox('setValue', jsonData[0].Id);
                        }
                    },
                    onSelect: function (record) {
                        if (isOnSelect) AddPandian.CbtOrgDepmt(record.Id, "cbtUseDepmt", $.trim($("#hUseDepmtId").val()));
                    }
                });
            }
        });
    },
    OnSave: function () {
        var container = AddPandian.Container;
        var isValid = container.find('#dlgFm').form('validate');
        if (!isValid) return false;
        var named = $.trim(container.find('[name=Named]').val());
        var allowUsers = $.trim(container.find('#hUserIdAppend').val());
        var remark = $.trim(container.find('[name=Remark]').val());
        var buyStartDate = container.find('#txtBuyStartDate').datebox('getValue');
        var buyEndDate = container.find('#txtBuyEndDate').datebox('getValue');
        var useCompany = container.find('#cbbUseCompany').combobox('getValue').replace("请选择","");
        var useDepmt = container.find('#cbtUseDepmt').combotree('getValue').replace("请选择", "");
        var ownedCompany = container.find('#cbbOwnedCompany').combobox('getValue').replace("请选择", "");
        var category = container.find('#cbtCategory').combotree('getValue').replace("请选择", "");
        var region = container.find('#cbtRegion').combotree('getValue').replace("请选择", "");
        var manager = $.trim(container.find('#hManagerIdAppend').val());
        var isConfirm = container.find('#hIsConfirm').val();
        var Id = $.trim($("#hId").val());
        
        var sData = '{"Id":"' + Id + '","Named":"' + named + '","AllowUsers":"' + allowUsers + '","Remark":"' + remark + '","BuyStartDate":"' + buyStartDate + '","BuyEndDate":"' + buyEndDate + '","UseCompany":"' + useCompany + '","UseDepmt":"' + useDepmt + '","OwnedCompany":"' + ownedCompany + '","Category":"' + category + '","Region":"' + region + '","Manager":"' + manager + '","IsConfirm":"' + isConfirm + '"}';

        AddPandian.SaveData(sData);
    },
    SaveData: function (sData) {
        $.ajax({
            url: "/asset/Services/AssetService.svc/SavePandianAsset",
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

                if (result.ResCode == 1002) {
                    $.messager.confirm('系统提示', result.Msg, function (r) {
                        if (r) {
                            $('#hIsConfirm').val('true');
                            AddPandian.OnSave();
                        }
                    });

                    return false;
                }

                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return false;
                }
                
                AddPandian.Container.dialog('close');
                jeasyuiFun.show("温馨提示", "保存成功！");
                setTimeout(function () {
                    window.location.reload();
                }, 1000)
            }
        });
    }
}