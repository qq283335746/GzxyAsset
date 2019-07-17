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
    public partial class UserMenu
    {
        private static readonly IUserMenu dal = DataAccess.CreateUserMenu();

        #region UserMenu Member

        public int Insert(UserMenuInfo model)
        {
            return dal.Insert(model);
        }

        public int Update(UserMenuInfo model)
        {
            return dal.Update(model);
        }

        public int Delete(Guid userId,Guid menuId)
        {
            return dal.Delete(userId,menuId);
        }

        public bool DeleteBatch(IList<object> list)
        {
            return dal.DeleteBatch(list);
        }

        public UserMenuInfo GetModel(Guid userId,Guid menuId)
        {
            return dal.GetModel(userId,menuId);
        }

        public IList<UserMenuInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<UserMenuInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(pageIndex, pageSize, sqlWhere, cmdParms);
        }

        public IList<UserMenuInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetList(sqlWhere, cmdParms);
        }

        public IList<UserMenuInfo> GetList()
        {
            return dal.GetList();
        }

        #endregion
    }
}
