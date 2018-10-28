<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCompanyOrgDepmt.aspx.cs" Inherits="TygaSoft.Web.Users.Asset.Base.AddCompanyOrgDepmt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新增公司信息</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="layout-h">
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl"><span class="cr">*</span>公司代码：</span>
                    <div class="fl">
                        <input name="Coded" class="easyui-validatebox txt" data-options="required:true" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl"><span class="cr">*</span>公司名称：</span>
                    <div class="fl">
                        <input name="Named" class="easyui-validatebox txt" data-options="required:true" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">移动电话：</span>
                    <div class="fl">
                        <input name="Phone" class="easyui-validatebox txt" data-options="validType:'mobilePhone'" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">固定电话：</span>
                    <div class="fl">
                        <input name="TelPhone" class="easyui-validatebox txt" data-options="validType:'telPhone'" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">地址：</span>
                    <div class="fl">
                        <input name="Address" class="easyui-validatebox txt" data-options="" />
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

        <input type="hidden" name="Id" />

    </form>
    <script type="text/javascript" src="/asset/Scripts/Users/Asset/Base/AddCompanyOrgDepmt.js"></script>
    <script type="text/javascript">
        $(function () {
            AddCompanyOrgDepmt.Init();
        })
    </script>
</body>
</html>
