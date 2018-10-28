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
    public partial class Pandian : IPandian
    {
        #region IPandian Member

        public int Insert(PandianInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into Pandian (Named,AllowUsers,CreateDate,UserId,TotalQty,Status,IsDown,Remark,LastUpdatedDate)
			            values
						(@Named,@AllowUsers,@CreateDate,@UserId,@TotalQty,@Status,@IsDown,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Named",SqlDbType.NVarChar,256),
new SqlParameter("@AllowUsers",SqlDbType.VarChar,1000),
new SqlParameter("@CreateDate",SqlDbType.DateTime),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@TotalQty",SqlDbType.Int),
new SqlParameter("@Status",SqlDbType.NVarChar,20),
new SqlParameter("@IsDown",SqlDbType.Bit),
new SqlParameter("@Remark",SqlDbType.NVarChar,300),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Named;
            parms[1].Value = model.AllowUsers;
            parms[2].Value = model.CreateDate;
            parms[3].Value = model.UserId;
            parms[4].Value = model.TotalQty;
            parms[5].Value = model.Status;
            parms[6].Value = model.IsDown;
            parms[7].Value = model.Remark;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(PandianInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update Pandian set Named = @Named,AllowUsers = @AllowUsers,CreateDate = @CreateDate,UserId = @UserId,TotalQty = @TotalQty,Status = @Status,IsDown = @IsDown,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@Named",SqlDbType.NVarChar,256),
new SqlParameter("@AllowUsers",SqlDbType.VarChar,1000),
new SqlParameter("@CreateDate",SqlDbType.DateTime),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@TotalQty",SqlDbType.Int),
new SqlParameter("@Status",SqlDbType.NVarChar,20),
new SqlParameter("@IsDown",SqlDbType.Bit),
new SqlParameter("@Remark",SqlDbType.NVarChar,300),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.Named;
            parms[2].Value = model.AllowUsers;
            parms[3].Value = model.CreateDate;
            parms[4].Value = model.UserId;
            parms[5].Value = model.TotalQty;
            parms[6].Value = model.Status;
            parms[7].Value = model.IsDown;
            parms[8].Value = model.Remark;
            parms[9].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(object Id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Pandian where Id = @Id");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parm);
        }

        public bool DeleteBatch(IList<object> list)
        {
            bool result = false;
            StringBuilder sb = new StringBuilder(500);
            ParamsHelper parms = new ParamsHelper();
            int n = 0;
            foreach (string item in list)
            {
                n++;
                sb.Append(@"delete from Pandian where Id = @Id" + n + " ;");
                SqlParameter parm = new SqlParameter("@Id" + n + "", SqlDbType.UniqueIdentifier);
                parm.Value = Guid.Parse(item);
                parms.Add(parm);
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.AssetConnString))
            {
                if (conn.State != ConnectionState.Open) conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int effect = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sb.ToString(), parms != null ? parms.ToArray() : null);
                        tran.Commit();
                        if (effect > 0) result = true;
                    }
                    catch
                    {
                        tran.Rollback();
                    }
                }
            }
            return result;
        }

        public PandianInfo GetModel(object Id)
        {
            PandianInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,Named,AllowUsers,CreateDate,UserId,TotalQty,Status,IsDown,Remark,LastUpdatedDate 
			            from Pandian
						where Id = @Id ");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new PandianInfo();
                        model.Id = reader.GetGuid(0);
                        model.Named = reader.GetString(1);
                        model.AllowUsers = reader.GetString(2);
                        model.CreateDate = reader.GetDateTime(3);
                        model.UserId = reader.GetGuid(4);
                        model.TotalQty = reader.GetInt32(5);
                        model.Status = reader.GetString(6);
                        model.IsDown = reader.GetBoolean(7);
                        model.Remark = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);
                    }
                }
            }

            return model;
        }

        public IList<PandianInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from Pandian ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PandianInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,Named,AllowUsers,CreateDate,UserId,TotalQty,Status,IsDown,Remark,LastUpdatedDate
					  from Pandian ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PandianInfo> list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianInfo model = new PandianInfo();
                        model.Id = reader.GetGuid(1);
                        model.Named = reader.GetString(2);
                        model.AllowUsers = reader.GetString(3);
                        model.CreateDate = reader.GetDateTime(4);
                        model.UserId = reader.GetGuid(5);
                        model.TotalQty = reader.GetInt32(6);
                        model.Status = reader.GetString(7);
                        model.IsDown = reader.GetBoolean(8);
                        model.Remark = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,Named,AllowUsers,CreateDate,UserId,TotalQty,Status,IsDown,Remark,LastUpdatedDate
					   from Pandian ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PandianInfo> list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianInfo model = new PandianInfo();
                        model.Id = reader.GetGuid(1);
                        model.Named = reader.GetString(2);
                        model.AllowUsers = reader.GetString(3);
                        model.CreateDate = reader.GetDateTime(4);
                        model.UserId = reader.GetGuid(5);
                        model.TotalQty = reader.GetInt32(6);
                        model.Status = reader.GetString(7);
                        model.IsDown = reader.GetBoolean(8);
                        model.Remark = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,Named,AllowUsers,CreateDate,UserId,TotalQty,Status,IsDown,Remark,LastUpdatedDate
                        from Pandian ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            IList<PandianInfo> list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianInfo model = new PandianInfo();
                        model.Id = reader.GetGuid(0);
                        model.Named = reader.GetString(1);
                        model.AllowUsers = reader.GetString(2);
                        model.CreateDate = reader.GetDateTime(3);
                        model.UserId = reader.GetGuid(4);
                        model.TotalQty = reader.GetInt32(5);
                        model.Status = reader.GetString(6);
                        model.IsDown = reader.GetBoolean(7);
                        model.Remark = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,Named,AllowUsers,CreateDate,UserId,TotalQty,Status,IsDown,Remark,LastUpdatedDate 
			            from Pandian
					    order by LastUpdatedDate desc ");

            IList<PandianInfo> list = new List<PandianInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianInfo model = new PandianInfo();
                        model.Id = reader.GetGuid(0);
                        model.Named = reader.GetString(1);
                        model.AllowUsers = reader.GetString(2);
                        model.CreateDate = reader.GetDateTime(3);
                        model.UserId = reader.GetGuid(4);
                        model.TotalQty = reader.GetInt32(5);
                        model.Status = reader.GetString(6);
                        model.IsDown = reader.GetBoolean(7);
                        model.Remark = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
