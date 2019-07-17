using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;
using TygaSoft.SysHelper;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.WcfModel;

namespace TygaSoft.WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class PdaService : IPda
    {
        #region 基础数据

        #region 组织机构

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetOrgDepmtTree()
        {
            try
            {
                var bll = new OrgDepmt();
                return ResResult.Response(true, "", bll.GetTreeJson());
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message,"");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrgDepmtTreeByCompanyId(object companyId)
        {
            try
            {
                var gId = Guid.Empty;
                if (companyId != null) Guid.TryParse(companyId.ToString(), out gId);
                var bll = new OrgDepmt();
                return ResResult.Response(true, "", bll.GetTreeByCompany(gId));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveOrgDepmt(OrgDepmtFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, "未获取到任何参数");
                if (string.IsNullOrWhiteSpace(model.Coded) || string.IsNullOrWhiteSpace(model.Named)) return ResResult.Response(false, "编码或名称不能为空字符串");
                var Id = Guid.Empty;
                var parentId = Guid.Empty;
                if (model.Id != null && !string.IsNullOrWhiteSpace(model.Id.ToString())) Guid.TryParse(model.Id.ToString(), out Id);
                if (model.ParentId != null && !string.IsNullOrWhiteSpace(model.ParentId.ToString())) Guid.TryParse(model.ParentId.ToString(), out parentId);

                var bll = new OrgDepmt();
                int effect = 0;

                var modelInfo = new OrgDepmtInfo();
                modelInfo.Id = Id;
                modelInfo.ParentId = parentId;
                modelInfo.Coded = model.Coded;
                modelInfo.Named = model.Named;
                modelInfo.Remark = model.Remark;
                modelInfo.Sort = model.Sort;
                modelInfo.LastUpdatedDate = DateTime.Now;
                modelInfo.UserId = Guid.Parse(SecurityService.GetUserId().ToString());

                if (modelInfo.Id.Equals(Guid.Empty))
                {
                    modelInfo.Id = Guid.NewGuid();
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, "操作失败，数据库操作异常");

                return ResResult.Response(true, "操作成功", modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteOrgDepmt(Guid Id)
        {
            try
            {
                if (Id.Equals(Guid.Empty))
                {
                    return ResResult.Response(false, "参数值无效");
                }
                var bll = new OrgDepmt();
                return ResResult.Response(bll.Delete(Id) > 0, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "");
            }
        }

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetCbbCompany()
        {
            try
            {
                var bll = new Company();
                var list = bll.GetList();
                if (list == null || list.Count() == 0) return ResResult.Response(true, "", "[]");

                var cbbList = new List<ComboboxModel>();
                foreach (var item in list)
                {
                    cbbList.Add(new ComboboxModel { Id = item.Id.ToString(), Text = string.Format("{0}{1}", item.Coded, item.Named) });
                }

                return ResResult.Response(true, "", JsonConvert.SerializeObject(cbbList));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 资产分类

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetCategoryTree()
        {
            try
            {
                var bll = new Category();
                return ResResult.Response(true, "", bll.GetTreeJson());
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveCategory(CategoryModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, "未获取到任何参数");
                if (string.IsNullOrWhiteSpace(model.Coded) || string.IsNullOrWhiteSpace(model.Named)) return ResResult.Response(false, "编码或名称不能为空字符串");
                var Id = Guid.Empty;
                var parentId = Guid.Empty;
                if (model.Id != null && !string.IsNullOrWhiteSpace(model.Id.ToString())) Guid.TryParse(model.Id.ToString(), out Id);
                if (model.ParentId != null && !string.IsNullOrWhiteSpace(model.ParentId.ToString())) Guid.TryParse(model.ParentId.ToString(), out parentId);

                var bll = new Category();
                int effect = 0;

                var modelInfo = new CategoryInfo();
                modelInfo.Id = Id;
                modelInfo.ParentId = parentId;
                modelInfo.Coded = model.Coded;
                modelInfo.Named = model.Named;
                modelInfo.Remark = model.Remark;
                modelInfo.Sort = model.Sort;
                modelInfo.LastUpdatedDate = DateTime.Now;
                modelInfo.UserId = Guid.Parse(SecurityService.GetUserId().ToString());

                if (modelInfo.Id.Equals(Guid.Empty))
                {
                    modelInfo.Id = Guid.NewGuid();
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, "操作失败，数据库操作异常");

                return ResResult.Response(true, "操作成功", modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteCategory(Guid Id)
        {
            try
            {
                if (Id.Equals(Guid.Empty))
                {
                    return ResResult.Response(false, "参数值无效");
                }
                var bll = new Category();
                return ResResult.Response(bll.Delete(Id) > 0, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "");
            }
        }

        #endregion

        #region 区域

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetRegionTree()
        {
            try
            {
                var bll = new Region();
                return ResResult.Response(true, "", bll.GetTreeJson());
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message);
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveRegion(RegionModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, "未获取到任何参数");
                if (string.IsNullOrWhiteSpace(model.Coded) || string.IsNullOrWhiteSpace(model.Named)) return ResResult.Response(false, "编码或名称不能为空字符串");
                var Id = Guid.Empty;
                var parentId = Guid.Empty;
                if (model.Id != null && !string.IsNullOrWhiteSpace(model.Id.ToString())) Guid.TryParse(model.Id.ToString(), out Id);
                if (model.ParentId != null && !string.IsNullOrWhiteSpace(model.ParentId.ToString())) Guid.TryParse(model.ParentId.ToString(), out parentId);

                var bll = new Region();
                int effect = 0;

                var modelInfo = new RegionInfo();
                modelInfo.Id = Id;
                modelInfo.ParentId = parentId;
                modelInfo.Coded = model.Coded;
                modelInfo.Named = model.Named;
                modelInfo.Remark = model.Remark;
                modelInfo.Sort = model.Sort;
                modelInfo.LastUpdatedDate = DateTime.Now;
                modelInfo.UserId = Guid.Parse(SecurityService.GetUserId().ToString());

                if (modelInfo.Id.Equals(Guid.Empty))
                {
                    modelInfo.Id = Guid.NewGuid();
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, "操作失败，数据库操作异常");

                return ResResult.Response(true, "操作成功", modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteRegion(Guid Id)
        {
            try
            {
                if (Id.Equals(Guid.Empty))
                {
                    return ResResult.Response(false, "参数值无效");
                }
                var bll = new Region();
                return ResResult.Response(bll.Delete(Id) > 0, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "");
            }
        }

        #endregion

        #endregion

        #region 入库

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetAssetInStore(AssetInStoreModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;

                var bll = new AssetInStore();
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, "", null);
                if (totalRecord == 0) return ResResult.Response(true, "", "{\"total\":0,\"rows\":[]}");

                var dgData = "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}";
                return ResResult.Response(true, "", dgData);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetAssetInStoreModel(object Id)
        {
            try
            {
                Guid gId = Guid.Empty;
                if (Id != null) Guid.TryParse(Id.ToString(), out gId);

                if (gId.Equals(Guid.Empty)) return ResResult.Response(false, "请求参数值“" + Id + "”不正确", "");

                var bll = new AssetInStore();
                var model = bll.GetModel(gId);
                if (model == null) return ResResult.Response(false, "参数值“" + Id + "”对应数据不存在或已被删除", "");
                return ResResult.Response(true, "调用成功", JsonConvert.SerializeObject(model));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveAssetInStore(AssetInStoreFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, "未获取到任何可保存的数据", "");

                var modelInfo = new AssetInStoreInfo();

                Guid gId = Guid.Empty;
                if (model.CategoryId != null) Guid.TryParse(model.CategoryId.ToString(), out gId);
                modelInfo.CategoryId = gId;
                if (model.UseCompanyId != null) Guid.TryParse(model.UseCompanyId.ToString(), out gId);
                modelInfo.UseCompanyId = gId;
                if (model.UseDepmtId != null) Guid.TryParse(model.UseDepmtId.ToString(), out gId);
                modelInfo.UseDepmtId = gId;
                if (model.RegionId != null) Guid.TryParse(model.RegionId.ToString(), out gId);
                modelInfo.RegionId = gId;
                if (model.OwnedCompanyId != null) Guid.TryParse(model.OwnedCompanyId.ToString(), out gId);
                modelInfo.OwnedCompanyId = gId;
                if (model.PictureId != null) Guid.TryParse(model.PictureId.ToString(), out gId);
                modelInfo.PictureId = gId;

                DateTime time = DateTime.MinValue;
                DateTime.TryParse(model.SBuyDate, out time);

                modelInfo.Barcode = model.Barcode;
                modelInfo.Named = model.Named;
                modelInfo.SpecModel = model.SpecModel;
                modelInfo.SNCode = model.SNCode;
                modelInfo.Unit = model.Unit;
                modelInfo.Price = model.Price;
                modelInfo.BuyDate = time == DateTime.MinValue ? DateTime.Parse("1754-01-01") : time;
                modelInfo.UsePerson = model.UsePerson;
                modelInfo.Manager = model.Manager;
                modelInfo.StoreLocation = model.StoreLocation;
                modelInfo.UseExpireMonth = model.UseExpireMonth;
                modelInfo.Supplier = model.Supplier;
                modelInfo.Remark = model.Remark;
                modelInfo.LastUpdatedDate = DateTime.Now;
                modelInfo.UserId = Guid.Parse(SecurityService.GetUserId().ToString());

                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out gId);
                modelInfo.Id = gId;

                var bll = new AssetInStore();
                int effect = -1;

                if (modelInfo.Id.Equals(Guid.Empty))
                {
                    effect = bll.Insert(modelInfo);
                }
                else
                {
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, "操作失败，数据库操作异常，请稍后再重试", "");

                return ResResult.Response(true, "操作成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "", "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteAssetInStore(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, "未找到任何可删除的数据", "");
                var list = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<object>();
                if (list == null && list.Count == 0)
                {
                    return ResResult.Response(false, "未找到任何可删除的数据", "");
                }
                var bll = new AssetInStore();
                return ResResult.Response(bll.DeleteBatch(list), "操作成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "", "");
            }
        }

        #endregion

        #region 盘点

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPandianList(PdaPandianModel model)
        {
            try
            {
                object userId = null;
                SecurityService.DoCheckLogin(model.AppKey, model.UserName, out userId);

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 10) model.PageSize = 10;
                int totalRecord = 0;

                var sqlWhere = new StringBuilder(100);
                var parms = new ParamsHelper();

                sqlWhere.AppendFormat("and CHARINDEX(AllowUsers,'{0}') > -1 ", userId.ToString());

                if (model.PandianId != null)
                {
                    var pandianId = Guid.Empty;
                    Guid.TryParse(model.PandianId.ToString(), out pandianId);
                    if (!pandianId.Equals(Guid.Empty))
                    {
                        sqlWhere.Append("and Id = @PandianId ");
                        var parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
                        parm.Value = pandianId;
                        parms.Add(parm);
                    }
                }

                var bll = new Pandian();

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere.ToString(), parms.ToArray());
                if (totalRecord == 0) return ResResult.Response(true, "", "{\"total\":0,\"rows\":[]}");

                var pdaList = new List<PdaPandianInfo>();
                foreach (var item in list)
                {
                    pdaList.Add(new PdaPandianInfo { Id = item.Id, Name = item.Named, SCreateDate = item.CreateDate.ToString("yyyy年MM月dd日"), CreateUserName = item.UserName, IsDown = item.IsDown, TotalQty = item.TotalQty });
                }

                var dgData = "{\"total\":" + pdaList.Count + ",\"rows\":" + JsonConvert.SerializeObject(pdaList) + "}";
                return ResResult.Response(true, "", dgData);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SavePandianDown(PdaPandianFmModel model)
        {
            try
            {
                object userId = null;
                SecurityService.DoCheckLogin(model.AppKey, model.UserName, out userId);

                var gId = Guid.Empty;
                if (!Guid.TryParse(model.Id.ToString(), out gId)) return ResResult.Response(false, "参数不正确", "");

                var bll = new Pandian();
                if (bll.UpdateIsDown(gId) < 1) return ResResult.Response(false, "下载失败，请稍后再重试！", "");

                return ResResult.Response(true, "调用成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPandianAssetList(PdaPandianAssetModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, "未找到任何参数", "");

                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 10) model.PageSize = 10;
                int totalRecord = 0;

                var pandianId = Guid.Empty;
                if (model.PandianId != null)
                {
                    Guid.TryParse(model.PandianId.ToString(), out pandianId);
                }
                //if (pandianId.Equals(Guid.Empty)) return ResResult.Response(false, "参数PandianId值为“" + model.PandianId + "”不正确", "");

                var status = Enum.GetName(typeof(EnumData.EnumPandianAssetStatus), model.Status);
                //if (string.IsNullOrWhiteSpace(status)) return ResResult.Response(false, "参数Status值为“" + model.Status + "”不正确", "");

                var sqlWhere = new StringBuilder(100);
                var parms = new ParamsHelper();
                SqlParameter parm = null;

                if (!pandianId.Equals(Guid.Empty))
                {
                    sqlWhere.Append("and PandianId = @PandianId ");
                    parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
                    parm.Value = pandianId;
                    parms.Add(parm);
                }
                
                if (model.Status > -1)
                {
                    sqlWhere.Append("and pda.Status = @Status ");
                    parm = new SqlParameter("@Status", SqlDbType.NVarChar, 20);
                    parm.Value = status;
                    parms.Add(parm);
                }

                var bll = new PandianAsset();
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere.ToString(), parms.ToArray());

                var pdaList = new List<PdaPandianAssetInfo>();
                foreach (var item in list)
                {
                    var pdaModel = new PdaPandianAssetInfo();
                    pdaModel.PandianId = item.PandianId;
                    pdaModel.AssetId = item.AssetId;
                    pdaModel.Named = item.Named;
                    pdaModel.PandianUser = item.UserName;
                    pdaModel.TotalQty = list.Count;
                    pdaModel.Remark = item.Remark;

                    pdaModel.PandianAssetStatus = item.Status;
                    pdaModel.PictureUrl = "";
                    pdaModel.AssetName = item.AssetName;
                    pdaModel.Barcode = item.Barcode;
                    pdaModel.SNCode = item.SNCode;
                    pdaModel.Category = item.Category;
                    pdaModel.CategoryId = item.CategoryId;
                    pdaModel.SpecModel = item.SpecModel;
                    pdaModel.OwnedCompany = item.OwnedCompany;
                    pdaModel.UseCompany = item.UseCompany;
                    pdaModel.UseDepmt = item.UseDepmt;
                    pdaModel.Region = item.Region;
                    pdaModel.StoreLocation = item.StoreLocation;
                    pdaModel.UsePerson = item.UsePerson;
                    pdaModel.Unit = item.Unit;

                    pdaList.Add(pdaModel);
                }

                var totals = bll.GetTotal(pandianId);

                var dgData = "{\"total\":" + pdaList.Count + ",\"rows\":" + JsonConvert.SerializeObject(pdaList) + ",\"footer\":[{\"TotalPan\":" + totals[0] + ",\"TotalYpan\":" + totals[1] + ",\"TotalNotPan\":" + totals[2] + "}]}";
                return ResResult.Response(true, "", dgData);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetPanYingBarcode()
        {
            try
            {
                OrderCode o = new OrderCode();
                return ResResult.Response(true, "", o.GetOrderCode(((int)EnumData.EnumOrderPrefix.盘盈).ToString()));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SavePandianAsset(PdaPandianAssetFmModel model)
        {
            object userId = null;
            SecurityService.DoCheckLogin(model.AppKey, model.UserName, out userId);

            if (model == null) return ResResult.Response(false, "请求参数集为空字符串", "");
            var pandianId = Guid.Empty;
            if (model.PandianId == null || !Guid.TryParse(model.PandianId.ToString(), out pandianId)) return ResResult.Response(false, "参数PandianId值为“" + model.PandianId + "”无效", "");
            if (model.ItemList == null || model.ItemList.Count == 0) return ResResult.Response(false, "请求参数集为空字符串", "");

            var pdaBll = new PandianAsset();
            var aisBll = new AssetInStore();
            var pdBll = new Pandian();
            var effect = 0;

            foreach (var item in model.ItemList)
            {
                PandianAssetInfo pdaModel = null;
                AssetInStoreInfo assetModel = null;

                var assetId = Guid.Empty;
                if (item.AssetId != null) Guid.TryParse(item.AssetId.ToString(), out assetId);
                if (assetId == Guid.Empty)
                {
                    if (string.IsNullOrWhiteSpace(item.Barcode)) continue;
                    
                    if (pdaBll.IsExist(item.Barcode))
                    {
                        assetModel = aisBll.GetModelByBarcode(item.Barcode);
                        if (assetModel != null)
                        {
                            CreateAssetInStoreInfo(item, ref assetModel, ref pdaModel);

                            pdaModel.AssetId = assetModel.Id;
                            pdaModel.PandianId = pandianId;
                            assetModel.UserId = Guid.Parse(userId.ToString());
                            pdaModel.UserId = assetModel.UserId;
                            effect += aisBll.Update(assetModel);
                            effect += pdaBll.Update(pdaModel);
                        }
                    }
                    else
                    {
                        CreateAssetInStoreInfo(item, ref assetModel,ref pdaModel);
                        assetModel.Id = Guid.NewGuid();
                        pdaModel.AssetId = assetModel.Id;
                        pdaModel.PandianId = pandianId;
                        assetModel.UserId = Guid.Parse(userId.ToString());
                        pdaModel.UserId = assetModel.UserId;
                        effect += aisBll.InsertByOutput(assetModel);
                        effect += pdaBll.Insert(pdaModel);
                    }
                }
                else
                {
                    assetModel = aisBll.GetModel(assetId);
                    pdaModel = pdaBll.GetModel(pandianId, assetId);
                    CreateAssetInStoreInfo(item, ref assetModel, ref pdaModel);

                    pdaModel.UserId = Guid.Parse(userId.ToString());
                    effect += pdaBll.Update(pdaModel);
                }
            }

            if(effect < 1) return ResResult.Response(false, "操作失败", "");

            return ResResult.Response(true, "调用成功", "");
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPandianAssetByBarcode(string appKey,string userName,object pandianId,string barcode)
        {
            try
            {
                object userId = null;
                SecurityService.DoCheckLogin(appKey, userName, out userId);

                if(string.IsNullOrWhiteSpace(barcode)) return ResResult.Response(false, "参数barcode值不能为空字符串", "");

                var gId = Guid.Empty;
                if(pandianId != null) Guid.TryParse(pandianId.ToString(), out gId);
                if(gId.Equals(Guid.Empty)) return ResResult.Response(false, "参数pandianId值不正确", "");

                var sqlWhere = @"and pd.Id = @PandianId and ais.Barcode = @Barcode ";
                SqlParameter[] parms = {
                    new SqlParameter("@PandianId",SqlDbType.UniqueIdentifier),
                    new SqlParameter("@Barcode",SqlDbType.VarChar,36)
                };
                parms[0].Value = gId;
                parms[1].Value = barcode;

                var bll = new PandianAsset();
                var list = bll.GetListByJoin(sqlWhere, parms.ToArray());
                if(list == null || list.Count == 0) return ResResult.Response(false, "数据不存在或已被删除", "");

                var pdaModel = new PdaPandianAssetInfo();
                var item = list[0];

                pdaModel.PandianId = item.PandianId;
                pdaModel.AssetId = item.AssetId;
                pdaModel.Named = item.Named;
                pdaModel.PandianUser = item.UserName;

                pdaModel.PandianAssetStatus = item.Status;
                pdaModel.PictureUrl = "";
                pdaModel.AssetName = item.AssetName;
                pdaModel.Barcode = item.Barcode;
                pdaModel.Category = item.Category;
                pdaModel.SpecModel = item.SpecModel;
                pdaModel.OwnedCompany = item.OwnedCompany;
                pdaModel.UseCompany = item.UseCompany;
                pdaModel.UseDepmt = item.UseDepmt;
                pdaModel.Region = item.Region;
                pdaModel.StoreLocation = item.StoreLocation;
                pdaModel.UsePerson = item.UsePerson;
                pdaModel.Unit = item.Unit;

                return ResResult.Response(true, "调用成功", JsonConvert.SerializeObject(pdaModel));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 私有

        private void CreateAssetInStoreInfo(PdaPandianAssetItemModel item, ref AssetInStoreInfo model,ref PandianAssetInfo pdaModel)
        {
            AssetInStoreInfo newModel = null;
            PandianAssetInfo newPdaModel = null;
            if (model != null) newModel = model;
            else newModel = new AssetInStoreInfo();
            if (pdaModel != null) newPdaModel = pdaModel;
            else newPdaModel = new PandianAssetInfo();

            var currTime = DateTime.Now;

            newModel.Barcode = item.Barcode;

            var gId = Guid.Empty;

            if (item.Category != null) Guid.TryParse(item.Category.ToString(), out gId);
            newModel.CategoryId = gId;
            newModel.Named = item.AssetName;
            newModel.SpecModel = item.SpecModel;
            if (model == null) newModel.SNCode = "";
            newModel.Unit = item.Unit;
            if (model == null) newModel.Price = 0;

            if (item.UseCompany != null) Guid.TryParse(item.UseCompany.ToString(), out gId);
            newPdaModel.UpdatedUseCompanyId = gId;
            newModel.UseCompanyId = gId;

            if (item.UseDepmt != null) Guid.TryParse(item.UseDepmt.ToString(), out gId);
            newPdaModel.UpdatedUseDepmtId = gId;

            if (model == null) newModel.BuyDate = currTime;
            newModel.UsePerson = item.UsePerson;
            newPdaModel.UpdatedUsePerson = item.UsePerson;
            if (model == null) newModel.Manager = "";
            if (item.OwnedCompany != null) Guid.TryParse(item.OwnedCompany.ToString(), out gId);
            newModel.OwnedCompanyId = gId;

            if (item.Region != null) Guid.TryParse(item.Region.ToString(), out gId);
            newModel.RegionId = gId;
            newPdaModel.UpdatedRegionId = gId;
            newModel.StoreLocation = item.StoreLocation;
            newPdaModel.UpdatedStoreLocation = item.StoreLocation;
            if (model == null) newModel.UseExpireMonth = 120;
            if (model == null) newModel.Supplier = "";
            if (model == null) newModel.PictureId = Guid.Empty;
            newModel.Remark = item.Remark;
            newModel.LastUpdatedDate = currTime;
            newPdaModel.Status = item.Status;
            newPdaModel.Remark = item.Remark;
            newPdaModel.LastUpdatedDate = currTime;
            newModel.LastUpdatedDate = currTime;

            model = newModel;
            pdaModel = newPdaModel;
        }

        #endregion
    }
}
