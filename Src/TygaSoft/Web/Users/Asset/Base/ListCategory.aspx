<%@ Page Title="资产分类管理" Language="C#" MasterPageFile="~/Users/Users.Master" AutoEventWireup="true" CodeBehind="ListCategory.aspx.cs" Inherits="TygaSoft.Web.Users.Asset.Base.ListCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div class="easyui-panel" title="资产分类" data-options="fit:true">
        <div class="mtb5">
           <a name="abtnAdd" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true">新建</a>
           <a name="abtnEdit" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true">编辑</a>
           <a name="abtnDel" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true">删除</a>
        </div>
       <ul id="treeCt"></ul>
       <div id="mmTree" class="easyui-menu" style="width:120px;">
           <div onclick="ListRegion.Add()" data-options="iconCls:'icon-add'">添加</div>
           <div onclick="ListRegion.Edit()" data-options="iconCls:'icon-edit'">编辑</div>
           <div onclick="ListRegion.Del()" data-options="iconCls:'icon-remove'">删除</div>
       </div> 
    </div>

    <div id="dlgCategory" style="width:720px;height:390px;padding:10px;"></div>

    <script type="text/javascript" src="../Scripts/Users/Asset/Base/ListCategory.js"></script>
    <script type="text/javascript">
        $(function () {
            try {
                ListCategory.Init();
            }
            catch (e) {
                $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
            }
        
        })

    
    </script>

</asp:Content>
