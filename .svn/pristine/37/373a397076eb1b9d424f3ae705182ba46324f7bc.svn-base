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
    public partial class RoleMenu : IRoleMenu
    {
        #region IRoleMenu Member

        public int Insert(RoleMenuInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into RoleMenu (RoleId,MenuId,OperationAccess)
			            values
						(@RoleId,@MenuId,@OperationAccess)
			            ");

            SqlParameter[] parms = {
                                        new SqlParameter("@RoleId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@MenuId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OperationAccess",SqlDbType.VarChar,300)
                                   };
            parms[0].Value = model.RoleId;
            parms[1].Value = model.MenuId;
            parms[2].Value = model.OperationAccess;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(RoleMenuInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update RoleMenu set OperationAccess = @OperationAccess 
			            where RoleId = @RoleId and MenuId = @MenuId
					    ");

            SqlParameter[] parms = {
                                        new SqlParameter("@RoleId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@MenuId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@OperationAccess",SqlDbType.VarChar,300)
                                   };
            parms[0].Value = model.RoleId;
            parms[1].Value = model.MenuId;
            parms[2].Value = model.OperationAccess;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(Guid roleId, Guid menuId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from RoleMenu where RoleId = @RoleId and MenuId = @MenuId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@RoleId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@MenuId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = roleId;
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
                sb.Append(@"delete from RoleMenu where RoleId = @RoleId" + n + " ;");
                SqlParameter parm = new SqlParameter("@RoleId" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null) > 0;
        }

        public RoleMenuInfo GetModel(Guid roleId, Guid menuId)
        {
            RoleMenuInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 RoleId,MenuId,OperationAccess 
			            from RoleMenu
						where RoleId = @RoleId and MenuId = @MenuId ");
            SqlParameter[] parms = {
                                     new SqlParameter("@RoleId",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@MenuId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = roleId;
            parms[1].Value = menuId;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new RoleMenuInfo();
                        model.RoleId = reader.GetGuid(0);
                        model.MenuId = reader.GetGuid(1);
                        model.OperationAccess = reader.GetString(2);
                    }
                }
            }

            return model;
        }

        public IList<RoleMenuInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select count(*) from RoleMenu ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<RoleMenuInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          RoleId,MenuId,OperationAccess
					  from RoleMenu ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<RoleMenuInfo> list = new List<RoleMenuInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RoleMenuInfo model = new RoleMenuInfo();
                        model.RoleId = reader.GetGuid(1);
                        model.MenuId = reader.GetGuid(2);
                        model.OperationAccess = reader.GetString(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<RoleMenuInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           RoleId,MenuId,OperationAccess
					   from RoleMenu ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<RoleMenuInfo> list = new List<RoleMenuInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RoleMenuInfo model = new RoleMenuInfo();
                        model.RoleId = reader.GetGuid(1);
                        model.MenuId = reader.GetGuid(2);
                        model.OperationAccess = reader.GetString(3);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<RoleMenuInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(500);
            sb.Append(@"select RoleId,MenuId,OperationAccess
                        from RoleMenu ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            IList<RoleMenuInfo> list = new List<RoleMenuInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RoleMenuInfo model = new RoleMenuInfo();
                        model.RoleId = reader.GetGuid(0);
                        model.MenuId = reader.GetGuid(1);
                        model.OperationAccess = reader.GetString(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<RoleMenuInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select RoleId,MenuId,OperationAccess 
			            from RoleMenu
					    order by LastUpdatedDate desc ");

            IList<RoleMenuInfo> list = new List<RoleMenuInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RoleMenuInfo model = new RoleMenuInfo();
                        model.RoleId = reader.GetGuid(0);
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
