using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using System.Web.SessionState;
using Newtonsoft.Json;
using TygaSoft.CustomProvider;
using TygaSoft.WebHelper;
using TygaSoft.BLL;

namespace TygaSoft.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

        protected void Profile_OnMigrateAnonymous(object sender, ProfileMigrateEventArgs args)
        {
            AnonymousIdentificationModule.ClearAnonymousIdentifier();
            Membership.DeleteUser(args.AnonymousID, true);

            try
            {
                string[] userRoles = Roles.GetRolesForUser();
                var menuBll = new Menus();
                var userMenuAccessList = menuBll.GetUserMenuAccessList(WebCommon.GetUserId(), userRoles);

                CustomProfileCommon profile = new CustomProfileCommon();
                profile.UserMenus = JsonConvert.SerializeObject(userMenuAccessList);
                profile.Save();
            }
            catch {
            }
        }
    }
}