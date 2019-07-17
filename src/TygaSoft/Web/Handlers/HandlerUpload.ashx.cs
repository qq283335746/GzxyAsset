using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.IO;
using System.Transactions;
using System.Web.Configuration;
using System.Text;
using TygaSoft.SysHelper;
using TygaSoft.WebHelper;
using TygaSoft.Model;
using TygaSoft.BLL;

namespace TygaSoft.Web.Handlers
{
    /// <summary>
    /// HandlerUpload 的摘要说明
    /// </summary>
    public class HandlerUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string reqName = "";
            switch (context.Request.HttpMethod.ToUpper())
            {
                case "GET":
                    reqName = context.Request.QueryString["reqName"].Trim();
                    break;
                case "POST":
                    reqName = context.Request.Form["reqName"].Trim();
                    break;
                default:
                    break;
            }

            switch (reqName)
            {
                case "AssetInStore":
                    OnUploadByAssetInStore(context);
                    break;
                case "ExportAssetInStore":
                    OnExportByAssetInStore(context);
                    break;
                default:
                    break;
            }
        }

        private void OnUploadByAssetInStore(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            try
            {
                HttpFileCollection files = context.Request.Files;
                if (files.Count == 0)
                {
                    context.Response.Write("{\"success\": false,\"message\": \"未找到任何可上传的文件，请检查！\"}");
                    return;
                }
                foreach (string item in files.AllKeys)
                {
                    HttpPostedFile file = files[item];
                    if (file == null || file.ContentLength == 0)
                    {
                        continue;
                    }
                    ImportByAssetInStore(context, file);
                }
            }
            catch (Exception ex)
            {
                context.Response.Write(ResResult.ResJsonString(false,ex.Message,""));
            }
        }

        private void OnExportByAssetInStore(HttpContext context)
        {
            try
            {
                AssetInStore aisBll = new AssetInStore();
                var list = aisBll.GetListByJoin();
                if (list == null || list.Count == 0)
                {
                    context.Response.Write(ResResult.ResJsonString(false, "无数据", ""));
                    return;
                }
                var colAppend = "照片,资产条码,资产名称,资产类别,规格型号,SN号,计量单位,金额,使用公司,使用部门,使用人,区域,存放地点,管理员,所属公司,购入时间,供应商,使用期限";
                var cols = colAppend.Split(',');
                var dt = new DataTable("dtAssetInStore");
                foreach (var item in cols)
                {
                    dt.Columns.Add(new DataColumn(item, System.Type.GetType("System.String")));
                }
                foreach (var model in list)
                {
                    var dr = dt.NewRow();
                    for (var i = 0; i < cols.Length; i++)
                    {
                        var text = "";
                        switch (i)
                        {
                            case 0:
                                text = "";
                                break;
                            case 1:
                                text = model.Barcode;
                                break;
                            case 2:
                                text = model.Named;
                                break;
                            case 3:
                                text = model.CategoryName;
                                break;
                            case 4:
                                text = model.SpecModel;
                                break;
                            case 5:
                                text = model.SNCode;
                                break;
                            case 6:
                                text = model.Unit;
                                break;
                            case 7:
                                text = model.Price.ToString();
                                break;
                            default:
                                break;
                        }
                        dr[i] = text;
                    }
                    dt.Rows.Add(dr);
                }

                //NpoiHelper npoi = new NpoiHelper();
                //var fileUrl = npoi.ExportExcel(dt, "资产列表.xlsx");
                var fileUrl = "";

                context.Response.Write(ResResult.ResJsonString(true, "", fileUrl));
            }
            catch (Exception ex)
            {
                context.Response.Write(ResResult.ResJsonString(false, ex.Message, ""));
            }
        }

        private void ImportByAssetInStore(HttpContext context, HttpPostedFile file)
        {
            //NpoiHelper npoi = new NpoiHelper();
            //var dt = npoi.GetExcelData(Path.GetExtension(file.FileName), file.InputStream);
            var dt = new DataTable();
            var drc = dt.Rows;
            if (drc.Count == 0)
            {
                context.Response.Write(ResResult.ResJsonString(false, "导入的数据不能为空字符串", ""));
                return;
            }
            Category cBll = new Category();
            OrgDepmt odBll = new OrgDepmt();
            Region rBll = new Region();
            decimal d = decimal.MinValue;
            DateTime time = DateTime.MinValue;
            var list = new List<AssetInStoreInfo>();

            foreach (DataRow dr in drc)
            {
                if (string.IsNullOrWhiteSpace(dr["资产条码"].ToString()) || string.IsNullOrWhiteSpace(dr["资产名称"].ToString()) || string.IsNullOrWhiteSpace(dr["资产类别编码"].ToString()) || string.IsNullOrWhiteSpace(dr["使用公司编码"].ToString()) || string.IsNullOrWhiteSpace(dr["存放地点"].ToString()) || string.IsNullOrWhiteSpace(dr["所属公司编码"].ToString()) || string.IsNullOrWhiteSpace(dr["购入时间"].ToString()))
                {
                    throw new ArgumentException("带有“*”的列为必填项，请正确操作");
                }
                var model = new AssetInStoreInfo();
                model.Barcode = dr["资产条码"].ToString();
                model.Named = dr["资产名称"].ToString();
                var categoryModel = cBll.GetModelByCode(dr["资产类别编码"].ToString().Trim());
                if (categoryModel == null) throw new ArgumentException("资产类别编码“" + dr["资产类别编码"].ToString().Trim() + "”对应的数据不存在或已被删除");
                model.CategoryId = categoryModel.Id;

                if (!decimal.TryParse(dr["金额"].ToString(), out d)) throw new ArgumentException("金额“" + dr["金额"].ToString() + "”不正确");
                model.Price = d;
                model.SpecModel = dr["规格型号"].ToString();
                model.Unit = dr["计量单位"].ToString();

                var useCompanyModel = odBll.GetModelByCode(dr["使用公司编码"].ToString());
                if(useCompanyModel == null) throw new ArgumentException("使用公司编码“" + dr["使用公司编码"].ToString() + "”对应的数据不存在或已被删除");
                var orgdModel = odBll.GetModelByCode(dr["使用部门编码"].ToString());
                if (orgdModel == null) throw new ArgumentException("使用部门编码“" + dr["使用部门编码"].ToString() + "”对应的数据不存在或已被删除");

                model.UseCompanyId = useCompanyModel.Id;
                model.UseDepmtId = orgdModel.Id;

                var rModel = rBll.GetModelByCode(dr["区域编码"].ToString());
                if(rModel == null) throw new ArgumentException("区域编码“" + dr["区域编码"].ToString() + "”对应的数据不存在或已被删除");
                model.RegionId = rModel.Id;

                var ownedCompanyModel = odBll.GetModelByCode(dr["所属公司编码"].ToString());
                if(ownedCompanyModel == null) throw new ArgumentException("所属公司编码“" + dr["所属公司编码"].ToString() + "”对应的数据不存在或已被删除");
                model.OwnedCompanyId = ownedCompanyModel.Id;

                model.UsePerson = dr["使用人"].ToString();
                model.StoreLocation = dr["存放地点"].ToString();
                model.Manager = dr["管理员姓名"].ToString();
                if(!DateTime.TryParse(dr["购入时间"].ToString(), out time)) throw new ArgumentException("购入时间“" + dr["购入时间"].ToString() + "”不正确");
                model.BuyDate = time;
                model.Supplier = dr["供应商"].ToString();
                model.Remark = dr["备注"].ToString();
                model.SNCode = dr["SN号"].ToString();

                model.UseExpireMonth = 1200;
                model.PictureId = Guid.Empty;
                model.LastUpdatedDate = DateTime.Now;

                list.Add(model);
            }

            AssetInStore aisBll = new AssetInStore();
            var index = 0;
            var userId = WebCommon.GetUserId();
            foreach (var model in list)
            {
                model.UserId = userId;
                if (aisBll.Insert(model) < 1) throw new ArgumentException(string.Format("{0}", index > 0 ? "部分数据已经成功导入，但是执行到第“" + index + "”行时发生异常" : "数据导入失败，行“" + index + "”发生异常"));
                index++;
            }
            context.Response.Write(ResResult.ResJsonString(true, "导入成功", ""));
        }
        
        private void CreateThumbnailImage(HttpContext context, ImagesHelper ih, string originalUrl, string fileDirectory, string randomFolder, string fileExtension)
        {
            string rndDirFullPath = context.Server.MapPath(string.Format("~{0}{1}", fileDirectory, randomFolder));
            if (!Directory.Exists(rndDirFullPath))
            {
                Directory.CreateDirectory(rndDirFullPath);
            }
            File.Copy(context.Server.MapPath(originalUrl), string.Format("{0}\\{1}{2}", rndDirFullPath, randomFolder, fileExtension), true);

            string[] platformNames = Enum.GetNames(typeof(EnumData.Platform));
            foreach (string name in platformNames)
            {
                string platformUrl = string.Format("{0}/{1}/{2}", fileDirectory, randomFolder, name);
                string platformUrlFullPath = context.Server.MapPath("~" + platformUrl);
                if (!Directory.Exists(platformUrlFullPath))
                {
                    Directory.CreateDirectory(platformUrlFullPath);
                }
                string sizeAppend = WebConfigurationManager.AppSettings[name];
                string[] sizeArr = sizeAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < sizeArr.Length; i++)
                {
                    string bmsPicUrl = string.Format("{0}\\{1}_{2}{3}", platformUrlFullPath, randomFolder, i, fileExtension);
                    string[] wh = sizeArr[i].Split('*');

                    ih.CreateThumbnailImage(context.Server.MapPath(originalUrl), bmsPicUrl, int.Parse(wh[0]), int.Parse(wh[1]), "DB", fileExtension);
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}