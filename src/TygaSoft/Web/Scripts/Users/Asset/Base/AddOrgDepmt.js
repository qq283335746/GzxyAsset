var AddOrgDepmt = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.SetCompanyId();
        var action = ListCompanyOrgDepmt.HAction;
        switch (action) {
            case "OnEdit":
                setTimeout(function () {
                    AddOrgDepmt.SetEdit();
                }, 1)
                break;
            default:
                break;
        }
    },
    Container: $("#dlgAddCompanyOrgDepmt"),
    SetEdit: function () {
        var container = AddOrgDepmt.Container;
        var data = ListCompanyOrgDepmt.SelectedNode;
        container.find('[name=Id]').val(data.Id);
        container.find('[name=Coded]').val(data.Coded);
        container.find('[name=Named]').val(data.Named);
        container.find('[name=Sort]').val(data.Sort);
        container.find('[name=Remark]').val(data.Remark);
    },
    SetCompanyId: function () {
        var container = AddOrgDepmt.Container;
        if (ListCompanyOrgDepmt.SelectedNode.IsCompany) container.find('[name=CompanyId]').val(ListCompanyOrgDepmt.SelectedNode.Id);
        else container.find('[name=CompanyId]').val(ListCompanyOrgDepmt.SelectedNode.CompanyId);
    },
    IdStep: '',
    CodeStep: '',
    GetIdStep: function (tg, node) {
        if (node) {
            AddOrgDepmt.IdStep += node.Id + ",";
            var pNode = tg.treegrid('getParent', node.Id);
            if (pNode) {
                AddOrgDepmt.GetIdStep(tg, pNode, AddOrgDepmt.IdStep);
            }
        }
    },
    GetCodeStep: function (tg, node) {
        if (node) {
            AddOrgDepmt.CodeStep += node.Coded + ",";
            var pNode = tg.treegrid('getParent', node.Id);
            if (pNode) {
                AddOrgDepmt.GetIdStep(tg, pNode, AddOrgDepmt.CodeStep);
            }
        }
    },
    OnSave: function () {
        var container = AddOrgDepmt.Container;
        var isValid = container.find('#dlgOrgDepmtFm').form('validate');
        if (!isValid) return false;

        var sCoded = $.trim(container.find('[name=Coded]').val());
        var sNamed = $.trim(container.find('[name=Named]').val());
        var sSort = $.trim(container.find('[name=Sort]').val());
        var sRemark = $.trim(container.find('[name=Remark]').val());
        var Id = container.find('[name=Id]').val();
        var sCompanyId = container.find('[name=CompanyId]').val();

        if (sSort == "") sSort = 0;

        var tg = $('#tgCompanyOrgDepmt');
        AddOrgDepmt.GetIdStep(tg, ListCompanyOrgDepmt.SelectedNode);
        var sIdStep = AddOrgDepmt.IdStep;
        AddOrgDepmt.GetCodeStep(tg,ListCompanyOrgDepmt.SelectedNode);
        var sCodeStep = AddOrgDepmt.CodeStep;

        var sData = '{"CompanyId":"' + sCompanyId + '","Id":"' + Id + '","Coded":"' + sCoded + '","Named":"' + sNamed + '","Sort":"' + sSort + '","Remark":"' + sRemark + '","IdStep":"' + sIdStep + '","CodeStep":"' + sCodeStep + '"}';
        $.ajax({
            url: "/asset/Services/AssetService.svc/SaveOrgDepmt",
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

                ListCompanyOrgDepmt.RefreshDepmtNode(result.Data);
                AddOrgDepmt.Container.dialog('close');
                jeasyuiFun.show("温馨提示", "保存成功！");
            }
        });
    }
}