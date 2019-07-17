using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;
using TygaSoft.DALFactory;

namespace TygaSoft.BLL
{
    public partial class RoleMenu
    {
        #region RoleMenu Member

        public int DeleteByMenuId(Guid menuId)
        {
            return dal.DeleteByMenuId(menuId);
        }

        #endregion
    }
}
