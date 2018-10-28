<%@ Page Title="菜单管理" Language="C#" MasterPageFile="~/Users/Users.Master" AutoEventWireup="true" CodeBehind="ListMenus.aspx.cs" Inherits="TygaSoft.Web.Users.Sys.ListMenus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="server">

    <div class="easyui-panel" title="菜单树" data-options="fit:true">
        <div class="mtb5">
           <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" onclick="ListMenus.Add()">新建</a>
           <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" onclick="ListMenus.Edit()">编辑</a>
           <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" onclick="ListMenus.Del()">删除</a>
        </div>
       <ul id="treeCt"></ul>
       <div id="mmTree" class="easyui-menu" style="width:120px;">
           <div onclick="ListMenus.Add()" data-options="iconCls:'icon-add'">添加</div>
           <div onclick="ListMenus.Edit()" data-options="iconCls:'icon-edit'">编辑</div>
           <div onclick="ListMenus.Del()" data-options="iconCls:'icon-remove'">删除</div>
       </div> 
    </div>

    <div id="dlgMenus" style="width:720px;height:390px;padding:10px;"></div>

    <script type="text/javascript" src="/asset/Scripts/Users/Sys/ListMenus.js"></script>
    <script type="text/javascript">
        $(function () {
            try {
                ListMenus.Init();
            }
            catch (e) {
                $.messager.alert('错误提醒', e.name + ": " + e.message, 'error');
            }
        
        })

    
    </script>

</asp:Content>
