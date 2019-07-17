using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TygaSoft.Web.Users
{
    public partial class Users : System.Web.UI.MasterPage
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
            Control ctl = this.LoadControl("~/WebUserControls/UCMenu.ascx");
            ctl.ID = "UCMenu";
            phUc.Controls.Clear();
            phUc.Controls.Add(ctl);
            lbSiteTitle.InnerText = "智能资产管理系统";

            //if (HttpContext.Current.User.IsInRole("Users_Asset"))
            //{
            //    Control ctl = this.LoadControl("~/WebUserControls/UCAssetMenu.ascx");
            //    ctl.ID = "UCAssetMenu";
            //    phUc.Controls.Clear();
            //    phUc.Controls.Add(ctl);

            //    SitePaths.SiteMapProvider = "AssetSiteMapProvider";
            //    lbSiteTitle.InnerText = "智能资产管理系统";
            //}
        }
    }
}