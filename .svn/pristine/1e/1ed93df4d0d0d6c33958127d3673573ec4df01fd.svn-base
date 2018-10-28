<%@ Page Title="资产入库" Language="C#" MasterPageFile="~/Users/Users.Master" AutoEventWireup="true" CodeBehind="ListAssetInStore.aspx.cs" Inherits="TygaSoft.Web.Users.Asset.InStore.ListAssetInStore" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="toolbar" style="padding:5px;">
        <a id="abtnAdd" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'">新建</a>
        <a id="abtnEdit" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'">编辑</a>
        <a id="abtnDel" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'">删除</a>
        <a class="easyui-menubutton" data-options="menu:'#mmExcel',iconCls:'icon-edit'">导入/导出</a>
    </div>
    <div id="mmExcel" style="width:150px;">
        <div><a href="../Files/Template/资产导入模板.xlsx">下载导入模板</a></div>
        <div onclick="ListAssetInStore.OnImport()">批量导入资产</div>
        <div onclick="ListAssetInStore.OnExport()">导出</div>
    </div>

    <table id="dgT" class="easyui-datagrid" title="资产入库列表" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:false,toolbar:'#toolbar'">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'PictureUrl',width:50,formatter:ListAssetInStore.FPicture">照片</th>
                <th data-options="field:'Barcode',width:120">资产条码</th>
                <th data-options="field:'Named',width:200">资产名称</th>
                <th data-options="field:'CategoryName',width:100">资产类别</th>
                <th data-options="field:'SpecModel',width:100">规格型号</th>
                <th data-options="field:'SNCode',width:120">SN号</th>
                <th data-options="field:'Unit',width:100">计量单位</th>
                <th data-options="field:'Price',width:100">金额</th>
                <th data-options="field:'UseCompanyName',width:150">使用公司</th>
                <th data-options="field:'UseDepmtName',width:150">使用部门</th>
                <th data-options="field:'UsePerson',width:100">使用人</th>
                <th data-options="field:'RegionName',width:100">区域</th>
                <th data-options="field:'StoreLocation',width:200">存放地点</th>
                <th data-options="field:'Manager',width:100">管理员</th>
                <th data-options="field:'OwnedCompanyName',width:150">所属公司</th>
                <th data-options="field:'SBuyDate',width:100">购入时间</th>
                <th data-options="field:'Supplier',width:150">供应商</th>
                <th data-options="field:'RFID',width:150">RFID</th>
                <th data-options="field:'UseExpireMonth',width:100">使用期限</th>
            </tr>
        </thead>
    </table>

    <div id="dlgAddAssetInStore"></div>
    <div id="dlgUpload" style="padding:10px;"></div>

    <script type="text/javascript" src="/Asset/Scripts/Users/Asset/InStore/ListAssetInStore.js"></script>
    <script type="text/javascript" src="/Asset/Scripts/Users/DlgUpload.js"></script>

    <script type="text/javascript">
        $(function () {
            ListAssetInStore.Init();
        })
    </script>

</asp:Content>
