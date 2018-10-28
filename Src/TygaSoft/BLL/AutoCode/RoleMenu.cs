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
        private static readonly IRoleMenu dal = DataAccess.CreateRoleMenu();

        #region RoleMenu Member

        public int Insert(RoleMenuInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(RoleMenuInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid roleId, Guid menuId)
        {
            return dal.Delete(roleId, menuId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public RoleMenuInfo GetModel(Guid roleId, Guid menuId)
        {
            return dal.GetModel(roleId, menuId);
        }

        public IList<RoleMenuInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<RoleMenuInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<RoleMenuInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<RoleMenuInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
