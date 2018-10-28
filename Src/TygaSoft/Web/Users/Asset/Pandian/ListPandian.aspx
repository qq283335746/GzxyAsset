<%@ Page Title="盘点单" Language="C#" MasterPageFile="~/Users/Users.Master" AutoEventWireup="true" CodeBehind="ListPandian.aspx.cs" Inherits="TygaSoft.Web.Users.Asset.Pandian.ListPandian" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="toolbar" style="padding: 5px;">
        <ul class="h_ul">
            <li style="width:30%;color:#3c8dbc;padding-left:10px;">0 盘点单 (全部)</li>
            <li style="width:30%;color:#3c763d;">0 盘点单 (已完成)</li>
            <li style="width:30%;color:#a94442;">0 盘点单 (未完成)</li>
        </ul>
        <span class="clr"></span>
        <div class="mt10">
            <a id="abtnAdd" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'">新增</a>
            <a id="abtnView" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'">查看</a>
            <a id="abtnAllowUser" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'">分配用户</a>
            <a id="abtnDel" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'">删除</a>
        </div>
    </div>

    <table id="dgT" class="easyui-datagrid" title="" data-options="rownumbers:true,pagination:true,fit:true,fitColumns:true,toolbar:'#toolbar'">
        <thead>
            <tr>
                <th data-options="field:'Id',checkbox:true"></th>
                <th data-options="field:'Named',width:150,formatter:ListPandian.FormatName">盘点单名称</th>
                <th data-options="field:'UserName',width:100">创建人</th>
                <th data-options="field:'SCreateDate',width:100">创建时间</th>
                <th data-options="field:'Status',width:100">状态</th>
            </tr>
        </thead>
    </table>
    <div id="dlgAddPandian"></div>

    <script type="text/javascript" src="/asset/Scripts/Users/Asset/Pandian/ListPandian.js"></script>

    <script type="text/javascript">
        $(function () {
            ListPandian.Init();
        })
    </script>

</asp:Content>
