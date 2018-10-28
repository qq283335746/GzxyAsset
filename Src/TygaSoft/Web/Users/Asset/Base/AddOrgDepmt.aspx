<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddOrgDepmt.aspx.cs" Inherits="TygaSoft.Web.Users.Asset.Base.AddOrgDepmt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建/编辑组织机构</title>
</head>
<body>
    <form id="dlgOrgDepmtFm" runat="server">
        <div class="layout-h">
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl"><span class="cr">*</span>部门代码：</span>
                    <div class="fl">
                        <input name="Coded" class="easyui-validatebox txt" data-options="required:true" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl"><span class="cr">*</span>部门名称：</span>
                    <div class="fl">
                        <input name="Named" class="easyui-validatebox txt" data-options="required:true" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">排序：</span>
                    <div class="fl">
                        <input name="Sort" class="easyui-validatebox txt" data-options="validType:'int'" />
                    </div>
                </div>
            </div>
            <span class="clr"></span>
            <div class="row mt10">
                <span class="rl">说明：</span>
                <div class="fl">
                    <input name="Remark" class="mtxt" />
                </div>
            </div>
        </div>

        <input type="hidden" name="CompanyId" />
        <input type="hidden" name="Id" />

    </form>
    <script type="text/javascript" src="/asset/Scripts/Users/Asset/Base/AddOrgDepmt.js"></script>
    <script type="text/javascript">
        $(function () {
            AddOrgDepmt.Init();
        })
    </script>
</body>
</html>
