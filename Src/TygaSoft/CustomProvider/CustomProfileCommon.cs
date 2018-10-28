using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Profile;
using TygaSoft.BLL;

namespace TygaSoft.CustomProvider
{
    public class CustomProfileCommon : ProfileBase
    {
        new HttpContext Context;

        public CustomProfileCommon()
        {
            this.Context = HttpContext.Current;
        }

        public CustomProfileCommon(HttpContext Context)
        {
            this.Context = Context;
        }

        public new void Save()
        {
            Context.Profile.Save();
        }

        [SettingsAllowAnonymous(false)]
        [ProfileProvider("SqlProfileProvider")]
        public string UserMenus
        {
            get { return (string)Context.Profile.GetPropertyValue("UserMenus"); }
            set { Context.Profile.SetPropertyValue("UserMenus", value); }
        }

        [SettingsAllowAnonymous(false)]
        [ProfileProvider("SqlProfileProvider")]
        public string MobilePhone
        {
            get { return (string)Context.Profile.GetPropertyValue("MobilePhone"); }
            set { Context.Profile.SetPropertyValue("MobilePhone", value); }
        }

        public CustomProfileCommon GetProfile(string userName,bool isAuthenticated)
        {
            return (CustomProfileCommon)ProfileBase.Create(userName, isAuthenticated);
        }

        public string GetUserName()
        {
            if (Context.Profile.IsAnonymous) return Context.Request.AnonymousID;
            else return Context.Profile.UserName;
        }
    }
}
