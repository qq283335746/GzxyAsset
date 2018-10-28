<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCategory.aspx.cs" Inherits="TygaSoft.Web.Users.Asset.Base.AddCategory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建资产分类</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row">
            <span class="rl">所属：</span>
            <div class="fl">
                <span id="lbParent"></span>
                <input type="hidden" id="hParentId" runat="server" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">编码：</span>
            <div class="fl">
                <input runat="server" id="txtCode" class="easyui-validatebox" data-options="required:true" style="width:530px;" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">名称：</span>
            <div class="fl">
                <input runat="server" id="txtName" class="easyui-validatebox" data-options="required:true" style="width:530px;" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">描述说明：</span>
            <div class="fl">
                <input runat="server" id="txtRemark" class="easyui-validatebox" style="width:530px;" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl">排序：</span>
            <div class="fl">
                <input runat="server" id="txtSort" class="easyui-validatebox" data-options="validType:'int'" style="width:530px;" />
            </div>
        </div>
        <input type="hidden" runat="server" id="hId" />
    </form>

    <script type="text/javascript">
        $(function () {
            var node = $("#treeCt").tree('find', $("#hParentId").val());
            if (node) {
                $("#lbParent").text(node.text);
            }
        })
    </script>
</body>
</html>
