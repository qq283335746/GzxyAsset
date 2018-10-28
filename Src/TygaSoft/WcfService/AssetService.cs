using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using TygaSoft.SysHelper;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.WcfModel;
using TygaSoft.WebHelper;

namespace TygaSoft.WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AssetService : IAsset
    {
        #region 基础数据

        #region 公司企业

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetCompanyList(CompanyModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 10) model.PageSize = 10;

                var bll = new Company();
                var totalRecord = 0;

                var list = bll.GetList(model.PageIndex, model.PageSize, out totalRecord, "", null);
                if (list != null && list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        item.state = "closed";
                        item.IsCompany = true;
                    }
                } 

                var dgData = "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}";

                return ResResult.Response(true, "", dgData);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
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
                cbbList.Add(new ComboboxModel { Id = Guid.Empty.ToString(), Text = "请选择" });
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

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveCompany(CompanyFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, "未获取到任何可保存的数据", "");
                if(string.IsNullOrWhiteSpace(model.Coded) || string.IsNullOrWhiteSpace(model.Named)) return ResResult.Response(false, "带有”*“符合的为必须项", "");

                var modelInfo = new CompanyInfo();

                modelInfo.Coded = model.Coded.Trim();
                modelInfo.Named = model.Named.Trim();
                modelInfo.Address = model.Address.Trim();
                modelInfo.Phone = model.Phone.Trim();
                modelInfo.TelPhone = model.TelPhone.Trim();
                modelInfo.Sort = model.Sort;
                modelInfo.Named = model.Named.Trim();
                modelInfo.Remark = model.Remark;
                modelInfo.LastUpdatedDate = DateTime.Now;

                Guid gId = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out gId);
                modelInfo.Id = gId;
                modelInfo.UserId = Guid.Parse(SecurityService.GetUserId().ToString());

                var bll = new Company();
                int effect = -1;

                if (gId.Equals(Guid.Empty))
                {
                    gId = Guid.NewGuid();
                    modelInfo.Id = gId;
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    effect = bll.Update(modelInfo);
                }

                if (effect < 1) return ResResult.Response(false, "操作失败，数据库操作异常，请稍后再重试", "");

                return ResResult.Response(true, "调用成功", JsonConvert.SerializeObject(modelInfo));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "异常：" + ex.Message + "", "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteCompany(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, "未找到任何可删除的数据", "");
                var arr = itemAppend.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr == null && arr.Length == 0) return ResResult.Response(false, "未找到任何可删除的数据", "");
                var bll = new Company();
                if(!bll.DeleteBatch(arr.ToList<object>())) return ResResult.Response(false, "操作失败，请稍后再重试", "");

                return ResResult.Response(true, "调用成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "", "");
            }
        }

        #endregion

        #region 组织机构

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrgDepmtList(object companyId,object parentId)
        {
            try
            {
                var bll = new OrgDepmt();
                var list = bll.GetListByCompany(companyId, parentId);

                var dgData = "{\"total\":" + list.Count + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}";

                return ResResult.Response(true, "", dgData);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetCbbOrgDepmt(object companyId)
        {
            try
            {
                var bll = new OrgDepmt();
                var list = bll.GetListByCompany(companyId,null);
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

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetOrgDepmtTree(object companyId)
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
                return ResResult.Response(false, ex.Message,"");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveOrgDepmt(OrgDepmtFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, "未获取到任何参数");
                if (string.IsNullOrWhiteSpace(model.Coded) || string.IsNullOrWhiteSpace(model.Named)) return ResResult.Response(false, "编码或名称不能为空字符串");

                var companyId = Guid.Empty;
                if (model.CompanyId != null) Guid.TryParse(model.CompanyId.ToString(), out companyId);
                if(companyId.Equals(Guid.Empty)) return ResResult.Response(false, "请选择公司");

                var Id = Guid.Empty;
                var parentId = Guid.Empty;
                if (model.Id != null && !string.IsNullOrWhiteSpace(model.Id.ToString())) Guid.TryParse(model.Id.ToString(), out Id);
                if (model.ParentId != null && !string.IsNullOrWhiteSpace(model.ParentId.ToString())) Guid.TryParse(model.ParentId.ToString(), out parentId);

                var bll = new OrgDepmt();
                OrgDepmtInfo modelInfo = null;
                int effect = 0;

                if (Id.Equals(Guid.Empty))
                {
                    modelInfo = new OrgDepmtInfo();
                    modelInfo.Id = Id;
                    modelInfo.CompanyId = companyId;
                    modelInfo.ParentId = parentId;
                    modelInfo.IdStep = model.IdStep.Trim(',');
                    modelInfo.CodeStep = model.Coded + "," + model.CodeStep.Trim(',');
                }
                else
                {
                    modelInfo = bll.GetModel(Id);
                }

                modelInfo.Coded = model.Coded;
                modelInfo.Named = model.Named;
                modelInfo.Remark = model.Remark;
                modelInfo.Sort = model.Sort;
                modelInfo.LastUpdatedDate = DateTime.Now;
                modelInfo.UserId = Guid.Parse(SecurityService.GetUserId().ToString());

                if (Id.Equals(Guid.Empty))
                {
                    modelInfo.Id = Guid.NewGuid();
                    modelInfo.IdStep = modelInfo.Id + "," + modelInfo.IdStep;
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, "操作失败，数据库操作异常");

                return ResResult.Response(true, "调用成功", JsonConvert.SerializeObject(modelInfo));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "","");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteOrgDepmt(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, "未找到任何可删除的数据", "");
                var list = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList<object>();
                if (list == null && list.Count == 0)
                {
                    return ResResult.Response(false, "未找到任何可删除的数据", "");
                }
                var bll = new OrgDepmt();
                return ResResult.Response(bll.DeleteBatch(list), "调用成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "");
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
                MenusDataProxy.UserIsAccess((int)EnumData.EnumOperationAccess.浏览);
                var bll = new Region();
                return ResResult.Response(true, "", bll.GetTreeJson());
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message,"");
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
                    MenusDataProxy.UserIsAccess((int)EnumData.EnumOperationAccess.新增);

                    modelInfo.Id = Guid.NewGuid();
                    effect = bll.InsertByOutput(modelInfo);
                }
                else
                {
                    MenusDataProxy.UserIsAccess((int)EnumData.EnumOperationAccess.编辑);

                    effect = bll.Update(modelInfo);
                }
                if (effect < 1) return ResResult.Response(false, "操作失败，数据库操作异常");

                return ResResult.Response(true, MC.M_Success, modelInfo.Id);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteRegion(Guid Id)
        {
            try
            {
                MenusDataProxy.UserIsAccess((int)EnumData.EnumOperationAccess.删除);

                if (Id.Equals(Guid.Empty))
                {
                    return ResResult.Response(false, MC.Request_Params_InvalidError,"");
                }
                var bll = new Region();
                return ResResult.Response(bll.Delete(Id) > 0, "","");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
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
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, "and IsDisable = 0 ", null);

                var dgData = "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + "}";
                return ResResult.Response(true, "", dgData);
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
                modelInfo.RFID = model.RFID;
                modelInfo.Remark = model.Remark;
                modelInfo.LastUpdatedDate = DateTime.Now;
                modelInfo.UserId = Guid.Parse(SecurityService.GetUserId().ToString());

                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out gId);
                modelInfo.Id = gId;

                var bll = new AssetInStore();
                int effect = -1;

                if (modelInfo.Id.Equals(Guid.Empty))
                {
                    OrderCode o = new OrderCode();
                    modelInfo.Barcode = o.GetOrderCode(((int)EnumData.EnumOrderPrefix.收货).ToString());
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
                return ResResult.Response(false, "异常：" + ex.Message + "", "");
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

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel IsExistAssetInStore(AssetInStoreSearchModel model)
        {
            try
            {
                #region 查询条件

                DateTime startDate = DateTime.MinValue;
                DateTime endDate = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(model.BuyStartDate)) DateTime.TryParse(model.BuyStartDate, out startDate);
                if (!string.IsNullOrWhiteSpace(model.BuyEndDate)) DateTime.TryParse(model.BuyEndDate, out endDate);
                if (startDate > endDate) return ResResult.Response(false, "购入日期的开始时间不能大于结束时间", "");

                var sqlWhere = new StringBuilder();
                var parms = new ParamsHelper();

                if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
                {
                    endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                    sqlWhere.Append("and (BuyDate between @StartDate and @EndDate) ");
                    var parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                    parm.Value = startDate;
                    parms.Add(parm);
                    parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                    parm.Value = endDate;
                    parms.Add(parm);
                }
                else
                {
                    if (startDate != DateTime.MinValue)
                    {
                        sqlWhere.Append("and (BuyDate >= @StartDate) ");
                        var parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        parm.Value = startDate;
                        parms.Add(parm);
                    }
                    if (endDate != DateTime.MinValue)
                    {
                        endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                        sqlWhere.Append("and (BuyDate <= @EndDate) ");
                        var parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        parm.Value = endDate;
                        parms.Add(parm);
                    }
                }
                if (!string.IsNullOrWhiteSpace(model.UseCompany))
                {
                    var arr = model.UseCompany.Split('|');
                    var sqlIn = new StringBuilder(1000);
                    foreach (var item in arr)
                    {
                        sqlIn.AppendFormat("'{0}',", item);
                    }
                    sqlWhere.Append("and (UseCompanyId in (" + sqlIn.ToString().Trim(',') + ")) ");
                }
                if (!string.IsNullOrWhiteSpace(model.UseDepmt))
                {
                    var arr = model.UseCompany.Split('|');
                    var sqlIn = new StringBuilder(1000);
                    foreach (var item in arr)
                    {
                        sqlIn.AppendFormat("'{0}',", item);
                    }
                    sqlWhere.Append("and (UseCompanyId in (" + sqlIn.ToString().Trim(',') + ")) ");
                }
                if (!string.IsNullOrWhiteSpace(model.OwnedCompany))
                {
                    var arr = model.OwnedCompany.Split('|');
                    var sqlIn = new StringBuilder(1000);
                    foreach (var item in arr)
                    {
                        sqlIn.AppendFormat("'{0}',", item);
                    }
                    sqlWhere.Append("and (OwnedCompanyId in (" + sqlIn.ToString().Trim(',') + ")) ");
                }
                if (!string.IsNullOrWhiteSpace(model.Category))
                {
                    var arr = model.OwnedCompany.Split('|');
                    var sqlIn = new StringBuilder(1000);
                    foreach (var item in arr)
                    {
                        sqlIn.AppendFormat("'{0}',", item);
                    }
                    sqlWhere.Append("and (CategoryId in (" + sqlIn.ToString().Trim(',') + ")) ");
                }
                if (!string.IsNullOrWhiteSpace(model.Region))
                {
                    var arr = model.Region.Split('|');
                    var sqlIn = new StringBuilder(1000);
                    foreach (var item in arr)
                    {
                        sqlIn.AppendFormat("'{0}',", item);
                    }
                    sqlWhere.Append("and (RegionId in (" + sqlIn.ToString().Trim(',') + ")) ");
                }
                if (!string.IsNullOrWhiteSpace(model.Manager))
                {
                    var arr = model.Manager.Split('|');
                    var sqlIn = new StringBuilder(1000);
                    foreach (var item in arr)
                    {
                        sqlIn.AppendFormat("'{0}',", item);
                    }
                    sqlWhere.Append("and (Manager in (" + sqlIn.ToString().Trim(',') + ")) ");
                }

                #endregion

                var bll = new AssetInStore();

                return ResResult.Response(true, "", bll.IsExist(sqlWhere.ToString(), parms.ToArray()));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

        #region 资产领用退库

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetAssetUseRefundList(AssetUseRefundModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;

                var sqlWhere = new StringBuilder(100);
                var parms = new ParamsHelper();
                var useRefundId = Guid.Empty;
                if (model.UseRefundId != null) Guid.TryParse(model.UseRefundId.ToString(), out useRefundId);
                if (useRefundId != Guid.Empty)
                {
                    sqlWhere.Append("and ur.Id = @UseRefundId ");
                    var parm = new SqlParameter("@UseRefundId", SqlDbType.UniqueIdentifier);
                    parm.Value = useRefundId;
                    parms.Add(parm);
                }

                var bll = new AssetUseRefund();

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere.ToString(), parms.ToArray());
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
        public ResResultModel SaveAssetUseRefund(AssetUseRefundFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, "未获取到任何可保存的数据", "");
                if (string.IsNullOrWhiteSpace(model.AssetIdAppend.Trim().Trim('|'))) return ResResult.Response(false, "选择的资产不能为空", "");
                var assetIdArr = model.AssetIdAppend.Split('|').ToList();

                var modelInfo = new UseRefundInfo();

                DateTime time = DateTime.MinValue;
                DateTime.TryParse(model.SUseTime, out time);
                if (time == DateTime.MinValue) return ResResult.Response(false, "领用时间格式不正确", "");
                modelInfo.UseTime = time;

                DateTime.TryParse(model.SEstimateRefundTime, out time);
                if (time == DateTime.MinValue) time = DateTime.Parse("1754-01-01");
                modelInfo.EstimateRefundTime = time;

                modelInfo.UsePerson = model.UsePerson;
                modelInfo.UseUser = HttpContext.Current.User.Identity.Name;
                modelInfo.RealRefundTime = DateTime.Parse("1754-01-01");
                modelInfo.RefundDealUser = "";
                modelInfo.Status = EnumData.EnumAssetStatus.领用.ToString();
                modelInfo.Remark = model.Remark;
                modelInfo.LastUpdatedDate = DateTime.Now;

                Guid gId = Guid.Empty;
                if (model.Id != null) Guid.TryParse(model.Id.ToString(), out gId);
                modelInfo.Id = gId;

                var bll = new UseRefund();
                var aurBll = new AssetUseRefund();
                int effect = -1;

                using (TransactionScope scope = new TransactionScope())
                {
                    if (modelInfo.Id.Equals(Guid.Empty))
                    {
                        modelInfo.Id = Guid.NewGuid();
                        effect = bll.InsertByOutput(modelInfo);
                        if (effect > 0)
                        {
                            foreach (var item in assetIdArr)
                            {
                                var aurInfo = new AssetUseRefundInfo();
                                aurInfo.UseRefundId = modelInfo.Id;
                                aurInfo.AssetId = Guid.Parse(item);
                                if (aurBll.Insert(aurInfo) < 1) throw new ArgumentException("数据库连接操作失败");
                            }
                        }
                    }
                    else
                    {
                        var parm = new SqlParameter("@UseRefundId", SqlDbType.UniqueIdentifier);
                        parm.Value = modelInfo.Id;
                        var oldAssetIdList = aurBll.GetList("and UseRefundId = @UseRefundId", parm);
                        foreach (var item in oldAssetIdList)
                        {
                            if (!assetIdArr.Any(s => Guid.Parse(s).Equals(item.AssetId)))
                            {
                                aurBll.Delete(modelInfo.Id, item.AssetId);
                            }
                            else
                            {
                                assetIdArr.Remove(item.AssetId.ToString());
                            }
                        }
                        effect = bll.Update(modelInfo);
                        if (effect > 0 && assetIdArr.Count > 0)
                        {
                            foreach (var item in assetIdArr)
                            {
                                var aurInfo = new AssetUseRefundInfo();
                                aurInfo.UseRefundId = modelInfo.Id;
                                aurInfo.AssetId = Guid.Parse(item);
                                if (aurBll.Insert(aurInfo) < 1) throw new ArgumentException("数据库连接操作失败");
                            }
                        }
                    }

                    scope.Complete();
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
        public ResResultModel DeleteAssetUseRefund(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, "未找到任何可删除的数据", "");
                var arr = itemAppend.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr == null && arr.Length == 0)
                {
                    return ResResult.Response(false, "未找到任何可删除的数据", "");
                }
                var aurBll = new AssetUseRefund();
                var effct = -1;

                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var item in arr)
                    {
                        var itemArr = item.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        effct = aurBll.Delete(itemArr[0], itemArr[1]);
                    }
                    scope.Complete();
                }

                if (effct < 1) return ResResult.Response(false, "操作失败，请稍后再重试", "");

                return ResResult.Response(true, "操作成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "", "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteUseRefund(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, "未找到任何可删除的数据", "");
                var arr = itemAppend.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr == null && arr.Length == 0)
                {
                    return ResResult.Response(false, "未找到任何可删除的数据", "");
                }
                var bll = new UseRefund();
                var aurBll = new AssetUseRefund();
                var effct = -1;

                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var item in arr)
                    {
                        effct = bll.Delete(item);
                        if (effct > 0)
                        {
                            if (aurBll.Delete(item) < 1) throw new ArgumentException("在删除资产领用退库关系表时发生异常");
                        }
                    }
                    scope.Complete();
                }

                if (effct < 1) return ResResult.Response(false, "操作失败，请稍后再重试", "");

                return ResResult.Response(true, "操作成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "", "");
            }
        }

        #endregion

        #region 盘点

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPandianList(PandianModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;

                var sqlWhere = new StringBuilder(100);
                var parms = new ParamsHelper();

                var bll = new Pandian();

                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere.ToString(), parms.ToArray());
                if (totalRecord == 0) return ResResult.Response(true, "", "{\"total\":0,\"rows\":[]}");

                var totals = bll.GetTotal();

                var dgData = "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + ",\"footer\":[{\"TotalAll\":" + totals[0] + ",\"TotalFinish\":" + totals[1] + ",\"TotalNotFinish\":" + totals[2] + "}]}";
                return ResResult.Response(true, "", dgData);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeletePandian(string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, "未找到任何可删除的数据", "");
                var arr = itemAppend.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr == null && arr.Length == 0)
                {
                    return ResResult.Response(false, "未找到任何可删除的数据", "");
                }
                var pdBll = new Pandian();
                var pdsBll = new PandianAsset();
                var effct = 0;

                using (TransactionScope scope = new TransactionScope())
                {
                    foreach (var item in arr)
                    {
                        effct += pdBll.Delete(item);
                        effct += pdsBll.Delete(item);
                    }
                    scope.Complete();
                }

                if (effct < 1) return ResResult.Response(false, "操作失败，请稍后再重试", "");

                return ResResult.Response(true, "操作成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "", "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SavePandianAsset(PandianAssetFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, "未获取到任何可保存的数据", "");
                if (string.IsNullOrWhiteSpace(model.Named)) return ResResult.Response(false, "盘点单名称不能为空字符串", "");
                if (string.IsNullOrWhiteSpace(model.AllowUsers)) return ResResult.Response(false, "请选择分配用户", "");

                #region 查询条件

                DateTime startDate = DateTime.MinValue;
                DateTime endDate = DateTime.MinValue;
                if (!string.IsNullOrWhiteSpace(model.BuyStartDate)) DateTime.TryParse(model.BuyStartDate, out startDate);
                if (!string.IsNullOrWhiteSpace(model.BuyEndDate)) DateTime.TryParse(model.BuyEndDate, out endDate);
                if (startDate > endDate) return ResResult.Response(false, "购入日期的开始时间不能大于结束时间", "");

                var sqlWhere = new StringBuilder();
                var parms = new ParamsHelper();
                SqlParameter parm = null;

                if (startDate != DateTime.MinValue && endDate != DateTime.MinValue)
                {
                    endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                    sqlWhere.Append("and (BuyDate between @StartDate and @EndDate) ");
                    parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                    parm.Value = startDate;
                    parms.Add(parm);
                    parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                    parm.Value = endDate;
                    parms.Add(parm);
                }
                else
                {
                    if (startDate != DateTime.MinValue)
                    {
                        sqlWhere.Append("and (BuyDate >= @StartDate) ");
                        parm = new SqlParameter("@StartDate", SqlDbType.DateTime);
                        parm.Value = startDate;
                        parms.Add(parm);
                    }
                    if (endDate != DateTime.MinValue)
                    {
                        endDate = endDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                        sqlWhere.Append("and (BuyDate <= @EndDate) ");
                        parm = new SqlParameter("@EndDate", SqlDbType.DateTime);
                        parm.Value = endDate;
                        parms.Add(parm);
                    }
                }
                if (!string.IsNullOrWhiteSpace(model.UseCompany))
                {
                    var gId = Guid.Empty;
                    Guid.TryParse(model.UseCompany, out gId);
                    if(!gId.Equals(Guid.Empty))
                    {
                        sqlWhere.Append("and UseCompanyId = @UseCompanyId ");
                        parm = new SqlParameter("@UseCompanyId", SqlDbType.UniqueIdentifier);
                        parm.Value = gId;
                        parms.Add(parm);
                    }
                }
                if (!string.IsNullOrWhiteSpace(model.UseDepmt))
                {
                    var gId = Guid.Empty;
                    Guid.TryParse(model.UseDepmt, out gId);
                    if (!gId.Equals(Guid.Empty))
                    {
                        sqlWhere.Append("and UseDepmtId = @UseDepmtId ");
                        parm = new SqlParameter("@UseDepmtId", SqlDbType.UniqueIdentifier);
                        parm.Value = gId;
                        parms.Add(parm);
                    }
                }
                if (!string.IsNullOrWhiteSpace(model.OwnedCompany))
                {
                    var gId = Guid.Empty;
                    Guid.TryParse(model.OwnedCompany, out gId);
                    if (!gId.Equals(Guid.Empty))
                    {
                        sqlWhere.Append("and OwnedCompanyId = @OwnedCompanyId ");
                        parm = new SqlParameter("@OwnedCompanyId", SqlDbType.UniqueIdentifier);
                        parm.Value = gId;
                        parms.Add(parm);
                    }
                }
                if (!string.IsNullOrWhiteSpace(model.Category))
                {
                    var gId = Guid.Empty;
                    Guid.TryParse(model.Category, out gId);
                    if (!gId.Equals(Guid.Empty))
                    {
                        sqlWhere.Append("and CategoryId = @CategoryId ");
                        parm = new SqlParameter("@CategoryId", SqlDbType.UniqueIdentifier);
                        parm.Value = gId;
                        parms.Add(parm);
                    }
                }
                if (!string.IsNullOrWhiteSpace(model.Region))
                {
                    var gId = Guid.Empty;
                    Guid.TryParse(model.Region, out gId);
                    if (!gId.Equals(Guid.Empty))
                    {
                        var rBll = new Region();
                        var aisModel = rBll.GetModel(gId);
                        if(aisModel != null && !aisModel.ParentId.Equals(Guid.Empty))
                        {
                            sqlWhere.Append("and RegionId = @RegionId ");
                            parm = new SqlParameter("@RegionId", SqlDbType.UniqueIdentifier);
                            parm.Value = gId;
                            parms.Add(parm);
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(model.Manager))
                {
                    var arr = model.Manager.Split('|');
                    var sqlIn = new StringBuilder(1000);
                    foreach (var item in arr)
                    {
                        sqlIn.AppendFormat("'{0}',", item);
                    }
                    sqlWhere.Append("and (Manager in (" + sqlIn.ToString().Trim(',') + ")) ");
                }

                #endregion

                var aisBll = new AssetInStore();
                var aisList = aisBll.GetList(sqlWhere.ToString(), parms.ToArray());
                if (aisList == null || aisList.Count == 0)
                {
                    if (!model.IsConfirm) return ResResult.Response((int)EnumData.ResCode.确认, "您所选范围没有资产，是否创建盘点单?", "");
                }

                var modelInfo = new PandianInfo();
                modelInfo.Named = model.Named.Trim();
                modelInfo.AllowUsers = model.AllowUsers.Trim();
                modelInfo.Remark = model.Remark.Trim();
                modelInfo.CreateDate = DateTime.Now;
                modelInfo.UserId = Guid.Parse(SecurityService.GetUserId().ToString());
                modelInfo.TotalQty = aisList.Count;
                modelInfo.Status = EnumData.EnumPandianStatus.未完成.ToString();
                modelInfo.IsDown = false;
                modelInfo.LastUpdatedDate = DateTime.Now;
                modelInfo.Id = Guid.NewGuid();

                var pdBll = new Pandian();
                var pdaBll = new PandianAsset();
                int effect = 0;

                using (TransactionScope scope = new TransactionScope())
                {
                    effect = pdBll.InsertByOutput(modelInfo);

                    foreach (var item in aisList)
                    {
                        var pdaInfo = new PandianAssetInfo();
                        pdaInfo.PandianId = modelInfo.Id;
                        pdaInfo.AssetId = item.Id;
                        pdaInfo.UpdatedRegionId = Guid.Empty;
                        pdaInfo.UpdatedUseCompanyId = Guid.Empty;
                        pdaInfo.UpdatedUseDepmtId = Guid.Empty;
                        pdaInfo.UpdatedStoreLocation = "";
                        pdaInfo.UpdatedUsePerson = "";
                        pdaInfo.UserId = modelInfo.UserId;
                        pdaInfo.Status = EnumData.EnumPandianAssetStatus.未盘点.ToString();
                        pdaInfo.Remark = "";
                        pdaInfo.LastUpdatedDate = modelInfo.LastUpdatedDate;

                        effect += pdaBll.Insert(pdaInfo);
                    }

                    scope.Complete();
                }

                if (effect < 1) return ResResult.Response(false, "操作失败，原因：可能是由于数据连接异常", "");

                return ResResult.Response(true, "操作成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "异常：" + ex.Message + "", "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetPandianAssetList(PandianAssetModel model)
        {
            try
            {
                if (model.PageIndex < 1) model.PageIndex = 1;
                if (model.PageSize < 1) model.PageSize = 10;
                int totalRecord = 0;

                var pandianId = Guid.Empty;
                if (!Guid.TryParse(model.PandianId.ToString(), out pandianId)) return ResResult.Response(false, "请求参数值“"+ model.PandianId + "”不正确", "");

                var sqlWhere = new StringBuilder(100);
                var parms = new ParamsHelper();

                sqlWhere.Append("and PandianId = @PandianId");
                var parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
                parm.Value = pandianId;
                parms.Add(parm);

                var bll = new PandianAsset();
                var list = bll.GetListByJoin(model.PageIndex, model.PageSize, out totalRecord, sqlWhere.ToString(), parms.ToArray());

                var totals = bll.GetTotal(pandianId);

                var dgData = "{\"total\":" + totalRecord + ",\"rows\":" + JsonConvert.SerializeObject(list) + ",\"footer\":[{\"TotalPan\":" + totals[0] + ",\"TotalYpan\":" + totals[1] + ",\"TotalNotPan\":" + totals[2] + "}]}";
                return ResResult.Response(true, "", dgData);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeletePandianAsset(object pandianId, string itemAppend)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(itemAppend)) return ResResult.Response(false, "未找到任何可删除的数据", "");
                var arr = itemAppend.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr == null && arr.Length == 0)
                {
                    return ResResult.Response(false, "未找到任何可删除的数据", "");
                }
                var gId = Guid.Empty;
                if (pandianId != null) Guid.TryParse(pandianId.ToString(), out gId);
                if (gId.Equals(Guid.Empty)) return ResResult.Response(false, "参数pandianId值为“" + pandianId + "”不正确", "");
                var pdsBll = new PandianAsset();
                var effct = 0;

                foreach (var item in arr)
                {
                    effct += pdsBll.Delete(gId, Guid.Parse(item));
                }

                if (effct < 1) return ResResult.Response(false, "操作失败，请稍后再重试", "");

                return ResResult.Response(true, "调用成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "异常：" + ex.Message + "", "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SavePandianAssetResult(object pandianId)
        {
            try
            {
                var gId = Guid.Empty;
                if (pandianId != null) Guid.TryParse(pandianId.ToString(), out gId);
                if (gId.Equals(Guid.Empty)) return ResResult.Response(false, "参数pandianId值为“" + pandianId + "”无效", "");

                var pdaBll = new PandianAsset();
                var list = pdaBll.GetListByPandianId(gId);
                if (list == null || list.Count == 0) return ResResult.Response(true, "调用成功", "");

                var aisBll = new AssetInStore();
                var effect = 0;
                var isRight = false;

                foreach (var model in list)
                {
                    var aisModel = aisBll.GetModel(model.AssetId);
                    if (aisModel == null) continue;

                    if (model.Status == EnumData.EnumPandianAssetStatus.盘盈.ToString())
                    {
                        if(aisModel.IsDisable)
                        {
                            isRight = true;
                            aisModel.IsDisable = false;
                        }
                    }
                    else
                    {
                        if (!model.UpdatedUseCompanyId.Equals(Guid.Empty))
                        {
                            isRight = true;

                            aisModel.RegionId = model.UpdatedRegionId;
                            aisModel.UseCompanyId = model.UpdatedUseCompanyId;
                            aisModel.UseDepmtId = model.UpdatedUseDepmtId;
                            aisModel.StoreLocation = model.UpdatedStoreLocation;
                            aisModel.UsePerson = model.UsePerson;
                        }
                    }

                    effect += aisBll.Update(aisModel);
                }

                if (effect < 1)
                {
                    if(isRight) return ResResult.Response(false, "操作失败，请稍后再重试！", "");
                }

                return ResResult.Response(true, "调用成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        #endregion

    }
}
