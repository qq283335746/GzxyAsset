<%@ Page Title="盘点结果" Language="C#" MasterPageFile="~/Users/Users.Master" AutoEventWireup="true" CodeBehind="ListPandianAsset.aspx.cs" Inherits="TygaSoft.Web.Users.Asset.Pandian.ListPandianAsset" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="toolbar" style="padding: 5px;">
        <ul class="h_ul">
            <li style="width:30%;color:#3c8dbc;">已盘（0 ）</li>
            <li style="width:30%;color:#3c763d;">盘盈（0）</li>
            <li style="width:30%;color:#a94442;">未盘（0）</li>
        </ul>
        <span class="clr"></span>
        <a name="abtnDel" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'">删除</a>
        <a name="abtnSave" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'">提交盘点结果</a>
    </div>

    <table id="dgT" class="easyui-datagrid" title="" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:false,toolbar:'#toolbar',pageSize:50">
        <thead>
            <tr>
                <th data-options="field:'AssetId',checkbox:true"></th>
                <th data-options="field:'Status',width:100">盘点状态</th>
                <th data-options="field:'PictureUrl',width:100">图片</th>
                <th data-options="field:'Category',width:100">固定资产类别</th>
                <th data-options="field:'Barcode',width:120">条形码</th>
                <th data-options="field:'AssetName',width:130">固定资产名称</th>
                <th data-options="field:'SpecModel',width:100">规格型号</th>
                <th data-options="field:'Unit',width:100">计量单位</th>
                <th data-options="field:'Region',width:100">区域</th>
                <th data-options="field:'StoreLocation',width:100">存放地点</th>
                <th data-options="field:'UseCompany',width:100">使用公司</th>
                <th data-options="field:'UseDepmt',width:100">使用部门</th>
                <th data-options="field:'OwnedCompany',width:100">所属公司</th>
                <th data-options="field:'UsePerson',width:100">使用人</th>
                <th data-options="field:'BookQty',width:100">账面数量</th>
                <th data-options="field:'UpdatedRegion',width:120">修改后区域</th>
                <th data-options="field:'UpdatedStoreLocation',width:150">修改后存放地点</th>
                <th data-options="field:'UpdatedUseCompany',width:150">修改后使用公司</th>
                <th data-options="field:'UpdatedUseDepmt',width:150">修改后使用部门</th>
                <th data-options="field:'UpdatedUsePerson',width:120">修改后使用人</th>
                <th data-options="field:'UserName',width:100">盘点人</th>
                <th data-options="field:'Remark',width:100">盘点备注</th>
            </tr>
        </thead>
    </table>

    <script type="text/javascript" src="/asset/Scripts/Common.js"></script>
    <script type="text/javascript" src="/asset/Scripts/Users/Asset/Pandian/ListPandianAsset.js"></script>

    <script type="text/javascript">
        $(function () {
            try {
                ListPandianAsset.Init();
            }
            catch (e) {
                $.messager.alert('异常提示', e.name + ": " + e.message, 'error');
            }
        })
    </script>

</asp:Content>
