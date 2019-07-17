<%@ Page Title="资产领用退库列表" Language="C#" MasterPageFile="~/Users/Users.Master" AutoEventWireup="true" CodeBehind="ListUseRefund.aspx.cs" Inherits="TygaSoft.Web.Users.Asset.OutStore.ListUseRefund" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="dlgListUseRefund" class="easyui-panel" data-options="fit:true,border:false">

        <div id="toolbar" style="padding: 5px;">
            <a id="abtnAdd" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'">新建</a>
            <a id="abtnEdit" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'">编辑</a>
            <a id="abtnDel" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'">删除</a>
            <a id="abtnRefund" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'">退库</a>
            <a id="abtnPrint" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-print'">打印</a>
        </div>

        <table id="dgT" class="easyui-datagrid" title="" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:false,toolbar:'#toolbar'">
            <thead>
                <tr>
                    <th data-options="field:'UseRefundId',checkbox:true"></th>
                    <th data-options="field:'AssetId',hidden:true"></th>
                    <th data-options="field:'UseUserName',hidden:true"></th>
                    <th data-options="field:'RefundDealUserName',hidden:true"></th>
                    <th data-options="field:'Remark',hidden:true"></th>
                    <th data-options="field:'PictureUrl',width:50,formatter:ListUseRefund.FPicture">照片</th>
                    <th data-options="field:'SUseTime',width:100">领用时间</th>
                    <th data-options="field:'UsePerson',width:100">领用人</th>
                    <th data-options="field:'SEstimateRefundTime',width:100">预计退库时间</th>
                    <th data-options="field:'Status',width:100">状态</th>
                    <th data-options="field:'SRealRefundTime',width:100">退库时间</th>
                    <th data-options="field:'Barcode',width:120">资产条码</th>
                    <th data-options="field:'CategoryName',width:150">资产类别</th>
                    <th data-options="field:'AssetName',width:150">资产名称</th>
                    <th data-options="field:'SpecModel',width:100">规格型号</th>
                    <th data-options="field:'SNCode',width:100">SN号</th>
                    <th data-options="field:'Price',width:100">金额</th>
                </tr>
            </thead>
        </table>

        <input type="hidden" name="hAction" />
    </div>

    <div id="dlgAddUseRefund" style="padding:10px;"></div>

    <script type="text/javascript" src="/Asset/Scripts/Users/Asset/OutStore/ListUseRefund.js"></script>

    <script type="text/javascript">
        $(function () {
            ListUseRefund.Init();
        })
    </script>

</asp:Content>
