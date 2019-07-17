var ListCompanyOrgDepmt = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        var tg = $('#tgCompanyOrgDepmt');
        $('#abtnUnSelectAll').click(function () {
            tg.treegrid('unselectAll');
        })
        $('#abtnAdd').click(function () {
            ListCompanyOrgDepmt.SelectedNode = tg.treegrid('getSelected');
            if (ListCompanyOrgDepmt.SelectedNode) {
                ListCompanyOrgDepmt.OnAddOrgDepmt();
            }
            else {
                ListCompanyOrgDepmt.OnAddCompany();
            }
            
        })
        $('#abtnEdit').click(function () {
            ListCompanyOrgDepmt.SelectedNode = tg.treegrid('getSelected');
            if (!ListCompanyOrgDepmt.SelectedNode) {
                $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
                return false;
            }
            if (ListCompanyOrgDepmt.SelectedNode.IsCompany) {
                ListCompanyOrgDepmt.OnEditCompany();
            }
            else {
                ListCompanyOrgDepmt.OnEditOrgDepmt();
            }
        })
        $('#abtnDel').click(function () {
            ListCompanyOrgDepmt.SelectedNode = tg.treegrid('getSelected');
            if (!ListCompanyOrgDepmt.SelectedNode) {
                $.messager.alert('错误提示', "请选择一行且仅一行数据进行操作", 'error');
                return false;
            }
            var child = tg.treegrid('getChildren', ListCompanyOrgDepmt.SelectedNode.Id);
            if (child && child.length > 0) {
                $.messager.alert('错误提示', "当前结点存在子节点，无法删除", 'error');
                return false;
            }
            if (ListCompanyOrgDepmt.SelectedNode.IsCompany) {
                ListCompanyOrgDepmt.OnDelCompany();
            }
            else {
                ListCompanyOrgDepmt.OnDelOrgDepmt();
            }
        })
    },
    InitData: function () {
        this.LoadTreeGridByCompany(1,10);
    },
    HAction: '',
    IdKey: '',
    SelectedNode:null,
    LoadTreeGridByCompany: function (pageIndex,pageSize) {

        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}';
        $.ajax({
            url: "/asset/Services/AssetService.svc/GetCompanyList",
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
                if (jsonData.total == 0) return;

                var tg = $("#tgCompanyOrgDepmt");
                tg.treegrid('loadData', jsonData);
                var pager = tg.treegrid('getPager');
                pager.pagination({
                    onSelectPage: function (pageNumber, pageSize) {
                        ListCompanyOrgDepmt.LoadTreeGridByCompany(pageNumber, pageSize);
                    }
                })
            }
        });
    },
    OnAddCompany: function () {
        var h = $(window).height();
        if (h > 277) h = 277;
        else h = h * 0.95;
        var w = $(window).width();
        if (w > 720) w = 720;
        else w = w * 0.95;
        ListCompanyOrgDepmt.HAction = 'OnAdd';
        $("#dlgAddCompanyOrgDepmt").dialog({
            title: '新增公司信息',
            width: w,
            height: h,
            closed: false,
            href: '/asset/u/aorgdepmt.html',
            modal: true,
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddCompanyOrgDepmt.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddCompanyOrgDepmt').dialog('close');
                }
            }]
        })
        return false;
    },
    OnEditCompany: function () {
        var h = $(window).height();
        if (h > 277) h = 277;
        else h = h * 0.95;
        var w = $(window).width();
        if (w > 720) w = 720;
        else w = w * 0.95;
        ListCompanyOrgDepmt.HAction = 'OnEdit';
        $("#dlgAddCompanyOrgDepmt").dialog({
            title: '编辑公司信息',
            width: w,
            height: h,
            closed: false,
            href: '/asset/u/aorgdepmt.html',
            modal: true,
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddCompanyOrgDepmt.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddCompanyOrgDepmt').dialog('close');
                }
            }]
        })
        return false;
    },
    OnAddOrgDepmt: function () {
        var h = $(window).height();
        if (h > 277) h = 277;
        else h = h * 0.95;
        var w = $(window).width();
        if (w > 720) w = 720;
        else w = w * 0.95;
        ListCompanyOrgDepmt.HAction = 'OnAdd';
        $("#dlgAddCompanyOrgDepmt").dialog({
            title: '新增部门信息',
            width: w,
            height: h,
            closed: false,
            href: '/asset/u/yorgdepmt.html',
            modal: true,
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddOrgDepmt.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddCompanyOrgDepmt').dialog('close');
                }
            }]
        })
        return false;
    },
    OnEditOrgDepmt: function () {
        var h = $(window).height();
        if (h > 277) h = 277;
        else h = h * 0.95;
        var w = $(window).width();
        if (w > 720) w = 720;
        else w = w * 0.95;
        ListCompanyOrgDepmt.HAction = 'OnEdit';
        $("#dlgAddCompanyOrgDepmt").dialog({
            title: '编辑部门信息',
            width: w,
            height: h,
            closed: false,
            href: '/asset/u/yorgdepmt.html',
            modal: true,
            buttons: [{
                id: 'btnSave', text: '保存', iconCls: 'icon-save', handler: function () {
                    AddOrgDepmt.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddCompanyOrgDepmt').dialog('close');
                }
            }]
        })
        return false;
    },
    OnDelCompany: function () {
        ListCompanyOrgDepmt.HAction = "OnDel";
        var Id = ListCompanyOrgDepmt.SelectedNode.Id;
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/asset/Services/AssetService.svc/DeleteCompany",
                    type: "post",
                    data: '{"itemAppend":"' + Id + '"}',
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

                        var tg = $('#tgCompanyOrgDepmt');
                        tg.treegrid('remove', Id);
                        jeasyuiFun.show("温馨提示", "操作成功！");
                    }
                });
            }
        });
    },
    OnDelOrgDepmt: function () {
        ListCompanyOrgDepmt.HAction = "OnDel";
        var Id = ListCompanyOrgDepmt.SelectedNode.Id;
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/asset/Services/AssetService.svc/DeleteOrgDepmt",
                    type: "post",
                    data: '{"itemAppend":"' + Id + '"}',
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

                        var tg = $('#tgCompanyOrgDepmt');
                        tg.treegrid('remove', Id);
                        jeasyuiFun.show("温馨提示", "操作成功！");
                    }
                });
            }
        });
    },
    RefreshNode: function (data) {
        var tg = $('#tgCompanyOrgDepmt');
        if (ListCompanyOrgDepmt.SelectedNode) {
            tg.treegrid('update', {
                id: ListCompanyOrgDepmt.SelectedNode.Id,
                row: {
                    Address: data.Address,
                    Phone: data.Phone,
                    TelPhone: data.TelPhone,
                    Sort: data.Sort,
                    Remark: data.Remark,
                    Named: data.Named,
                    Coded: data.Coded
                }
            });
        }
        else {
            ListCompanyOrgDepmt.LoadTreeGridByCompany(1, 10);
        }
    },
    RefreshDepmtNode: function (sData) {
        var tg = $('#tgCompanyOrgDepmt');
        if (ListCompanyOrgDepmt.SelectedNode) {
            if (ListCompanyOrgDepmt.HAction == "OnEdit") {
                var jsonData = eval("(" + sData + ")");
                tg.treegrid('update', {
                    id: ListCompanyOrgDepmt.SelectedNode.Id,
                    row: {
                        Sort: jsonData.Sort,
                        Remark: jsonData.Remark,
                        Named: jsonData.Named,
                        Coded: jsonData.Coded
                    }
                });
            }
            else {
                tg.treegrid('append', {
                    parent: ListCompanyOrgDepmt.SelectedNode.Id,
                    data: eval("([" + sData + "])")
                });
            }
        }
    },
    OnClickRow: function (row) {
        ListCompanyOrgDepmt.LoadTreeGridByDepmt(row);
    },
    LoadTreeGridByDepmt: function (row) {
        var tg = $('#tgCompanyOrgDepmt');
        var child = tg.treegrid('getChildren', row.Id);
        if (child && child.length > 0) return false;

        var companyId = row.CompanyId;
        if (row.IsCompany) companyId = row.Id;
        var parentId = row.ParentId == undefined ? "" : row.ParentId;
        var sData = '{"companyId":"' + companyId + '","parentId":"' + parentId + '"}';
        //console.log(sData);

        $.ajax({
            url: "/asset/Services/AssetService.svc/GetOrgDepmtList",
            type: "POST",
            data: sData,
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
                //console.log("result.Data--" + result.Data);
                var jsonData = eval("(" + result.Data + ")");
                if (jsonData.total == 0) return;

                var isExist = false;
                $.map(jsonData.rows, function (item) {
                    if (item.Id == row.Id) {
                        isExist = true;
                        return false;
                    }
                })
                if (!isExist) {
                    tg.treegrid('append', {
                        parent: row.Id,
                        data: jsonData.rows
                    });
                    tg.treegrid('expand', row.Id);
                }
            }
        });
    }
}