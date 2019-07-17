var ListPandian = {
    Init: function () {
        this.InitEvent();
        this.InitData();
    },
    InitEvent: function () {
        var container = ListPandian.Container;
        container.find("[name=abtnMore]").click(function () {
            ListPandian.OnMore();
            return false;
        })
    },
    InitData: function () {
        this.LoadListPandian(ListPandian.PageIndex, 0);
    },
    Container: $("#dlgListPandian"),
    PageIndex:1,
    LoadListPandian: function (pageIndex, pageSize) {
        var sData = '{"AppKey":"' + Login.AppKey + '","UserName":"' + Login.UserName + '","PageIndex":"' + pageIndex + '","PageSize":"' + pageSize + '"}';
        $.ajax({
            url: "" + All.ServerUrl() + "/Services/PdaService.svc/GetPandianList",
            type: "POST",
            data: '{"model":' + sData + '}',
            contentType: "application/json; charset=utf-8",
            timeout:180000,
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
                ListPandian.SetListPandian(jsonData.rows);

                ListPandian.PageIndex++;
            }
        });
    },
    SetListPandian: function (data) {
        if (data.length < 1 && ListPandian.PageIndex > 1) {
            $.messager.show({
                title: '',
                msg: '没有更多的数据.',
                showType: 'slide',
                timeout: 1000,
                style: {
                    right: '',
                    top: '',
                    bottom: -document.body.scrollTop - document.documentElement.scrollTop
                }
            });
            return false;
        }
        var listPandian = $('#listPandian');
        var liAppend = "";
        $.map(data, function (item) {
            var isExist = false;
            listPandian.find('li>').each(function () {
                if ($.trim($(this).find("input[name=Id]").val()) == $.trim(item.Id)) {
                    isExist = true;
                    return false;
                }
            })
            if (!isExist) {
                var imgSrc = "/asset/Mobile/www/images/c_down_dark.jpg";
                if (item.IsDown) imgSrc = "/asset/Mobile/www/images/c_down_light.jpg";
                liAppend += "<li onclick=\"ListPandian.OnClickRow(this)\"><input type=\"hidden\" name=\"Id\" value=\"" + item.Id + "\" /><img class=\"list-image\" src=\"" + imgSrc + "\" onclick=\"ListPandian.OnDown(this)\" /><div class=\"list-header\">" + item.Name + "</div><div class=\"list-content\"><div class=\"fl\"><span class=\"highlight\">创建日期：</span><span class=\"dark\">" + item.SCreateDate + "</span></div><div class=\"fr\"><span class=\"highlight\">创建人：</span><span class=\"dark\">" + item.CreateUserName + "</span></div><span class=\"clr\"></span></div></li>";
            }
        })
        if (liAppend != "") {
            listPandian.append(liAppend);
        }
    },
    OnDown: function (t) {
        var $_this = $(t);
        var src = $_this.attr("src");
        if (src.indexOf('c_down_dark') > -1) {
            var Id = $_this.prev().val();
            var sData = '{"AppKey":"' + Login.AppKey + '","UserName":"' + Login.UserName + '","Id":"' + Id + '"}';
            $.ajax({
                url: "" + All.ServerUrl() + "/Services/PdaService.svc/SavePandianDown",
                type: "POST",
                data: '{"model":' + sData + '}',
                contentType: "application/json; charset=utf-8",
                timeout: 180000,
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
                    $_this.attr('src', "/asset/Mobile/www/images/c_down_light.jpg");
                }
            });
        }
    },
    OnClickRow: function (t) {
        var $_this = $(t);
        var src = $_this.find('.list-image').attr("src");
        if (src.indexOf('c_down_light') > -1) {
            var Id = $_this.find("[name=Id]").val();
            ListPandian.Container.find('#hPandianId').val(Id);
            Default.OnDialog('dlgListPandianAsset', '/asset/Mobile/www/AssetSite/Pandian/ListPandianAsset.html', '');
        }
        else {
            $.messager.show({
                title: '',
                msg: '请先下载盘点表',
                showType: 'show',
                timeout: 500,
                style: {
                    right: '',
                    bottom: ''
                }
            });
        }
    },
    OnMore: function () {
        ListPandian.LoadListPandian(ListPandian.PageIndex, 0);
    }
}