<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddAssetInStore.aspx.cs" Inherits="TygaSoft.Web.Users.Asset.InStore.AddAssetInStore" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>新建/编辑资产信息</title>
</head>
<body>
    <form id="dlgFm" runat="server">
        <div class="layout-h">
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl"><span class="cr">*</span> 资产条码：</span>
                    <div class="fl">
                        <input id="txtBarcode" runat="server" clientidmode="Static" class="easyui-validatebox txt form-readonly" data-options="readonly:true" placeholder="系统自动生成" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl"><span class="cr">*</span>资产类别：</span>
                    <div class="fl">
                        <input id="cbtCategory" class="txt" data-options="required:true,prompt:'请选择资产类别'" style="width:210px;" />
                    </div>
                    <span class="clr"></span>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl"><span class="cr">*</span>资产名称：</span>
                    <div class="fl">
                        <input id="txtName" runat="server" clientidmode="Static" class="easyui-validatebox txt" data-options="required:true" />
                    </div>
                    <span class="clr"></span>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">规格型号：</span>
                    <div class="fl">
                        <input id="txtSpecModel" runat="server" clientidmode="Static" class="txt" />
                    </div>
                    <span class="clr"></span>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">SN号：</span>
                    <div class="fl">
                        <input id="txtSNCode" runat="server" clientidmode="Static" class="txt" />
                    </div>
                    <span class="clr"></span>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">计量单位：</span>
                    <div class="fl">
                        <input id="txtUnit" runat="server" clientidmode="Static" class="txt" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">金额：</span>
                    <div class="fl">
                        <input id="txtPrice" runat="server" clientidmode="Static" class="easyui-validatebox txt" data-options="validType:'float'" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl"><span class="cr">*</span>使用公司：</span>
                    <div class="fl">
                        <input id="cbbUseCompany" class="txt" data-options="required:true,editable:false,prompt:'请选择使用公司'" style="width:210px;" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">使用部门：</span>
                    <div class="fl">
                        <input id="cbtUseDepmt" class="txt" data-options="editable:false,readonly:true,prompt:'请选择使用部门'" style="width:210px;" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl"><span class="cr">*</span>购入时间：</span>
                    <div class="fl">
                        <input id="txtBuyDate" runat="server" clientidmode="Static" class="easyui-datebox txt" data-options="editable:false" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">使用人：</span>
                    <div class="fl">
                        <input id="txtUsePerson" runat="server" clientidmode="Static" class="txt" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">管理员：</span>
                    <div class="fl">
                        <input id="txtManager" runat="server" clientidmode="Static" class="txt" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl"><span class="cr">*</span>所属公司：</span>
                    <div class="fl">
                        <input id="cbbOwnedCompany" class="txt" data-options="required:true,editable:false,prompt:'请选择所属公司'" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl"><span class="cr">*</span>区域：</span>
                    <div class="fl">
                        <input id="cbtRegion" runat="server" clientidmode="Static" class="txt" data-options="required:true,editable:false,prompt:'请选择区域'" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl"><span class="cr">*</span>存放地点：</span>
                    <div class="fl">
                        <input id="txtStoreLocation" runat="server" clientidmode="Static" class="easyui-validatebox txt" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">使用期限(月)：</span>
                    <div class="fl">
                        <input id="txtUseExpireMonth" runat="server" clientidmode="Static" class="easyui-validatebox txt" data-options="validType:'int'" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">供应商：</span>
                    <div class="fl">
                        <input id="txtSupplier" runat="server" clientidmode="Static" class="txt" />
                    </div>
                </div>
            </div>
            <div class="fl-h">
                <div class="row mt10">
                    <span class="rl">RFID：</span>
                    <div class="fl">
                        <input id="txtRFID" runat="server" clientidmode="Static" class="txt" />
                    </div>
                </div>
            </div>
            <span class="clr"></span>
            <div class="row mt10">
                <span class="rl">备注：</span>
                <div class="fl">
                    <input id="txtRemark" runat="server" clientidmode="Static" class="mtxt" />
                </div>
            </div>
            <div class="row mt10">
                <span class="rl">照片：</span>
                <div class="fl">
                    <div class="easyui-panel">
                        <img id="imgPicture" runat="server" clientidmode="Static" src="~/Images/nopicture.jpg" style="width:150px;height:150px; cursor:pointer;" />
                        <input type="hidden" id="hPictureId" runat="server" clientidmode="Static" />
                    </div>
                </div>
            </div>
        </div>

        <input type="hidden" id="hId" runat="server" clientidmode="Static" />
        <input type="hidden" id="hCategoryId" runat="server" clientidmode="Static" />
        <input type="hidden" id="hUseCompanyId" runat="server" clientidmode="Static" />
        <input type="hidden" id="hUseDepmtId" runat="server" clientidmode="Static" />
        <input type="hidden" id="hOwnedCompanyId" runat="server" clientidmode="Static" />
        <input type="hidden" id="hRegionId" runat="server" clientidmode="Static" />
        
    </form>

    <div id="dlgUploadPicture" style="padding:10px;"></div>
    <div id="dlgSingleSelectPicture" class="easyui-dialog" title="选择图片（单选）" data-options="closed:true,modal:true,href:'/t/pictureselect.html?dlgId=dlgSingleSelectPicture&funName=PictureAssetInStore',width:810,height:$(window).height()*0.8,
    buttons: [{
        id:'btnSingleSelectPicture',text:'确定',iconCls:'icon-ok',
        handler:function(){
            DlgPictureSelect.SetSinglePicture('imgPicture');
            $('#dlgSingleSelectPicture').dialog('close');
        }
    },{
        id:'btnCancelSingleSelectPicture', text:'取消',iconCls:'icon-cancel',
        handler:function(){
            $('#dlgSingleSelectPicture').dialog('close');
        }
    }],
    toolbar:[{
        id:'dlgSingleSelectPictureToolbarUpload',text:'上传',iconCls:'icon-add',
		handler:function(){
            DlgPictureSelect.DlgUpload();
        }
	}]" style="padding:10px;"></div>

    <script type="text/javascript" src="../Scripts/Users/Asset/InStore/AddAssetInStore.js"></script>
    <script type="text/javascript" src="../Scripts/Users/DlgPictureSelect.js"></script>
    <script type="text/javascript">
        $(function () {
            AddAssetInStore.Init();
        })
    </script>
</body>
</html>
