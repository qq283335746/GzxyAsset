<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DlgAssetInStore.aspx.cs" Inherits="TygaSoft.Web.Users.Asset.OutStore.DlgAssetInStore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>资产列表弹出框</title>
</head>
<body>
    <div id="dgDlgAssetInStoreToolbar">

    </div>
    <table id="dgDlgAssetInStore" class="easyui-datagrid"
           data-options="fit:true, fitColumns:true,singleSelect:false,pagination:true,rownumbers:true,border:false,toolbar:'#dgDlgAssetInStoreToolbar'">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'PictureUrl',width:100">照片</th>
                <th data-options="field:'Barcode',width:100">资产条码</th>
                <th data-options="field:'Named',width:100">资产名称</th>
                <th data-options="field:'OwnedCompany',width:100">所属公司</th>
                <th data-options="field:'UseCompany',width:100">当前所在公司</th>
                <th data-options="field:'UseCompanyDepmt',width:100">当前所在部门</th>
                <th data-options="field:'UsePerson',width:100">当前使用人</th>
                <th data-options="field:'StoreLocation',width:100">存放地点</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/Asset/Scripts/Users/Asset/OutStore/DlgAssetInStore.js"></script>
    <script type="text/javascript">
        $(function () {
            DlgAssetInStore.Init();
        })
    </script>
</body>
</html>
