<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPandian.aspx.cs" Inherits="TygaSoft.Web.Users.Asset.Pandian.AddPandian" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建盘点单</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="row mt10">
            <span class="rl"><span class="cr">*</span>盘点单名称：</span>
            <div class="fl">
                <input name="Named" class="easyui-validatebox mtxt" data-options="required:true" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl"><span class="cr">*</span>分配用户：</span>
            <div class="fl">
                <input id="txtAllowUsers" name="AllowUsers" class="easyui-textbox mtxt" data-options="required:true,prompt:'分配给用户',missingMessage:'请选择用户',invalidMessage:'请选择用户', icons:[{
                    iconCls:'icon-search',    
                    handler: function(e){
                            DlgUsers.OnDlg($('#txtAllowUsers'),$('#hUserIdAppend'),$('#hUserTextAppend'));
                        }
                    },{
                        iconCls:'icon-remove',
		                handler: function(e){
			                $(e.data.target).textbox('clear');
                            $('#hUserIdAppend').val('');
                            $('#hUserTextAppend').val('');
		                }
                    }]" />

            </div>
        </div>
        <div class="row mt10">
            <span class="rl">备注：</span>
            <div class="fl">
                <input name="Remark" class="mtxt" />
            </div>
        </div>
        <div class="row mt10">
            <span class="rl" style="width:30px;">&nbsp;</span>
            <div class="fl" style="width:520px;">
                <div id="tabAddPandian" class="easyui-tabs" style="margin-top:10px;">
                    <div title="盘点范围" style="padding:20px;">
                        <div class="row">
                            <span class="rl">购入日期：</span>
                            <div class="fl">
                                <input id="txtBuyStartDate" name="BuyStartDate" data-options="editable:false" />
                            </div>
                            <span class="fl">--</span>
                            <div class="fl">
                                <input id="txtBuyEndDate" name="BuyEndDate" data-options="editable:false" />
                            </div>
                        </div>
                        <div class="row mt10">
                            <span class="rl">使用公司：</span>
                            <div class="fl">
                                <input id="cbbUseCompany" class="mtxt" data-options="editable:false" />
                            </div>
                        </div>
                        <div class="row mt10">
                            <span class="rl">使用部门：</span>
                            <div class="fl">
                                <input id="cbtUseDepmt" class="mtxt" data-options="editable:false,readonly:true" />
                            </div>
                        </div>
                        <div class="row mt10">
                            <span class="rl">所属公司：</span>
                            <div class="fl">
                                <input id="cbbOwnedCompany" class="mtxt" data-options="editable:false" />
                            </div>
                        </div>
                        <div class="row mt10">
                            <span class="rl">资产分类：</span>
                            <div class="fl">
                                <input id="cbtCategory" class="mtxt" data-options="required:true" />
                            </div>
                        </div>
                        <div class="row mt10">
                            <span class="rl">区域：</span>
                            <div class="fl">
                                <input id="cbtRegion" class="mtxt" />
                            </div>
                        </div>
                        <div class="row mt10">
                            <span class="rl">管理员：</span>
                            <div class="fl">
                                <input id="txtManager" class="easyui-textbox mtxt" data-options="prompt:'多选管理员', icons:[{
                                iconCls:'icon-search',    
                                handler: function(e){
                                        DlgUsers.OnDlg($('#txtManager'),$('#hManagerIdAppend'),$('#hManagerTextAppend'));
                                    }
                                },{
                                    iconCls:'icon-remove',
		                            handler: function(e){
			                            $(e.data.target).textbox('clear');
                                        $('#hManagerIdAppend').val('');
                                        $('#hManagerTextAppend').val('');
		                            }
                                }]" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <input type="hidden" id="hId" />
        <input type="hidden" id="hUseCompanyId" />
        <input type="hidden" id="hUseDepmtId" />
        <input type="hidden" id="hCategoryIdAppend" />
        <input type="hidden" id="hCategoryTextAppend" />
        <input type="hidden" id="hUseCompanyIdAppend" />
        <input type="hidden" id="hUseCompanyTextAppend" />
        <input type="hidden" id="hUseDepmtAppend" />
        <input type="hidden" id="hUseDepmtTextAppend" />
        <input type="hidden" id="hOwnedCompanyIdAppend" />
        <input type="hidden" id="hOwnedCompanyTextAppend" />
        <input type="hidden" id="hRegionIdAppend" />
        <input type="hidden" id="hRegionTextAppend" />
        <input type="hidden" id="hUserIdAppend" />
        <input type="hidden" id="hUserTextAppend" />
        <input type="hidden" id="hManagerIdAppend" />
        <input type="hidden" id="hManagerTextAppend" />
        <input type="hidden" id="hIsConfirm" value="false" />
        
    </form>

    <script type="text/javascript" src="/asset/Scripts/Users/Asset/Pandian/AddPandian.js"></script>
    <script type="text/javascript" src="/asset/Scripts/Users/Sys/DlgUsers.js"></script>
    <script type="text/javascript">
        $(function () {
            AddPandian.Init();
        })
    </script>
</body>
</html>
