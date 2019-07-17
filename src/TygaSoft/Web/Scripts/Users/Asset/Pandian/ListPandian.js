var ListPandian = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        $("#abtnAdd").click(function () {
            ListPandian.OnAdd();
        })
        $("#abtnView").click(function () {
            ListPandian.OnView();
        })
        $("#abtnDel").click(function () {
            ListPandian.Del();
        })
    },
    InitData: function () {
        this.LoadDg(1, 10);
    },
    Container: $("#dlgListPandian"),
    LoadDg: function (pageIndex, pageSize) {
        var dg = $("#dgT");
        var sData = '{"PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}';

        $.ajax({
            url: "/asset/Services/AssetService.svc/GetPandianList",
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
                dg.datagrid('loadData', jsonData);

                var pager = dg.datagrid('getPager');
                pager.pagination({
                    onSelectPage: function (pageNumber, pageSize) {
                        ListPandian.LoadDg(pageNumber, pageSize);
                    }
                })

                var footerData = jsonData.footer;
                var lis = $('#toolbar>ul>li');
                lis.eq(0).text("" + footerData[0].TotalAll + " 盘点单 (全部)");
                lis.eq(1).text("" + footerData[0].TotalFinish + " 盘点单 (已完成)");
                lis.eq(2).text("" + footerData[0].TotalNotFinish + " 盘点单 (未完成)");
            }
        });
    },
    FormatName: function (value, row, index) {
        return '<a href="/asset/u/gpandian.html?Id=' + row.Id + '" class="c_blue">' + value + '</a>';
    },
    OnAdd: function () {
        var w = $(window).width();
        if (w > 600) w = 600;
        var h = $(window).height();
        if (h > 520) h = 520;
        else h = h * 0.8;
        $("#dlgAddPandian").dialog({
            title: '新增盘点单信息',
            width: w,
            height: h,
            closed: false,
            href: '/asset/u/ypandian.html',
            modal: true,
            iconCls: 'icon-add',
            buttons: [{
                id: 'btnSave', text: '创建', iconCls: 'icon-save', handler: function () {
                    AddPandian.OnSave();
                }
            }, {
                id: 'btnCancelSave', text: '取消', iconCls: 'icon-cancel', handler: function () {
                    $('#dlgAddPandian').dialog('close');
                }
            }]
        })
        return false;
    },
    OnView: function () {
        var rows = $("#dgT").datagrid('getSelections');
        if (!rows || rows.length != 1) {
            $.messager.alert('错误提示', "请选择一行且仅一行数据再进行操作", 'error');
            return false;
        }

        window.location = '/asset/u/gpandian.html?Id=' + rows[0].Id + '';
    },
    Del: function () {
        var rows = $("#dgT").datagrid('getSelections');
        if (!rows || rows.length < 1) {
            $.messager.alert('错误提示', "请至少选择一行数据再进行操作", 'error');
            return false;
        }
        var itemAppend = "";
        for (var i = 0; i < rows.length; i++) {
            if (i > 0) itemAppend += ",";
            itemAppend += rows[i].Id;
        }
        $.messager.confirm('温馨提醒', '确定要删除吗？', function (r) {
            if (r) {
                $.ajax({
                    url: "/asset/Services/AssetService.svc/DeletePandian",
                    type: "post",
                    data: '{"itemAppend":"' + itemAppend + '"}',
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
                        $.messager.alert('温馨提示', "操作成功", "info", function () {
                            window.location.reload();
                        });
                    }
                });
            }
        });
    }
}