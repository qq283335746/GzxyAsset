var AddCompanyOrgDepmt = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        
    },
    InitData: function () {
        var action = ListCompanyOrgDepmt.HAction;
        switch (action) {
            case "OnEdit":
                setTimeout(function () {
                    AddCompanyOrgDepmt.SetEdit();
                }, 1)
                break;
            default:
                break;
        }
    },
    Container: $("#dlgAddCompanyOrgDepmt"),
    SetEdit: function () {
        var container = AddCompanyOrgDepmt.Container;
        var data = ListCompanyOrgDepmt.SelectedNode;
        container.find('[name=Id]').val(data.Id);
        container.find('[name=Coded]').val(data.Coded);
        container.find('[name=Named]').val(data.Named);
        container.find('[name=Phone]').val(data.Phone);
        container.find('[name=TelPhone]').val(data.TelPhone);
        container.find('[name=Address]').val(data.Address);
        container.find('[name=Sort]').val(data.Sort);
        container.find('[name=Remark]').val(data.Remark);
    },
    OnSave: function () {
        var container = AddCompanyOrgDepmt.Container;
        var isValid = container.find('#dlgFm').form('validate');
        if (!isValid) return false;

        var sCoded = $.trim(container.find('[name=Coded]').val());
        var sNamed = $.trim(container.find('[name=Named]').val());
        var sPhone = $.trim(container.find('[name=Phone]').val());
        var sTelPhone = $.trim(container.find('[name=TelPhone]').val());
        var sAddress = $.trim(container.find('[name=Address]').val());
        var sSort = $.trim(container.find('[name=Sort]').val());
        var sRemark = $.trim(container.find('[name=Remark]').val());
        var Id = container.find('[name=Id]').val();

        if (sSort == "") sSort = 0;

        var sData = '{"Id":"' + Id + '","Coded":"' + sCoded + '","Named":"' + sNamed + '","Address":"' + sAddress + '","Phone":"' + sPhone + '","TelPhone":"' + sTelPhone + '","Sort":"' + sSort + '","Remark":"' + sRemark + '"}';

        $.ajax({
            url: "/asset/Services/AssetService.svc/SaveCompany",
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
                ListCompanyOrgDepmt.RefreshNode(eval("(" + result.Data + ")"));
                AddCompanyOrgDepmt.Container.dialog('close');
                jeasyuiFun.show("温馨提示", "保存成功！");
            }
        });
    }
}