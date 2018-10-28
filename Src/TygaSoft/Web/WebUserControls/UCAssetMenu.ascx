<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCAssetMenu.ascx.cs" Inherits="TygaSoft.Web.WebUserControls.UCAssetMenu" %>

<div id="menuLeft" class="easyui-accordion" data-options="fit:true">
    <div title="资产管理" data-options="selected:true" style="padding:10px;">
        <ul class="aMenu">
            <li>
                <a href="/Asset/u/tinstore.html">资产入库</a>
            </li>
            <li>
                <a href="/Asset/u/toutstore.html">领用退库</a>
            </li>
            <li>
                <a href="#">借用归还</a>
            </li>
            <li>
                <a href="#">实物信息变更</a>
            </li>
            <li>
                <a href="#">财务信息变更</a>
            </li>
            <li>
                <a href="#">维修信息登记</a>
            </li>
            <li>
                <a href="#">清理报废</a>
            </li>
        </ul>
    </div>
    <div title="盘点管理" style="padding:10px;">
        <ul class="aMenu">
            <li>
                <a href="/asset/u/tpandian.html">盘点单</a>
            </li>
            <li>
                <a href="#">闲时盘</a>
            </li>
        </ul>   
    </div>
    <div title="分析报表" style="padding:10px;">
        <ul class="aMenu">
            <li>
                <a href="#">资产清单</a>
            </li>
            <li>
                <a href="#">月增加对账表</a>
            </li>
            <li>
                <a href="#">到期资产</a>
            </li>
            <li>
                <a href="#">清理清单</a>
            </li>
        </ul>   
    </div>
    <div title="基础数据" style="padding:10px;">
        <ul class="aMenu">
            <li>
                <a href="/asset/u/gorgdepmt.html">组织结构</a>
            </li>
            <li>
                <a href="/asset/u/tcategory.html">资产分类</a>
            </li>
            <li>
                <a href="/asset/u/tregion.html">区域</a>
            </li>
        </ul>   
    </div>
    <div title="系统管理" style="padding:10px;">
        <ul class="aMenu">
            <li>
                <a href="/asset/u/rmember.html">角色管理</a>
            </li>
            <li>
                <a href="/asset/u/umember.html">用户管理</a>
            </li>
            <li>
                <a href="#">标签模板设置</a>
            </li>
            <li>
                <a href="/asset/u/tmenu.html">菜单导航管理</a>
            </li>
        </ul>   
    </div>
</div>