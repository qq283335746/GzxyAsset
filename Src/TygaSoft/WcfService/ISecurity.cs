using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using TygaSoft.Model;
using TygaSoft.WcfModel;

namespace TygaSoft.WcfService
{
    [ServiceContract(Namespace = "http://TygaSoft.Services.SecurityService")]
    public interface ISecurity
    {
        #region 系统管理

        #region 用户角色管理

        [OperationContract(Name = "SaveRole")]
        ResResultModel SaveRole(RoleModel model);

        [OperationContract(Name = "SaveUser")]
        ResResultModel SaveUser(UserFmModel model);

        [OperationContract(Name = "SaveUserInRole")]
        ResResultModel SaveUserInRole(string userName, string roleName, bool isInRole);

        [OperationContract(Name = "DelRole")]
        ResResultModel DelRole(string itemAppend);

        [OperationContract(Name = "SaveIsLockedOut")]
        ResResultModel SaveIsLockedOut(string userName);

        [OperationContract(Name = "SaveIsApproved")]
        ResResultModel SaveIsApproved(string userName);

        [OperationContract(Name = "GetUserInRole")]
        ResResultModel GetUserInRole(string userName);

        [OperationContract(Name = "DelUser")]
        ResResultModel DelUser(string userName);

        [OperationContract(Name = "ResetPassword")]
        ResResultModel ResetPassword(string username);

        [OperationContract(Name = "CheckUserName")]
        ResResultModel CheckUserName(string userName);

        [OperationContract(Name = "GetUserList")]
        ResResultModel GetUserList(UserModel model);

        #endregion

        #region 菜单管理

        [OperationContract(Name = "GetMenusChildrenByParentName")]
        ResResultModel GetMenusChildrenByParentName(string parentName);

        [OperationContract(Name = "GetMenusTreeChildrenByParentName")]
        ResResultModel GetMenusTreeChildrenByParentName(string parentName);

        [OperationContract(Name = "GetMenusTree")]
        ResResultModel GetMenusTree();

        [OperationContract(Name = "GetMenusTreeGrid")]
        ResResultModel GetMenusTreeGrid(MenusPermissionModel model);

        [OperationContract(Name = "SaveRoleMenu")]
        ResResultModel SaveRoleMenu(RoleMenuFmModel model);

        [OperationContract(Name = "SaveMenus")]
        ResResultModel SaveMenus(MenusModel model);

        [OperationContract(Name = "DeleteMenus")]
        ResResultModel DeleteMenus(Guid Id);

        [OperationContract(Name = "GetFirstMenus")]
        IList<MenusInfo> GetFirstMenus();

        #endregion

        [OperationContract(Name = "Login")]
        ResResultModel Login(string appKey, string userName, string password, string validateCode);

        #endregion
    }
}
