using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IMenus
    {
        #region IMenus Member

        int Insert(MenusInfo model);

        int Update(MenusInfo model);

        int Delete(Guid id);

        bool DeleteBatch(IList<object> list);

        MenusInfo GetModel(Guid id);

        IList<MenusInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        IList<MenusInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms);

        IList<MenusInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms);

        IList<MenusInfo> GetList();

        #endregion
    }
}
