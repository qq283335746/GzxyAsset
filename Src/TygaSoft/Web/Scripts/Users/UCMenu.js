var UCMenu = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {

    },
    InitData: function () {
        this.InitForm();
        this.GetMenuByParentName(Common.EnumMenuName.UCMenuParentName);
    },
    InitForm:function(){
    
    },
    GetMenuByParentName: function (parentName) {
        var acc = $("#accUcMenu");
        var sData = { "parentName": "" + parentName + "" };

        $.ajax({
            url: "/asset/Services/SecurityService.svc/GetMenusChildrenByParentName",
            type: "GET",
            data: sData,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
                //$.messager.progress({ title: '请稍等', msg: '正在执行...' });
            },
            complete: function () {
                //$.messager.progress('close');
            },
            success: function (result) {
                //console.log('GetMenusChildrenByParentName--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return;
                }
                var jData = JSON.parse(result.Data);
                UCMenu.AccInit($('#accUcMenu'), jData);
            }
        });
    },
    AccInit: function (acc, data) {
        var smpMenu = $('#SitePaths>span');
        for (var i in data) {
            var isSelect = i == 0;
            var isHasSelect = smpMenu.filter(':contains("' + data[i].Title + '")').length > 0;
            if (isHasSelect) isSelect = true;
            var treeCtId = 'treeCt' + i + '';
            acc.accordion('add', {
                selected:isSelect,
                title: data[i].Title,
                content: '<ul id="' + treeCtId + '" class="menus" style="margin-top:8px;"></ul>'
            });
        }
    },
    OnAccAdd: function (title, index) {
        var t = $('#treeCt' + index + '');
        t.tree({
            onLoadSuccess: function (node, data) {
                UCMenu.OnTreeLoadSuccess(t, data);
            },
            onClick: function (node) {
                var url = node.attributes.Url;
                if (url && url != '') {
                    window.location = url;
                }
            }
        })
    },
    OnAccSelect: function (title, index) {
        var t = $('#treeCt' + index + '');
        var roots = t.tree('getRoots');
        if (roots && roots.length > 0) return;

        var sData = { "parentName": "" + title + "" };
        $.ajax({
            url: "/asset/Services/SecurityService.svc/GetMenusTreeChildrenByParentName",
            type: "GET",
            data: sData,
            contentType: "application/json; charset=utf-8",
            success: function (result) {
                //console.log('GetMenusTreeChildrenByParentName11--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return;
                }
                var jData = JSON.parse(result.Data);

                t.tree('loadData', jData);
            }
        });
    },
    OnTreeLoadSuccess: function (t, data) {
        var smpMenu = $('#SitePaths>span');
        for (var i in data) {
            var isSelect = smpMenu.filter(':contains("' + data[i].text + '")').length > 0;
            if (isSelect) {
                var node = t.tree('find', data[i].id);
                t.tree('select', node.target);
                return false;
            }
        }
    },
    AppendTreeChildren: function (t, node) {
        if (!node.attributes.HasChild) return;
        var childNode = t.tree('getChildren', node.target);
        if (childNode && childNode.length > 0) return;

        var sData = { "parentName": "" + node.text + "" };
        $.ajax({
            url: "/asset/Services/SecurityService.svc/GetMenusTreeChildrenByParentName",
            type: "GET",
            data: sData,
            contentType: "application/json; charset=utf-8",
            beforeSend: function () {
            },
            complete: function () {
            },
            success: function (result) {
                console.log('GetMenusTreeChildrenByParentName--' + JSON.stringify(result));
                if (result.ResCode != 1000) {
                    $.messager.alert('系统提示', result.Msg, 'info');
                    return;
                }
                var jData = JSON.parse(result.Data);
                t.tree('append', {
                    parent: node.target,
                    data: jData
                });
            }
        });
    }
}