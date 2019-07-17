using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using System.Web.Security;
using System.Transactions;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using TygaSoft.CustomProvider;
using TygaSoft.SysHelper;
using TygaSoft.BLL;
using TygaSoft.Model;
using TygaSoft.WcfModel;
using TygaSoft.WebHelper;

namespace TygaSoft.WcfService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SecurityService:ISecurity
    {
        const string AppKey = "DFE95D05-E044-4E12-BEC8-278ADE2BC708";

        #region 系统管理

        #region 用户角色管理

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveRole(RoleModel model)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators"))
                {
                    return ResResult.Response(false, MC.Role_InvalidError, "");
                }

                model.RoleName = model.RoleName.Trim();
                if (string.IsNullOrEmpty(model.RoleName))
                {
                    return ResResult.Response(false, MC.Submit_Params_InvalidError, "");
                }

                if (Roles.RoleExists(model.RoleName))
                {
                    return ResResult.Response(false, MC.Submit_Exist, "");
                }

                Guid gId = Guid.Empty;
                if (model.RoleId != null)
                {
                    Guid.TryParse(model.RoleId.ToString(), out gId);
                }

                Role bll = new Role();
                RoleInfo modelInfo = new RoleInfo();
                modelInfo.RoleId = gId;
                modelInfo.RoleName = model.RoleName;
                modelInfo.UserName = model.UserName;
                modelInfo.IsInRole = model.IsInRole;

                if (!gId.Equals(Guid.Empty))
                {
                    bll.Update(modelInfo);
                }
                else
                {
                    Roles.CreateRole(model.RoleName);
                }

                return ResResult.Response(true, "调用成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveUser(UserFmModel model)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators"))
                {
                    return ResResult.Response(false, MC.Role_InvalidError, "");
                }

                if (string.IsNullOrWhiteSpace(model.UserName) || string.IsNullOrWhiteSpace(model.Password))
                {
                    return ResResult.Response(false, MC.Submit_Params_InvalidError, "");
                }
                if (model.Password != model.CfmPsw)
                {
                    return ResResult.Response(false, MC.Request_InvalidCompareToPassword, "");
                }
                model.UserName = model.UserName.Trim();
                model.Password = model.Password.Trim();
                if (!Regex.IsMatch(model.Password, Membership.PasswordStrengthRegularExpression))
                {
                    return ResResult.Response(false, MC.Request_InvalidPassword, "");
                }
                if (string.IsNullOrWhiteSpace(model.Email))
                {
                    model.Email = model.UserName + "@tygaweb.com";
                }

                model.RoleName = model.RoleName.Trim().Trim(',');
                string[] roles = null;
                if (!string.IsNullOrEmpty(model.RoleName))
                {
                    roles = model.RoleName.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                }

                MembershipCreateStatus status;
                MembershipUser user;

                using (TransactionScope scope = new TransactionScope())
                {
                    user = Membership.CreateUser(model.UserName, model.Password, model.Email, null, null, model.IsApproved, out status);
                    if (roles != null && roles.Length > 0)
                    {
                        Roles.AddUserToRoles(model.UserName, roles);
                    }

                    scope.Complete();
                }

                if (user == null)
                {
                    return ResResult.Response(false, EnumMembershipCreateStatus.GetStatusMessage(status), "");
                }

                return ResResult.Response(true, "调用成功", "");
            }
            catch (MembershipCreateUserException ex)
            {
                return ResResult.Response(false, EnumMembershipCreateStatus.GetStatusMessage(ex.StatusCode), "");
            }
            catch (HttpException ex)
            {
                return ResResult.Response(false, "" + MC.AlertTitle_Ex_Error + "：" + ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveUserInRole(string userName, string roleName, bool isInRole)
        {
            if (!HttpContext.Current.User.IsInRole("Administrators"))
            {
                return ResResult.Response(false, MC.Role_InvalidError, "");
            }

            if (string.IsNullOrWhiteSpace(userName))
            {
                return ResResult.Response(false, MC.GetString(MC.Request_InvalidArgument, "用户名"), "");
            }
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return ResResult.Response(false, MC.GetString(MC.Request_InvalidArgument, "角色"), "");
            }
            try
            {
                if (isInRole)
                {
                    if (!Roles.IsUserInRole(userName, roleName))
                    {
                        Roles.AddUserToRole(userName, roleName);
                    }
                }
                else
                {
                    if (Roles.IsUserInRole(userName, roleName))
                    {
                        Roles.RemoveUserFromRole(userName, roleName);
                    }
                }

                return ResResult.Response(true, "调用成功", "");
            }
            catch (System.Configuration.Provider.ProviderException pex)
            {
                return ResResult.Response(false, pex.Message, "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DelRole(string itemAppend)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators"))
                {
                    return ResResult.Response(false, MC.Role_InvalidError, "");
                }

                itemAppend = itemAppend.Trim();
                if (string.IsNullOrEmpty(itemAppend))
                {
                    return ResResult.Response(false, MC.Submit_InvalidRow, "");
                }

                string[] roleIds = itemAppend.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in roleIds)
                {
                    Roles.DeleteRole(item);
                }

                return ResResult.Response(true, "调用成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveIsLockedOut(string userName)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators"))
                {
                    return ResResult.Response(false, MC.Role_InvalidError, "");
                }

                MembershipUser user = Membership.GetUser(userName);
                if (user == null)
                {
                    return ResResult.Response(false, "当前用户不存在，请检查", "");
                }
                if (user.IsLockedOut)
                {
                    if (user.UnlockUser())
                    {
                        return ResResult.Response(false, "", "0");
                    }
                    else
                    {
                        return ResResult.Response(false, "操作失败，请联系管理员", "");
                    }
                }

                return ResResult.Response(false, "只有“已锁定”的用户才能执行此操作", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveIsApproved(string userName)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators"))
                {
                    return ResResult.Response(false, MC.Role_InvalidError, "");
                }

                MembershipUser user = Membership.GetUser(userName);
                if (user == null)
                {
                    return ResResult.Response(false, "当前用户不存在，请检查", "");
                }
                if (user.IsApproved)
                {
                    user.IsApproved = false;
                }
                else
                {
                    user.IsApproved = true;
                }

                Membership.UpdateUser(user);

                return ResResult.Response(user.IsApproved, user.IsApproved ? "调用成功" : "", user.IsApproved ? "1" : "0");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetUserInRole(string userName)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators"))
                {
                    return ResResult.Response(false, MC.Role_InvalidError, "");
                }

                string[] roles = Roles.GetRolesForUser(userName);
                if (roles.Length == 0) return ResResult.Response(true, "", "");

                return ResResult.Response(true, "调用成功", string.Join(",", roles));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DelUser(string userName)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators"))
                {
                    return ResResult.Response(false, MC.Role_InvalidError, "");
                }

                Membership.DeleteUser(userName);

                return ResResult.Response(true, "调用成功", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "" + MC.AlertTitle_Ex_Error + "：" + ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel ResetPassword(string username)
        {
            try
            {
                if (!HttpContext.Current.User.IsInRole("Administrators"))
                {
                    return ResResult.Response(false, MC.Role_InvalidError, "");
                }

                if (!Membership.EnablePasswordReset)
                {
                    return ResResult.Response(false, "系统不允许重置密码操作，请联系管理员", "");
                }
                var user = Membership.GetUser(username);
                if (user == null)
                {
                    return ResResult.Response(false, "用户【" + username + "】不存在或已被删除，请检查", "");
                }
                string rndPsw = new Random().Next(100000, 999999).ToString();
                if (!user.ChangePassword(user.ResetPassword(), rndPsw))
                {
                    return ResResult.Response(false, "重置密码失败，请稍后再重试", "");
                }

                return ResResult.Response(true, "调用成功", rndPsw);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel CheckUserName(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return ResResult.Response(false, "参数不能为空字符串", "-1");
            }

            try
            {
                MembershipUser user = Membership.GetUser(userName);
                if (user != null)
                {
                    return ResResult.Response(true, "调用成功", 1);
                }

                return ResResult.Response(true, "调用成功", 0);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        public static object GetUserId()
        {
            var user = Membership.GetUser();
            if (user == null) return Guid.Empty;
            return user.ProviderUserKey;
        }
        public static object GetUserId(string userName)
        {
            var user = Membership.GetUser(userName);
            if (user == null) return Guid.Empty;
            return user.ProviderUserKey;
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetUserList(UserModel model)
        {
            try
            {
                var list = new List<UserInfo>();
                var users = Membership.GetAllUsers();
                foreach (MembershipUser user in users)
                {
                    list.Add(new UserInfo { Id = user.ProviderUserKey, UserName = user.UserName });
                }
                return ResResult.Response(true, "调用成功", JsonConvert.SerializeObject(list));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
            
        }

        #endregion

        #region 菜单管理

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetMenusChildrenByParentName(string parentName)
        {
            try
            {
                List<MenusInfo> userMenuList = MenusDataProxy.GetUserMenus();
                var parentInfo = userMenuList.FirstOrDefault(m => (m.Title == parentName));
                if(parentInfo == null) return ResResult.Response(false, MC.Data_InvalidExist, "");
                var childData = userMenuList.Where(m => (m.ParentId == parentInfo.Id));
                if(childData == null) return ResResult.Response(false, MC.Data_InvalidExist, "");

                //var list = MenusDataProxy.GetList();
                //var childData = list.Where(m => m.ParentId == list.First(mm => mm.Title == parentName).Id);
                return ResResult.Response(true, "", JsonConvert.SerializeObject(childData));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetMenusTreeChildrenByParentName(string parentName)
        {
            try
            {
                List<MenusInfo> userMenuList = MenusDataProxy.GetUserMenus();
                var parentInfo = userMenuList.First(mm => mm.Title == parentName);
                //var childData = list.Where(m => m.ParentId == parentInfo.Id);

                var sb = new StringBuilder();
                var bll = new Menus();
                bll.CreateTreeJson(userMenuList, parentInfo.Id, ref sb);

                //List<JeasyuiTreeInfo> treeList = new List<JeasyuiTreeInfo>();

                //if (childData != null && childData.Count() > 0)
                //{
                //    foreach (var item in childData)
                //    {
                //        var state = list.FirstOrDefault(m => m.ParentId.Equals(item.Id)) != null ? "closed" : "open";
                //        var hasChild = state == "closed";

                //        var attributesInfo = new JeasyuiTreeAttributesInfo { Url = item.Url, HasChild = hasChild };

                //        var childrenList = new List<JeasyuiTreeInfo>();
                //        if (hasChild)
                //        {
                //            var q = list.Where(m => m.ParentId.Equals(item.Id));
                //            foreach (var qi in q)
                //            {
                //                var qiState = list.FirstOrDefault(m => m.ParentId.Equals(qi.Id)) != null ? "closed" : "open";
                //                var qiHasChild = state == "closed";
                //                var qiAttributesInfo = new JeasyuiTreeAttributesInfo { Url = qi.Url, HasChild = qiHasChild };
                //                childrenList.Add(new JeasyuiTreeInfo { id = qi.Id, text = qi.Title, state = qiState, attributes = qiAttributesInfo, children = childrenList });
                //            }
                //        }

                //        treeList.Add(new JeasyuiTreeInfo { id = item.Id, text = item.Title, state = state, attributes = attributesInfo, children = childrenList });
                //    }
                //}
                //return ResResult.Response(true, "", JsonConvert.SerializeObject(treeList));

                return ResResult.Response(true, "", sb.ToString());
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public ResResultModel GetMenusTree()
        {
            try
            {
                var bll = new Menus();
                return ResResult.Response(true, "", bll.GetTreeJson(Membership.ApplicationName));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message,"");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveMenus(MenusModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, "未获取到任何参数");
                if (string.IsNullOrWhiteSpace(model.Title)) return ResResult.Response(false, "菜单名称不能为空字符串");
                var Id = Guid.Empty;
                var parentId = Guid.Empty;
                if (model.Id != null && !string.IsNullOrWhiteSpace(model.Id.ToString())) Guid.TryParse(model.Id.ToString(), out Id);
                if (model.ParentId != null && !string.IsNullOrWhiteSpace(model.ParentId.ToString())) Guid.TryParse(model.ParentId.ToString(), out parentId);

                var appBll = new Application();
                var appId = appBll.GetApplicationId(Membership.ApplicationName);

                var bll = new Menus();
                int effect = 0;

                var modelInfo = new MenusInfo(Guid.Parse(appId.ToString()), Id, parentId, model.IdStep, model.Title, model.Url, model.Descr,"*","",model.Sort,DateTime.Now);

                if (Id.Equals(Guid.Empty))
                {
                    modelInfo.Id = Guid.NewGuid();
                    modelInfo.IdStep = (modelInfo.Id + "," + modelInfo.IdStep).Trim(',');
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
                return ResResult.Response(false, "操作异常："+ex.Message+"");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel DeleteMenus(Guid Id)
        {
            try
            {
                if (Id.Equals(Guid.Empty))
                {
                    return ResResult.Response(false, "参数值无效");
                }
                var bll = new Menus();
                var rmBll = new RoleMenu();
                var umBll = new UserMenu();

                bll.Delete(Id);
                rmBll.DeleteByMenuId(Id);
                umBll.DeleteByMenuId(Id);
                return ResResult.Response(true,"", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "操作异常：" + ex.Message + "","");
            }
        }

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public IList<MenusInfo> GetFirstMenus()
        {
            var list = MenusDataProxy.GetList();
            return null;
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel GetMenusTreeGrid(MenusPermissionModel model)
        {
            try
            {
                object allowRoleId = null;
                object denyUserId = null;
                if (!string.IsNullOrWhiteSpace(model.AllowRole))
                {
                    Role rBll = new Role();
                    allowRoleId = rBll.GetModel(model.AllowRole).RoleId;
                }
                if (!string.IsNullOrWhiteSpace(model.DenyUser))
                {
                    denyUserId = Membership.GetUser(model.DenyUser).ProviderUserKey;
                }
                var bll = new Menus();
                return ResResult.Response(true, "", bll.GetTreeGridJson(Membership.ApplicationName, allowRoleId, denyUserId));
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message,"");
            }
        }

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel SaveRoleMenu(RoleMenuFmModel model)
        {
            try
            {
                if (model == null) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                if(string.IsNullOrWhiteSpace(model.MenuItemJson)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                model.MenuItemJson = HttpUtility.UrlDecode(model.MenuItemJson);
                if (string.IsNullOrWhiteSpace(model.RoleName) && string.IsNullOrWhiteSpace(model.UserName)) return ResResult.Response(false, MC.Request_Params_InvalidError, "");
                List<RoleMenuFmInfo> list = JsonConvert.DeserializeObject<List<RoleMenuFmInfo>>(model.MenuItemJson);
                string roleId = "";
                string userId = "";
                var isRole = !string.IsNullOrWhiteSpace(model.RoleName);
                RoleMenu roleMenuBll = null;
                UserMenu userMenuBll = null;
                if (isRole)
                {
                    roleMenuBll = new RoleMenu();
                    var roleBll = new Role();
                    var roleModel = roleBll.GetModel(model.RoleName);
                    if (roleModel == null) return ResResult.Response(false, MC.GetString(MC.Request_NotExist, "角色'" + model.RoleName + "'对应数据"), "");
                    roleId = roleModel.RoleId.ToString();
                }
                else
                {
                    userMenuBll = new UserMenu();
                    userId = Membership.GetUser(model.UserName).ProviderUserKey.ToString();
                }
                var menuBll = new Menus();
                foreach (var item in list)
                {
                    var menuModel = menuBll.GetModel(Guid.Parse(item.MenuId.ToString()));
                    menuModel.AllowRoles = menuModel.AllowRoles.Replace("*","");
                    var isChanged = false;
                    var isAccessChanged = false;
                    var isInsert = false;
                    var apaItems = new List<string>();

                    if (isRole)
                    {
                        var roleMenuModel = roleMenuBll.GetModel(Guid.Parse(roleId), Guid.Parse(item.MenuId.ToString()));
                        if (roleMenuModel == null)
                        {
                            isInsert = true;
                            roleMenuModel = new RoleMenuInfo();
                            roleMenuModel.RoleId = Guid.Parse(roleId);
                            roleMenuModel.MenuId = Guid.Parse(item.MenuId.ToString());
                        }
                        if (!string.IsNullOrEmpty(roleMenuModel.OperationAccess)) apaItems = roleMenuModel.OperationAccess.Split(',').ToList();

                        #region 允许角色

                        if (item.IsView)
                        {
                            if (!menuModel.AllowRoles.Contains(roleId))
                            {
                                var allowRoles = menuModel.AllowRoles.Split(',').ToList();
                                allowRoles.Add(roleId);
                                menuModel.AllowRoles = string.Join(",", allowRoles.ToArray()).Trim(',');
                                isChanged = true;
                            }
                        }
                        else
                        {
                            if (menuModel.AllowRoles.Contains(roleId))
                            {
                                var allowRoles = menuModel.AllowRoles.Split(',').ToList();
                                allowRoles.Remove(roleId);
                                if (allowRoles.Count > 0) menuModel.AllowRoles = string.Join(",", allowRoles.ToArray()).Trim(',');
                                else menuModel.AllowRoles = "";
                                isChanged = true;
                            }
                        }

                        #endregion

                        #region 角色其它权限

                        if (item.IsAdd)
                        {
                            if (!apaItems.Contains(((int)EnumData.EnumOperationAccess.新增).ToString()))
                            {
                                apaItems.Add(((int)EnumData.EnumOperationAccess.新增).ToString());
                                roleMenuModel.OperationAccess = string.Join(",", apaItems.ToArray()).Trim(',');
                                isAccessChanged = true;
                            }
                        }
                        else
                        {
                            if (apaItems.Contains(((int)EnumData.EnumOperationAccess.新增).ToString()))
                            {
                                apaItems.Remove(((int)EnumData.EnumOperationAccess.新增).ToString());
                                isAccessChanged = true;
                                if (apaItems.Count > 0)
                                {
                                    roleMenuModel.OperationAccess = string.Join(",", apaItems.ToArray()).Trim(',');
                                }
                                else roleMenuModel.OperationAccess = "";
                            }
                        }
                        if (item.IsDelete)
                        {
                            if (!apaItems.Contains(((int)EnumData.EnumOperationAccess.删除).ToString()))
                            {
                                apaItems.Add(((int)EnumData.EnumOperationAccess.删除).ToString());
                                roleMenuModel.OperationAccess = string.Join(",", apaItems.ToArray()).Trim(',');
                                isAccessChanged = true;
                            }
                        }
                        else
                        {
                            if (apaItems.Contains(((int)EnumData.EnumOperationAccess.删除).ToString()))
                            {
                                apaItems.Remove(((int)EnumData.EnumOperationAccess.删除).ToString());
                                isAccessChanged = true;
                                if (apaItems.Count > 0)
                                {
                                    roleMenuModel.OperationAccess = string.Join(",", apaItems.ToArray()).Trim(',');
                                }
                                else roleMenuModel.OperationAccess = "";
                            }
                        }
                        if (item.IsEdit)
                        {
                            if (!apaItems.Contains(((int)EnumData.EnumOperationAccess.编辑).ToString()))
                            {
                                apaItems.Add(((int)EnumData.EnumOperationAccess.编辑).ToString());
                                roleMenuModel.OperationAccess = string.Join(",", apaItems.ToArray()).Trim(',');
                                isAccessChanged = true;
                            }
                        }
                        else
                        {
                            if (apaItems.Contains(((int)EnumData.EnumOperationAccess.编辑).ToString()))
                            {
                                apaItems.Remove(((int)EnumData.EnumOperationAccess.编辑).ToString());
                                isAccessChanged = true;
                                if (apaItems.Count > 0)
                                {
                                    roleMenuModel.OperationAccess = string.Join(",", apaItems.ToArray()).Trim(',');
                                }
                                else roleMenuModel.OperationAccess = "";
                            }
                        }

                        #endregion

                        if (isAccessChanged)
                        {
                            if (isInsert) roleMenuBll.Insert(roleMenuModel);
                            else roleMenuBll.Update(roleMenuModel);
                        }
                    }
                    else
                    {
                        var userMenuInfo = userMenuBll.GetModel(Guid.Parse(userId), Guid.Parse(item.MenuId.ToString()));
                        if (userMenuInfo == null)
                        {
                            isInsert = true;
                            userMenuInfo = new UserMenuInfo();
                            userMenuInfo.UserId = Guid.Parse(userId);
                            userMenuInfo.MenuId = Guid.Parse(item.MenuId.ToString());
                        }
                        if (!string.IsNullOrEmpty(userMenuInfo.OperationAccess)) apaItems = userMenuInfo.OperationAccess.Split(',').ToList();

                        #region 拒绝用户

                        if (item.IsView)
                        {
                            if (!menuModel.DenyUsers.Contains(userId))
                            {
                                var denyUsers = menuModel.DenyUsers.Split(',').ToList();
                                denyUsers.Add(userId);
                                menuModel.DenyUsers = string.Join(",", denyUsers.ToArray()).Trim(',');
                                isChanged = true;
                            }
                        }
                        else
                        {
                            if (menuModel.DenyUsers.Contains(userId))
                            {
                                var denyUsers = menuModel.DenyUsers.Split(',').ToList();
                                denyUsers.Remove(userId);
                                if (denyUsers.Count > 0) menuModel.DenyUsers = string.Join(",", denyUsers.ToArray()).Trim(',');
                                else menuModel.DenyUsers = "";
                                isChanged = true;
                            }
                        }

                        #endregion

                        #region 拒绝用户其它权限

                        if (item.IsAdd)
                        {
                            if (!apaItems.Contains(((int)EnumData.EnumOperationAccess.新增).ToString()))
                            {
                                apaItems.Add(((int)EnumData.EnumOperationAccess.新增).ToString());
                                userMenuInfo.OperationAccess = string.Join(",", apaItems.ToArray()).Trim(',');
                                isAccessChanged = true;
                            }
                        }
                        else
                        {
                            if (apaItems.Contains(((int)EnumData.EnumOperationAccess.新增).ToString()))
                            {
                                apaItems.Remove(((int)EnumData.EnumOperationAccess.新增).ToString());
                                isAccessChanged = true;
                                if (apaItems.Count > 0)
                                {
                                    userMenuInfo.OperationAccess = string.Join(",", apaItems.ToArray()).Trim(',');
                                }
                                else userMenuInfo.OperationAccess = "";
                            }
                        }
                        if (item.IsDelete)
                        {
                            if (!apaItems.Contains(((int)EnumData.EnumOperationAccess.删除).ToString()))
                            {
                                apaItems.Add(((int)EnumData.EnumOperationAccess.删除).ToString());
                                userMenuInfo.OperationAccess = string.Join(",", apaItems.ToArray()).Trim(',');
                                isAccessChanged = true;
                            }
                        }
                        else
                        {
                            if (apaItems.Contains(((int)EnumData.EnumOperationAccess.删除).ToString()))
                            {
                                apaItems.Remove(((int)EnumData.EnumOperationAccess.删除).ToString());
                                isAccessChanged = true;
                                if (apaItems.Count > 0)
                                {
                                    userMenuInfo.OperationAccess = string.Join(",", apaItems.ToArray()).Trim(',');
                                }
                                else userMenuInfo.OperationAccess = "";
                            }
                        }
                        if (item.IsEdit)
                        {
                            if (!apaItems.Contains(((int)EnumData.EnumOperationAccess.编辑).ToString()))
                            {
                                apaItems.Add(((int)EnumData.EnumOperationAccess.编辑).ToString());
                                userMenuInfo.OperationAccess = string.Join(",", apaItems.ToArray()).Trim(',');
                                isAccessChanged = true;
                            }
                        }
                        else
                        {
                            if (apaItems.Contains(((int)EnumData.EnumOperationAccess.编辑).ToString()))
                            {
                                apaItems.Remove(((int)EnumData.EnumOperationAccess.编辑).ToString());
                                isAccessChanged = true;
                                if (apaItems.Count > 0)
                                {
                                    userMenuInfo.OperationAccess = string.Join(",", apaItems.ToArray()).Trim(',');
                                }
                                else userMenuInfo.OperationAccess = "";
                            }
                        }

                        #endregion

                        if (isAccessChanged)
                        {
                            if (isInsert) userMenuBll.Insert(userMenuInfo);
                            else userMenuBll.Update(userMenuInfo);
                        }
                    }

                    if (isChanged)
                    {
                        menuBll.Update(menuModel);
                    }
                }

                return ResResult.Response(true, "", "");
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, "异常：" + ex.Message + "", "");
            }
        }

        #endregion

        [WebInvoke(Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        public ResResultModel Login(string appKey, string userName, string password, string validateCode)
        {
            try
            {
                if (appKey != AppKey) return ResResult.Response(false, "非法请求", "");

                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                {
                    return ResResult.Response(false, "用户名、密码为必需项", "");
                }
                //if (string.IsNullOrWhiteSpace(validateCode))
                //{
                //    return ResResultData(false, "验证码为必需项", "");
                //}

                //var cookieCode = HttpContext.Current.Request.Cookies["Asset_LoginVc"];
                //if(cookieCode == null) return ResResultData(false, "您的设备不支持Cookie或已禁闭Cookie，请务必确保Cookie正常运行", "");

                //string validCode = cookieCode.Value;
                //if (string.IsNullOrWhiteSpace(validCode))
                //{
                //    return ResResultData(false, "您的设备不支持Cookie或已禁闭Cookie，请务必确保Cookie正常运行", "");
                //}
                //AESEncrypt aes = new AESEncrypt();
                //if (validateCode.ToLower() != aes.DecryptString(validCode).ToLower())
                //{
                //    return ResResultData(false, "验证码不正确", "");
                //}

                if (!Membership.ValidateUser(userName, password))
                {
                    return ResResult.Response(false, "登录失败，用户名或密码有误", "");
                }

                //AuthenticationServiceClient authServiceClient = new AuthenticationServiceClient();
                //if (!authServiceClient.Login(userName, password, "", true))
                //{
                //    return ResResultData(false, "登录失败，用户名或密码有误", "");
                //}

                string roleAppend = "";
                var roles = Roles.GetRolesForUser(userName);
                if (roles != null && roles.Length > 0)
                {
                    roleAppend = string.Join(",", roles);
                }

                return ResResult.Response(true, "登录成功", roleAppend);
            }
            catch (Exception ex)
            {
                return ResResult.Response(false, ex.Message, "");
            }
        }

        public static void DoCheckLogin(string appKey, string userName, out object userId)
        {
            //if (appKey != AppKey) throw new ArgumentException("非法请求");
            //if (string.IsNullOrWhiteSpace(userName)) throw new ArgumentException("未登录");
            //userId = GetUserId(userName.Trim());
            //if (userId.Equals(Guid.Empty)) throw new ArgumentException("用户名“" + userName + "”无效");

            userId = GetUserId();
        }

        #endregion

        #region 私有

        #endregion
    }
}
