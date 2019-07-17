<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUserDataPermission.aspx.cs" Inherits="TygaSoft.Web.Users.Sys.AddUserDataPermission" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>用户数据权限分配</title>
</head>
<body>
    <form id="dlgUserDataPermissionFm" runat="server">
        <div class="row mt10">
            <span class="rl">用户：</span>
            <div class="fl">
                <span runat="server" id="lbUserName"></span>
            </div>
        </div>
        <div class="row mt10">
            <span class="rl"><span class="cr">*</span>公司授权：</span>
            <div class="fl">
                <input id="txtCompany" runat="server" clientidmode="Static" class="easyui-textbox mtxt" data-options="prompt:'多选'" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl"><span class="cr">*</span>资产类别授权：</span>
            <div class="fl">
                <input id="txtCategory" runat="server" clientidmode="Static" class="easyui-textbox mtxt" data-options="prompt:'多选'" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl"><span class="cr">*</span>区域授权：</span>
            <div class="fl">
                <input id="txtRegion" runat="server" clientidmode="Static" class="easyui-textbox mtxt" data-options="prompt:'多选'" />
            </div>
        </div>

        <input type="hidden" id="hId" runat="server" clientidmode="Static" />
        
    </form>

    <script type="text/javascript" src="../Scripts/Users/Asset/Pandian/AddPandian.js"></script>
    <script type="text/javascript">
        $(function () {
            AddPandian.Init();
        })
    </script>
</body>
</html>
