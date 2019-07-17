using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using TygaSoft.Model;
using TygaSoft.WcfModel;

namespace TygaSoft.WcfService
{
    [ServiceContract(Namespace = "http://TygaSoft.Services.PdaService")]
    public interface IPda
    {
        #region 基础数据

        #region 组织机构

        [OperationContract(Name = "GetOrgDepmtTree")]
        ResResultModel GetOrgDepmtTree();

        [OperationContract(Name = "GetOrgDepmtTreeByCompanyId")]
        ResResultModel GetOrgDepmtTreeByCompanyId(object companyId);

        [OperationContract(Name = "SaveOrgDepmt")]
        ResResultModel SaveOrgDepmt(OrgDepmtFmModel model);

        [OperationContract(Name = "DeleteOrgDepmt")]
        ResResultModel DeleteOrgDepmt(Guid Id);

        [OperationContract(Name = "GetCbbCompany")]
        ResResultModel GetCbbCompany();

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

        [OperationContract(Name = "GetAssetInStoreModel")]
        ResResultModel GetAssetInStoreModel(object Id);

        [OperationContract(Name = "SaveAssetInStore")]
        ResResultModel SaveAssetInStore(AssetInStoreFmModel model);

        [OperationContract(Name = "DeleteAssetInStore")]
        ResResultModel DeleteAssetInStore(string itemAppend);

        #endregion

        #region 盘点

        [OperationContract(Name = "GetPandianList")]
        ResResultModel GetPandianList(PdaPandianModel model);

        [OperationContract(Name = "SavePandianDown")]
        ResResultModel SavePandianDown(PdaPandianFmModel model);

        [OperationContract(Name = "GetPandianAssetList")]
        ResResultModel GetPandianAssetList(PdaPandianAssetModel model);

        [OperationContract(Name = "GetPanYingBarcode")]
        ResResultModel GetPanYingBarcode();

        [OperationContract(Name = "SavePandianAsset")]
        ResResultModel SavePandianAsset(PdaPandianAssetFmModel model);

        [OperationContract(Name = "GetPandianAssetByBarcode")]
        ResResultModel GetPandianAssetByBarcode(string appKey, string userName, object pandianId, string barcode);

        #endregion
    }
}
