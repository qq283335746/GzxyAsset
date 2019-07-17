using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;
using TygaSoft.IDAL;

namespace TygaSoft.DALFactory
{
    public sealed class DataAccess
    {
        private static readonly string[] paths = ConfigurationManager.AppSettings["WebDAL"].Split(',');

        #region ¹«¹²

        public static IRole CreateRole()
        {
            string className = paths[0] + ".Role";
            return (IRole)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IApplication CreateApplication()
        {
            string className = paths[0] + ".Application";
            return (IApplication)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IMenus CreateMenus()
        {
            string className = paths[0] + ".Menus";
            return (IMenus)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IRoleMenu CreateRoleMenu()
        {
            string className = paths[0] + ".RoleMenu";
            return (IRoleMenu)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IUserMenu CreateUserMenu()
        {
            string className = paths[0] + ".UserMenu";
            return (IUserMenu)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IOrderRandom CreateOrderRandom()
        {
            string className = paths[0] + ".OrderRandom";
            return (IOrderRandom)Assembly.Load(paths[1]).CreateInstance(className);
        }

        #endregion

        #region AssetDb

        public static IRegion CreateRegion()
        {
            string className = paths[0] + ".Region";
            return (IRegion)Assembly.Load(paths[1]).CreateInstance(className);
        }
        public static ICompany CreateCompany()
        {
            string className = paths[0] + ".Company";
            return (ICompany)Assembly.Load(paths[1]).CreateInstance(className);
        }
        public static IOrgDepmt CreateOrgDepmt()
        {
            string className = paths[0] + ".OrgDepmt";
            return (IOrgDepmt)Assembly.Load(paths[1]).CreateInstance(className);
        }
        public static ICategory CreateCategory()
        {
            string className = paths[0] + ".Category";
            return (ICategory)Assembly.Load(paths[1]).CreateInstance(className);
        }
        public static IAssetInStore CreateAssetInStore()
        {
            string className = paths[0] + ".AssetInStore";
            return (IAssetInStore)Assembly.Load(paths[1]).CreateInstance(className);
        }

        public static IUseRefund CreateUseRefund()
        {
            string className = paths[0] + ".UseRefund";
            return (IUseRefund)Assembly.Load(paths[1]).CreateInstance(className);
        }
        public static IAssetUseRefund CreateAssetUseRefund()
        {
            string className = paths[0] + ".AssetUseRefund";
            return (IAssetUseRefund)Assembly.Load(paths[1]).CreateInstance(className);
        }
        public static IPandian CreatePandian()
        {
            string className = paths[0] + ".Pandian";
            return (IPandian)Assembly.Load(paths[1]).CreateInstance(className);
        }
        public static IPandianAsset CreatePandianAsset()
        {
            string className = paths[0] + ".PandianAsset";
            return (IPandianAsset)Assembly.Load(paths[1]).CreateInstance(className);
        }

        #endregion

    }
}
