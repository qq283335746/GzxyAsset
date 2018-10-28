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
    public partial class Company : ICompany
    {
        #region ICompany Member

        public int Insert(CompanyInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into Company (Coded,Named,Address,Phone,TelPhone,Sort,Remark,LastUpdatedDate,UserId)
			            values
						(@Coded,@Named,@Address,@Phone,@TelPhone,@Sort,@Remark,@LastUpdatedDate,@UserId)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Coded",SqlDbType.VarChar,50),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@Address",SqlDbType.NVarChar,100),
new SqlParameter("@Phone",SqlDbType.VarChar,15),
new SqlParameter("@TelPhone",SqlDbType.VarChar,20),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = model.Coded;
            parms[1].Value = model.Named;
            parms[2].Value = model.Address;
            parms[3].Value = model.Phone;
            parms[4].Value = model.TelPhone;
            parms[5].Value = model.Sort;
            parms[6].Value = model.Remark;
            parms[7].Value = model.LastUpdatedDate;
            parms[8].Value = model.UserId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(CompanyInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update Company set Coded = @Coded,Named = @Named,Address = @Address,Phone = @Phone,TelPhone = @TelPhone,Sort = @Sort,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate,UserId = @UserId 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,50),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@Address",SqlDbType.NVarChar,100),
new SqlParameter("@Phone",SqlDbType.VarChar,15),
new SqlParameter("@TelPhone",SqlDbType.VarChar,20),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.Coded;
            parms[2].Value = model.Named;
            parms[3].Value = model.Address;
            parms[4].Value = model.Phone;
            parms[5].Value = model.TelPhone;
            parms[6].Value = model.Sort;
            parms[7].Value = model.Remark;
            parms[8].Value = model.LastUpdatedDate;
            parms[9].Value = model.UserId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(object Id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Company where Id = @Id");
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
                sb.Append(@"delete from Company where Id = @Id" + n + " ;");
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

        public CompanyInfo GetModel(object Id)
        {
            CompanyInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,Coded,Named,Address,Phone,TelPhone,Sort,Remark,LastUpdatedDate,UserId 
			            from Company
						where Id = @Id ");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new CompanyInfo();
                        model.Id = reader.GetGuid(0);
                        model.Coded = reader.GetString(1);
                        model.Named = reader.GetString(2);
                        model.Address = reader.GetString(3);
                        model.Phone = reader.GetString(4);
                        model.TelPhone = reader.GetString(5);
                        model.Sort = reader.GetInt32(6);
                        model.Remark = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);
                        model.UserId = reader.GetGuid(9);
                    }
                }
            }

            return model;
        }

        public IList<CompanyInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from Company ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<CompanyInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			          Id,Coded,Named,Address,Phone,TelPhone,Sort,Remark,LastUpdatedDate,UserId
					  from Company ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<CompanyInfo> list = new List<CompanyInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CompanyInfo model = new CompanyInfo();
                        model.Id = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.Address = reader.GetString(4);
                        model.Phone = reader.GetString(5);
                        model.TelPhone = reader.GetString(6);
                        model.Sort = reader.GetInt32(7);
                        model.Remark = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);
                        model.UserId = reader.GetGuid(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<CompanyInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			           Id,Coded,Named,Address,Phone,TelPhone,Sort,Remark,LastUpdatedDate,UserId
					   from Company ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<CompanyInfo> list = new List<CompanyInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CompanyInfo model = new CompanyInfo();
                        model.Id = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.Address = reader.GetString(4);
                        model.Phone = reader.GetString(5);
                        model.TelPhone = reader.GetString(6);
                        model.Sort = reader.GetInt32(7);
                        model.Remark = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);
                        model.UserId = reader.GetGuid(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<CompanyInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,Coded,Named,Address,Phone,TelPhone,Sort,Remark,LastUpdatedDate,UserId
                        from Company ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<CompanyInfo> list = new List<CompanyInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CompanyInfo model = new CompanyInfo();
                        model.Id = reader.GetGuid(0);
                        model.Coded = reader.GetString(1);
                        model.Named = reader.GetString(2);
                        model.Address = reader.GetString(3);
                        model.Phone = reader.GetString(4);
                        model.TelPhone = reader.GetString(5);
                        model.Sort = reader.GetInt32(6);
                        model.Remark = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);
                        model.UserId = reader.GetGuid(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<CompanyInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,Coded,Named,Address,Phone,TelPhone,Sort,Remark,LastUpdatedDate,UserId 
			            from Company
					    order by Sort ");

            IList<CompanyInfo> list = new List<CompanyInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CompanyInfo model = new CompanyInfo();
                        model.Id = reader.GetGuid(0);
                        model.Coded = reader.GetString(1);
                        model.Named = reader.GetString(2);
                        model.Address = reader.GetString(3);
                        model.Phone = reader.GetString(4);
                        model.TelPhone = reader.GetString(5);
                        model.Sort = reader.GetInt32(6);
                        model.Remark = reader.GetString(7);
                        model.LastUpdatedDate = reader.GetDateTime(8);
                        model.UserId = reader.GetGuid(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
