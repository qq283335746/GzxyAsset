using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TygaSoft.BLL;

namespace TygaSoft.Web.Users.Asset.InStore
{
    public partial class AddAssetInStore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Bind();
            }
        }

        private void Bind()
        {
            Guid gId = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(Request.QueryString["Id"])) Guid.TryParse(Request.QueryString["Id"], out gId);
            if (!gId.Equals(Guid.Empty))
            {
                var bll = new AssetInStore();
                var model = bll.GetModel(gId);
                if (model != null)
                {
                    hId.Value = model.Id.ToString();
                    hCategoryId.Value = model.CategoryId.ToString();
                    hUseCompanyId.Value = model.UseCompanyId.ToString();
                    hUseDepmtId.Value = model.UseDepmtId.ToString();
                    hOwnedCompanyId.Value = model.OwnedCompanyId.ToString();
                    hRegionId.Value = model.RegionId.ToString();
                    hPictureId.Value = model.PictureId.ToString();
                    txtBarcode.Value = model.Barcode;
                    txtName.Value = model.Named;
                    txtSpecModel.Value = model.SpecModel;
                    txtSNCode.Value = model.SNCode;
                    txtUnit.Value = model.Unit;
                    txtPrice.Value = model.Price.ToString();
                    txtBuyDate.Value = model.BuyDate.ToString("yyyy-MM-dd");
                    txtUsePerson.Value = model.UsePerson;
                    txtManager.Value = model.Manager;
                    txtStoreLocation.Value = model.StoreLocation;
                    txtUseExpireMonth.Value = model.UseExpireMonth.ToString();
                    txtSupplier.Value = model.Supplier;
                    txtRFID.Value = model.RFID;
                    txtRemark.Value = model.Remark;
                }
            }
        }
    }
}