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
    public partial class UserMenu : IUserMenu
    {
        #region IUserMenu Member

        public int Insert(UserMenuInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into UserMenu (UserId,MenuId,OperationAccess)
			            values
						(@UserId,@MenuId,@OperationAccess)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@MenuId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OperationAccess",SqlDbType.VarChar,300)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.MenuId;
            parms[2].Value = model.OperationAccess;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(UserMenuInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update UserMenu set OperationAccess = @OperationAccess 
			            where UserId = @UserId and MenuId = @MenuId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@MenuId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OperationAccess",SqlDbType.VarChar,300)
                                   };
            parms[0].Value = model.UserId;
            parms[1].Value = model.MenuId;
            parms[2].Value = model.OperationAccess;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid userId, Guid menuId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from UserMenu where UserId = @UserId and MenuId = @MenuId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@MenuId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = userId;
            parms[1].Value = menuId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public bool DeleteBatch(IList<object> list)
        {
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from UserMenu where UserId = @UserId" + n + " ;");
                SqlParameter parm = new SqlParameter("@UserId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public UserMenuInfo GetModel(Guid userId, Guid menuId)
        {
            UserMenuInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 UserId,MenuId,OperationAccess 
			            from UserMenu
						where UserId = @UserId and MenuId = @MenuId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@MenuId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = userId;
            parms[1].Value = menuId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new UserMenuInfo();
                        model.UserId = reader.GetGuid(0);
                        model.MenuId = reader.GetGuid(1);
                        model.OperationAccess = reader.GetString(2);
                    }
                }
            }

            return model;
        }

        public IList<UserMenuInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from UserMenu ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<UserMenuInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          UserId,MenuId,OperationAccess
					  from UserMenu ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<UserMenuInfo> list = new List<UserMenuInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserMenuInfo model = new UserMenuInfo();
                        model.UserId = reader.GetGuid(1);
                        model.MenuId = reader.GetGuid(2);
                        model.OperationAccess = reader.GetString(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<UserMenuInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           UserId,MenuId,OperationAccess
					   from UserMenu ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<UserMenuInfo> list = new List<UserMenuInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserMenuInfo model = new UserMenuInfo();
                        model.UserId = reader.GetGuid(1);
                        model.MenuId = reader.GetGuid(2);
                        model.OperationAccess = reader.GetString(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<UserMenuInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select UserId,MenuId,OperationAccess
                        from UserMenu ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            IList<UserMenuInfo> list = new List<UserMenuInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserMenuInfo model = new UserMenuInfo();
                        model.UserId = reader.GetGuid(0);
                        model.MenuId = reader.GetGuid(1);
                        model.OperationAccess = reader.GetString(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<UserMenuInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select UserId,MenuId,OperationAccess 
			            from UserMenu
					    order by LastUpdatedDate desc ");

            IList<UserMenuInfo> list = new List<UserMenuInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UserMenuInfo model = new UserMenuInfo();
                        model.UserId = reader.GetGuid(0);
                        model.MenuId = reader.GetGuid(1);
                        model.OperationAccess = reader.GetString(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
