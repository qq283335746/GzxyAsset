using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class RoleMenu
    {
        #region IRoleMenu Member

        public int DeleteByMenuId(Guid menuId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from RoleMenu where MenuId = @MenuId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@MenuId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = menuId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        #endregion
    }
}
