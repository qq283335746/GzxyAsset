using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IMenus
    {
        #region IMenus Member

        IList<MenusInfo> GetListByParentName(string parentName);

        IList<MenusInfo> GetUserMenuAccessList(object userId, string[] userRoles);

        IList<MenusInfo> GetListForMenuAccess(object allowRoleId, object denyUserId, string sqlWhere, params SqlParameter[] cmdParms);

        int InsertByOutput(MenusInfo model);

        #endregion
    }
}
