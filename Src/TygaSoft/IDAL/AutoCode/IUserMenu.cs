using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IUserMenu
    {
        #region IUserMenu Member

        int Insert(UserMenuInfo model);

        int Update(UserMenuInfo model);

        int Delete(Guid userId, Guid menuId);

        bool DeleteBatch(IList<object> list);

        UserMenuInfo GetModel(Guid userId, Guid menuId);

        IList<UserMenuInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<UserMenuInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<UserMenuInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<UserMenuInfo> GetList();

        #endregion
    }
}
