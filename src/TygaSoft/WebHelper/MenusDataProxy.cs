using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Web.Security;
using Newtonsoft.Json;
using TygaSoft.CacheDependencyFactory;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.CustomProvider;
using TygaSoft.SysHelper;

namespace TygaSoft.WebHelper
{
    public class MenusDataProxy
    {
        private static readonly bool enableCaching = bool.Parse(ConfigurationManager.AppSettings["EnableCaching"]);
        private static readonly int menusTimeout = int.Parse(ConfigurationManager.AppSettings["MenusCacheDuration"]);

        public static List<MenusInfo> GetUserMenus()
        {
            List<MenusInfo> userMenuList = new List<MenusInfo>();
            CustomProfileCommon profile = new CustomProfileCommon();
            var sUserMenu = profile.UserMenus;
            if (!string.IsNullOrEmpty(sUserMenu)) userMenuList = JsonConvert.DeserializeObject<List<MenusInfo>>(sUserMenu).FindAll(m => m.IsView && m.Descr != "hide");

            return userMenuList;
        }

        public static void UserIsAccess(int enumValidateAccess)
        {
            if (HttpContext.Current.User.IsInRole("Administrators")) return;

            var uri = HttpContext.Current.Request.UrlReferrer;
            if(uri == null) throw new ArgumentException(MC.Role_InvalidError);
            var url = uri.ToString();
            List<MenusInfo> userMenuList = GetUserMenus();
            var currNode = userMenuList.FirstOrDefault(m => !string.IsNullOrEmpty(m.Url) && url.Contains(m.Url));
            if (currNode == null) throw new ArgumentException(MC.Role_InvalidError);
            switch (enumValidateAccess)
            {
                case (int)EnumData.EnumValidateAccess.IsView:
                    if(!currNode.IsView) throw new ArgumentException(MC.Role_InvalidError);
                    break;
                case (int)EnumData.EnumValidateAccess.IsAdd:
                    if(!currNode.IsAdd) throw new ArgumentException(MC.Role_InvalidError);
                    break;
                case (int)EnumData.EnumValidateAccess.IsEdit:
                    if(!currNode.IsEdit) throw new ArgumentException(MC.Role_InvalidError);
                    break;
                case (int)EnumData.EnumValidateAccess.IsDelete:
                    if(!currNode.IsDelete) throw new ArgumentException(MC.Role_InvalidError);
                    break;
                default:
                    throw new ArgumentException(MC.Role_InvalidError);
            }
        }

        public static IList<MenusInfo> GetList()
        {
            var appName = Membership.ApplicationName;
            Menus bll = new Menus();

            if (!enableCaching)
            {
                return bll.GetMenus(appName);
            }

            string key = "Menus_All_" + appName + "";
            IList<MenusInfo> data = (List<MenusInfo>)HttpRuntime.Cache[key];

            if (data == null)
            {
                data = bll.GetMenus(appName);

                AggregateCacheDependency cd = DependencyFacade.GetMenusDependency();
                HttpRuntime.Cache.Add(key, data, cd, DateTime.Now.AddHours(menusTimeout), Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }

            return data;
        }
    }
}
