using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using TygaSoft.Model;
using TygaSoft.WcfModel;

namespace TygaSoft.WcfService
{
    [ServiceContract(Namespace = "http://TygaSoft.Services.AssetService")]
    public interface IAsset
    {
        #region 基础数据

        #region 公司企业

        [OperationContract(Name = "GetCompanyList")]
        ResResultModel GetCompanyList(CompanyModel model);

        [OperationContract(Name = "SaveCompany")]
        ResResultModel SaveCompany(CompanyFmModel model);

        [OperationContract(Name = "DeleteCompany")]
        ResResultModel DeleteCompany(string itemAppend);

        [OperationContract(Name = "GetCbbCompany")]
        ResResultModel GetCbbCompany();

        #endregion

        #region 组织机构

        [OperationContract(Name = "GetOrgDepmtList")]
        ResResultModel GetOrgDepmtList(object companyId, object parentId);

        [OperationContract(Name = "GetCbbOrgDepmt")]
        ResResultModel GetCbbOrgDepmt(object companyId);

        [OperationContract(Name = "GetOrgDepmtTree")]
        ResResultModel GetOrgDepmtTree(object companyId);

        [OperationContract(Name = "SaveOrgDepmt")]
        ResResultModel SaveOrgDepmt(OrgDepmtFmModel model);

        [OperationContract(Name = "DeleteOrgDepmt")]
        ResResultModel DeleteOrgDepmt(string itemAppend);

        #endregion

        #region 资产分类

        [OperationContract(Name = "GetCategoryTree")]
        ResResultModel GetCategoryTree();

        [OperationContract(Name = "SaveCategory")]
        ResResultModel SaveCategory(CategoryModel model);

        [OperationContract(Name = "DeleteCategory")]
        ResResultModel DeleteCategory(Guid Id);

        #endregion

        #region 区域

        [OperationContract(Name = "GetRegionTree")]
        ResResultModel GetRegionTree();

        [OperationContract(Name = "SaveRegion")]
        ResResultModel SaveRegion(RegionModel model);

        [OperationContract(Name = "DeleteRegion")]
        ResResultModel DeleteRegion(Guid Id);

        #endregion

        #endregion

        #region 入库

        [OperationContract(Name = "GetAssetInStore")]
        ResResultModel GetAssetInStore(AssetInStoreModel model);

        [OperationContract(Name = "SaveAssetInStore")]
        ResResultModel SaveAssetInStore(AssetInStoreFmModel model);

        [OperationContract(Name = "DeleteAssetInStore")]
        ResResultModel DeleteAssetInStore(string itemAppend);

        [OperationContract(Name = "IsExistAssetInStore")]
        ResResultModel IsExistAssetInStore(AssetInStoreSearchModel model);

        #endregion

        #region 资产领用退库

        [OperationContract(Name = "GetAssetUseRefundList")]
        ResResultModel GetAssetUseRefundList(AssetUseRefundModel model);

        [OperationContract(Name = "SaveAssetUseRefund")]
        ResResultModel SaveAssetUseRefund(AssetUseRefundFmModel model);

        [OperationContract(Name = "DeleteAssetUseRefund")]
        ResResultModel DeleteAssetUseRefund(string itemAppend);

        [OperationContract(Name = "DeleteUseRefund")]
        ResResultModel DeleteUseRefund(string itemAppend);

        #endregion

        #region 盘点

        [OperationContract(Name = "GetPandianList")]
        ResResultModel GetPandianList(PandianModel model);

        [OperationContract(Name = "DeletePandian")]
        ResResultModel DeletePandian(string itemAppend);

        [OperationContract(Name = "SavePandianAsset")]
        ResResultModel SavePandianAsset(PandianAssetFmModel model);

        [OperationContract(Name = "GetPandianAssetList")]
        ResResultModel GetPandianAssetList(PandianAssetModel model);

        [OperationContract(Name = "DeletePandianAsset")]
        ResResultModel DeletePandianAsset(object pandianId, string itemAppend);

        [OperationContract(Name = "SavePandianAssetResult")]
        ResResultModel SavePandianAssetResult(object pandianId);

        #endregion
    }
}
