using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;
using TygaSoft.SysHelper;

namespace TygaSoft.SqlServerDAL
{
    public partial class Menus
    {
        #region IMenus Member

        public IList<MenusInfo> GetUserMenuAccessList(object userId, string[] userRoles)
        {
            var sbIn = new StringBuilder(300);
            sbIn.Append("(");
            var index = 0;
            foreach (var item in userRoles)
            {
                if (index > 0) sbIn.Append(",");
                sbIn.AppendFormat("'{0}'", item.ToLower());

                index++;
            }
            sbIn.Append(")");
            var sb = new StringBuilder(2000);
            sb.AppendFormat(@"select Id,ParentId,Title,Url,Descr 
                            ,CHARINDEX(convert(varchar(36),r.RoleId),m.AllowRoles) IsAllowRole
                            ,CHARINDEX('{0}',DenyUsers) IsDenyUser
                            ,CHARINDEX('{2}',rm.OperationAccess) IsAllowAdd,CHARINDEX('{3}',rm.OperationAccess) IsAllowEdit,CHARINDEX('{4}',rm.OperationAccess) IsAllowDelete
                            ,CHARINDEX('{2}',um.OperationAccess) IsDenyAdd,CHARINDEX('{3}',um.OperationAccess) IsDenyEdit,CHARINDEX('{4}',um.OperationAccess) IsDenyDelete
                            from Menus m
                            left join TygaSoftAspnetDb.dbo.aspnet_Roles r on CHARINDEX(convert(varchar(36),r.RoleId),m.AllowRoles) > 0
                            left join RoleMenu rm on rm.RoleId = r.RoleId and rm.MenuId = m.Id
                            left join UserMenu um on um.UserId = '{0}' and um.MenuId = m.Id
                            where 
                            r.LoweredRoleName in {1}
                            order by m.Sort ", userId, sbIn.ToString(), (int)EnumData.EnumOperationAccess.新增, (int)EnumData.EnumOperationAccess.编辑, (int)EnumData.EnumOperationAccess.删除);

            IList<MenusInfo> list = new List<MenusInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MenusInfo model = new MenusInfo();
                        model.Id = reader.GetGuid(0);
                        model.ParentId = reader.GetGuid(1);
                        model.Title = reader.GetString(2);
                        model.Url = reader.GetString(3);
                        model.Descr = reader.GetString(4);

                        var isAllowRole = reader.GetInt32(5);
                        var isDenyUser = reader.GetInt32(6);
                        var isAllowAdd = reader.IsDBNull(7) ? false : reader.GetInt32(7) > 0;
                        var isAllowEdit = reader.IsDBNull(8) ? false : reader.GetInt32(8) > 0;
                        var isAllowDelete = reader.IsDBNull(9) ? false : reader.GetInt32(9) > 0;

                        var isDenyAdd = reader.IsDBNull(10) ? false : reader.GetInt32(10) > 0;
                        var isDenyEdit = reader.IsDBNull(11) ? false : reader.GetInt32(11) > 0;
                        var isDenyDelete = reader.IsDBNull(12) ? false : reader.GetInt32(12) > 0;

                        if (isDenyUser > 0) model.IsView = false;
                        else model.IsView = isAllowRole > 0;

                        if (isDenyAdd) model.IsAdd = false;
                        else model.IsAdd = isAllowAdd;

                        if (isDenyEdit) model.IsEdit = false;
                        else model.IsEdit = isAllowEdit;

                        if (isDenyDelete) model.IsDelete = false;
                        else model.IsDelete = isAllowDelete;

                        list.Add(model);
                    }
                }
            }

            if (list.Count == 0) return list;

            var endList = new List<MenusInfo>();
            var q = list.GroupBy(m => m.Id);
            foreach (var g in q)
            {
                var itemInfo = list.First(m => m.Id.Equals(g.Key));
                var isView = list.Any(m => (m.Id.Equals(g.Key) && m.IsView));
                var isAdd = list.Any(m => (m.Id.Equals(g.Key) && m.IsAdd));
                var isEdit = list.Any(m => (m.Id.Equals(g.Key) && m.IsEdit));
                var isDelete = list.Any(m => (m.Id.Equals(g.Key) && m.IsDelete));

                itemInfo.IsView = isView;
                itemInfo.IsAdd = isAdd;
                itemInfo.IsEdit = isEdit;
                itemInfo.IsDelete = isDelete;

                endList.Add(itemInfo);
            }

            return endList;
        }

        public IList<MenusInfo> GetListByParentName(string parentName)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select m.ApplicationId,m.Id,m.ParentId,m.IdStep,m.Title,m.Url,m.Descr,m.AllowRoles,m.DenyUsers,m.Sort,m.LastUpdatedDate
                        from Menus m 
                        join Menus m2 on CHARINDEX(convert(varchar(36),m2.Id),m.IdStep) > 0
                        and m2.Title = @Title
                        order by m.Sort 
                        ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Title",SqlDbType.NVarChar,20)
                                   };
            parms[0].Value = parentName;

            var list = new List<MenusInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MenusInfo model = new MenusInfo();
                        model.ApplicationId = reader.GetGuid(0);
                        model.Id = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.IdStep = reader.GetString(3);
                        model.Title = reader.GetString(4);
                        model.Url = reader.GetString(5);
                        model.Descr = reader.GetString(6);
                        model.AllowRoles = reader.GetString(7);
                        model.DenyUsers = reader.GetString(8);
                        model.Sort = reader.GetInt32(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<MenusInfo> GetListForMenuAccess(object allowRoleId, object denyUserId, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var roleMenuIndex = string.Format("CHARINDEX('{0}',OperationAccess) IsAdd,CHARINDEX('{1}',OperationAccess) IsEdit,CHARINDEX('{2}',OperationAccess) IsDelete ", (int)EnumData.EnumOperationAccess.新增, (int)EnumData.EnumOperationAccess.编辑, (int)EnumData.EnumOperationAccess.删除);
            StringBuilder sb = new StringBuilder(250);
            sb.AppendFormat(@"select Id,ParentId,Title,Url,Descr,CHARINDEX('{0}',AllowRoles) IsAllowRole,CHARINDEX('{1}',DenyUsers) IsDenyUser {2} 
			            from Menus m ", allowRoleId, denyUserId, ","+ roleMenuIndex + "");
            if (allowRoleId != null)
            {
                sb.AppendFormat("left join RoleMenu on m.Id = MenuId and RoleId = '{0}' ", allowRoleId);
            }
            else
            {
                sb.AppendFormat("left join UserMenu on m.Id = MenuId and UserId = '{0}' ", denyUserId);
            }
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<MenusInfo> list = new List<MenusInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(),cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MenusInfo model = new MenusInfo();
                        model.Id = reader.GetGuid(0);
                        model.ParentId = reader.GetGuid(1);
                        model.Title = reader.GetString(2);
                        model.Url = reader.GetString(3);
                        model.Descr = reader.GetString(4);

                        var isAllowRole = reader.GetInt32(5);
                        var isDenyUser = reader.GetInt32(6);

                        model.IsView = isAllowRole > 0;
                        if (isDenyUser > 0) model.IsView = false;
                        model.IsAdd = reader.IsDBNull(7) ? false : reader.GetInt32(7) > 0;
                        model.IsEdit = reader.IsDBNull(8) ? false : reader.GetInt32(8) > 0;
                        model.IsDelete = reader.IsDBNull(9) ? false : reader.GetInt32(9) > 0;

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public int InsertByOutput(MenusInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into Menus (ApplicationId,Id,ParentId,IdStep,Title,Url,Descr,AllowRoles,DenyUsers,Sort,LastUpdatedDate)
			            values
						(@ApplicationId,@Id,@ParentId,@IdStep,@Title,@Url,@Descr,@AllowRoles,@DenyUsers,@Sort,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@ApplicationId",SqlDbType.UniqueIdentifier),
                                       new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@IdStep",SqlDbType.VarChar,1000),
                                        new SqlParameter("@Title",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Url",SqlDbType.VarChar,256),
                                        new SqlParameter("@Descr",SqlDbType.NVarChar,50),
                                        new SqlParameter("@AllowRoles",SqlDbType.NVarChar,300),
                                        new SqlParameter("@DenyUsers",SqlDbType.NVarChar,300),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.ApplicationId;
            parms[1].Value = model.Id;
            parms[2].Value = model.ParentId;
            parms[3].Value = model.IdStep;
            parms[4].Value = model.Title;
            parms[5].Value = model.Url;
            parms[6].Value = model.Descr;
            parms[7].Value = model.AllowRoles;
            parms[8].Value = model.DenyUsers;
            parms[9].Value = model.Sort;
            parms[10].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        #endregion
    }
}
