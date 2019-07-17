using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class Menus
    {
        #region Menus Member

        public IList<MenusInfo> GetUserMenuAccessList(object userId,string[] userRoles)
        {
            return dal.GetUserMenuAccessList(userId, userRoles);
        }

        public IList<MenusInfo> GetListByParentName(string parentName)
        {
            return dal.GetListByParentName(parentName);
        }

        public int InsertByOutput(MenusInfo model)
        {
            return dal.InsertByOutput(model);
        }

        //public void GetTreeList(List<MenusInfo> listSource,object parentId,ref List<JeasyuiTreeInfo> treeList)
        //{
        //    foreach (var item in listSource)
        //    {
        //        var childList = listSource.FindAll(m => m.ParentId.Equals(item.Id));
        //        var state = childList != null ? "closed" : "open";
        //        var hasChild = state == "closed";

        //        var attributesInfo = new JeasyuiTreeAttributesInfo { Url = item.Url, HasChild = hasChild };

        //        var childTreeList = new List<JeasyuiTreeInfo>();
        //        if (childList != null && childList.Count > 0)
        //        {
        //            foreach (var qi in childList)
        //            {
        //                childTreeList.Add(new JeasyuiTreeInfo { id = qi.Id, text = qi.Title, state = state, attributes = attributesInfo, children = childrenList })
        //            }
        //            //GetTreeList(listSource,item.Id,ref treeList)
        //        }

        //        treeList.Add(new JeasyuiTreeInfo { id = item.Id, text = item.Title, state = state, attributes = attributesInfo, children = childTreeList });
        //    }
        //}

        public string GetTreeJson(string appName)
        {
            StringBuilder jsonAppend = new StringBuilder();
            var list = GetMenus(appName).ToList<MenusInfo>();
            if (list != null && list.Count > 0)
            {
                CreateTreeJson(list, Guid.Empty, ref jsonAppend);
            }
            else
            {
                jsonAppend.Append("[{\"id\":\"" + Guid.Empty + "\",\"text\":\"请选择\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + Guid.Empty + "\",\"parentName\":\"请选择\"}}]");
            }

            return jsonAppend.ToString();
        }

        public void CreateTreeJson(List<MenusInfo> list, object parentId, ref StringBuilder jsonAppend)
        {
            jsonAppend.Append("[");
            var childList = list.FindAll(x => x.ParentId.Equals(parentId));
            if (childList.Count > 0)
            {
                int index = 0;
                foreach (var model in childList)
                {
                    var hasChild = list.Any(r => r.ParentId.Equals(model.Id));
                    var state = hasChild ? "closed" : "open";
                    jsonAppend.Append("{\"id\":\"" + model.Id + "\",\"text\":\"" + model.Title + "\",\"state\":\""+ state + "\",\"attributes\":{\"ParentId\":\"" + model.ParentId + "\",\"Url\":\"" + model.Url + "\"}");
                    if (hasChild)
                    {
                        jsonAppend.Append(",\"children\":");
                        CreateTreeJson(list, model.Id, ref jsonAppend);
                    }
                    jsonAppend.Append("}");
                    if (index < childList.Count - 1) jsonAppend.Append(",");
                    index++;
                }
            }
            jsonAppend.Append("]");
        }

        public string GetTreeGridJson(string appName, object roleId, object userId)
        {
            StringBuilder jsonAppend = new StringBuilder();
            var list = GetListForMenuAccess(appName, roleId, userId).ToList<MenusInfo>();
            if (list != null && list.Count > 0)
            {
                var isRole = roleId != null;
                CreateTreeGridJson(list, Guid.Empty, ref jsonAppend, isRole);
            }
            else
            {
                jsonAppend.Append("[{\"Id\":\"" + Guid.Empty + "\",\"Title\":\"请选择\",\"IsAllowRole\":\"0\",\"IsDenyUser\":\"0\",\"IsView\":\"0\",\"IsAdd\":\"0\",\"IsEdit\":\"0\",\"IsDel\":\"0\"}]");
            }

            return jsonAppend.ToString();
        }

        private void CreateTreeGridJson(List<MenusInfo> list, object parentId, ref StringBuilder jsonAppend,bool isRole)
        {
            jsonAppend.Append("[");
            var childList = list.FindAll(x => x.ParentId.Equals(parentId));
            if (childList.Count > 0)
            {
                int index = 0;
                foreach (var model in childList)
                {
                    var isView = model.IsView ? 1 : 0;
                    var isAdd = model.IsAdd ? 1 : 0;
                    var isEdit = model.IsEdit ? 1 : 0;
                    var isDelete = model.IsDelete ? 1 : 0;
                    jsonAppend.Append("{\"Id\":\"" + model.Id + "\",\"Title\":\"" + model.Title + "\",\"Url\":\"" + model.Url + "\",\"IsView\":\"" + isView + "\",\"IsAdd\":\"" + isAdd + "\",\"IsEdit\":\"" + isEdit + "\",\"IsDel\":\"" + isDelete + "\"");
                    if (list.Any(r => r.ParentId.Equals(model.Id)))
                    {
                        jsonAppend.Append(",\"children\":");
                        CreateTreeGridJson(list, model.Id, ref jsonAppend, isRole);
                    }
                    jsonAppend.Append("}");
                    if (index < childList.Count - 1) jsonAppend.Append(",");
                    index++;
                }
            }
            jsonAppend.Append("]");
        }

        public IList<MenusInfo> GetMenus(string appName)
        {
            var sqlWhere = @"and ApplicationId = (select ApplicationId from TygaSoftAspnetDb.dbo.aspnet_Applications 
                           where LOWER(@AppName) = LoweredApplicationName) ";
            var parm = new SqlParameter("@AppName", SqlDbType.NVarChar, 256);
            parm.Value = appName;
            return dal.GetList(sqlWhere, parm);
        }

        public IList<MenusInfo> GetListForMenuAccess(string appName, object allowRoleId, object denyUserId)
        {
            var sqlWhere = @"and ApplicationId = (select ApplicationId from TygaSoftAspnetDb.dbo.aspnet_Applications 
                           where LOWER(@AppName) = LoweredApplicationName) ";
            var parm = new SqlParameter("@AppName", SqlDbType.NVarChar, 256);
            parm.Value = appName;
            return dal.GetListForMenuAccess(allowRoleId, denyUserId, sqlWhere, parm);
        }

        #endregion
    }
}
