<%@ Page Title="公司组织机构" Language="C#" MasterPageFile="~/Users/Users.Master" AutoEventWireup="true" CodeBehind="ListCompanyOrgDepmt.aspx.cs" Inherits="TygaSoft.Web.Users.Asset.Base.ListCompanyOrgDepmt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div id="toolbar" style="padding:5px;">
        <a id="abtnAdd" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'">新建</a>
        <a id="abtnEdit" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'">编辑</a>
        <a id="abtnDel" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'">删除</a>
        <a id="abtnUnSelectAll" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-remove'">清除选中状态</a>
    </div>
    <table id="tgCompanyOrgDepmt" title="公司组织机构" class="easyui-treegrid" data-options="idField: 'Id',treeField: 'Named',animate:true,fit:true,fitColumns:true,rownumbers:true,pagination:true,onClickRow:ListCompanyOrgDepmt.OnClickRow,toolbar:'#toolbar'">
        <thead>
            <tr>
                <th data-options="field:'Address',hidden:true"></th>
                <th data-options="field:'Phone',hidden:true"></th>
                <th data-options="field:'TelPhone',hidden:true"></th>
                <th data-options="field:'Sort',hidden:true"></th>
                <th data-options="field:'Remark',hidden:true"></th>
                <th data-options="field:'IsCompany',hidden:true"></th>
                <th data-options="field:'CompanyId',hidden:true"></th>
                <th data-options="field:'ParentId',hidden:true"></th>
                <th data-options="field:'Named',width:200">名称</th>
                <th data-options="field:'Coded',width:150">编码</th>
            </tr>
        </thead>
    </table>

    <div id="dlgAddCompanyOrgDepmt"></div>

    <script type="text/javascript" src="/asset/Scripts/Users/Asset/Base/ListCompanyOrgDepmt.js"></script>
    <script type="text/javascript">
        $(function () {
            ListCompanyOrgDepmt.Init();
        })
    </script>

</asp:Content>
