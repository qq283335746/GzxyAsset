using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IRoleMenu
    {
        #region IRoleMenu Member

        int Insert(RoleMenuInfo model);

        int Update(RoleMenuInfo model);

        int Delete(Guid roleId, Guid menuId);

        bool DeleteBatch(IList<object> list);

        RoleMenuInfo GetModel(Guid roleId, Guid menuId);

        IList<RoleMenuInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<RoleMenuInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<RoleMenuInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<RoleMenuInfo> GetList();

        #endregion
    }
}
