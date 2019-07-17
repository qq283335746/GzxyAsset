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
    public partial class UseRefund : IUseRefund
    {
        #region IUseRefund Member

        public int Insert(UseRefundInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into UseRefund (UsePerson,UseTime,EstimateRefundTime,UseUser,RealRefundTime,RefundDealUser,Status,Remark,LastUpdatedDate)
			            values
						(@UsePerson,@UseTime,@EstimateRefundTime,@UseUser,@RealRefundTime,@RefundDealUser,@Status,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UsePerson",SqlDbType.NVarChar,50),
new SqlParameter("@UseTime",SqlDbType.DateTime),
new SqlParameter("@EstimateRefundTime",SqlDbType.DateTime),
new SqlParameter("@UseUser",SqlDbType.NVarChar,50),
new SqlParameter("@RealRefundTime",SqlDbType.DateTime),
new SqlParameter("@RefundDealUser",SqlDbType.NVarChar,50),
new SqlParameter("@Status",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,300),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.UsePerson;
            parms[1].Value = model.UseTime;
            parms[2].Value = model.EstimateRefundTime;
            parms[3].Value = model.UseUser;
            parms[4].Value = model.RealRefundTime;
            parms[5].Value = model.RefundDealUser;
            parms[6].Value = model.Status;
            parms[7].Value = model.Remark;
            parms[8].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(UseRefundInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update UseRefund set UsePerson = @UsePerson,UseTime = @UseTime,EstimateRefundTime = @EstimateRefundTime,UseUser = @UseUser,RealRefundTime = @RealRefundTime,RefundDealUser = @RefundDealUser,Status = @Status,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@UsePerson",SqlDbType.NVarChar,50),
new SqlParameter("@UseTime",SqlDbType.DateTime),
new SqlParameter("@EstimateRefundTime",SqlDbType.DateTime),
new SqlParameter("@UseUser",SqlDbType.NVarChar,50),
new SqlParameter("@RealRefundTime",SqlDbType.DateTime),
new SqlParameter("@RefundDealUser",SqlDbType.NVarChar,50),
new SqlParameter("@Status",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,300),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.UsePerson;
            parms[2].Value = model.UseTime;
            parms[3].Value = model.EstimateRefundTime;
            parms[4].Value = model.UseUser;
            parms[5].Value = model.RealRefundTime;
            parms[6].Value = model.RefundDealUser;
            parms[7].Value = model.Status;
            parms[8].Value = model.Remark;
            parms[9].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(object Id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from UseRefund where Id = @Id");
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
                sb.Append(@"delete from UseRefund where Id = @Id" + n + " ;");
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

        public UseRefundInfo GetModel(object Id)
        {
            UseRefundInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,UsePerson,UseTime,EstimateRefundTime,UseUser,RealRefundTime,RefundDealUser,Status,Remark,LastUpdatedDate 
			            from UseRefund
						where Id = @Id ");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new UseRefundInfo();
                        model.Id = reader.GetGuid(0);
                        model.UsePerson = reader.GetString(1);
                        model.UseTime = reader.GetDateTime(2);
                        model.EstimateRefundTime = reader.GetDateTime(3);
                        model.UseUser = reader.GetString(4);
                        model.RealRefundTime = reader.GetDateTime(5);
                        model.RefundDealUser = reader.GetString(6);
                        model.Status = reader.GetString(7);
                        model.Remark = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);
                    }
                }
            }

            return model;
        }

        public IList<UseRefundInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from UseRefund ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<UseRefundInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,UsePerson,UseTime,EstimateRefundTime,UseUser,RealRefundTime,RefundDealUser,Status,Remark,LastUpdatedDate
					  from UseRefund ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<UseRefundInfo> list = new List<UseRefundInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UseRefundInfo model = new UseRefundInfo();
                        model.Id = reader.GetGuid(1);
                        model.UsePerson = reader.GetString(2);
                        model.UseTime = reader.GetDateTime(3);
                        model.EstimateRefundTime = reader.GetDateTime(4);
                        model.UseUser = reader.GetString(5);
                        model.RealRefundTime = reader.GetDateTime(6);
                        model.RefundDealUser = reader.GetString(7);
                        model.Status = reader.GetString(8);
                        model.Remark = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<UseRefundInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,UsePerson,UseTime,EstimateRefundTime,UseUser,RealRefundTime,RefundDealUser,Status,Remark,LastUpdatedDate
					   from UseRefund ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<UseRefundInfo> list = new List<UseRefundInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UseRefundInfo model = new UseRefundInfo();
                        model.Id = reader.GetGuid(1);
                        model.UsePerson = reader.GetString(2);
                        model.UseTime = reader.GetDateTime(3);
                        model.EstimateRefundTime = reader.GetDateTime(4);
                        model.UseUser = reader.GetString(5);
                        model.RealRefundTime = reader.GetDateTime(6);
                        model.RefundDealUser = reader.GetString(7);
                        model.Status = reader.GetString(8);
                        model.Remark = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<UseRefundInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,UsePerson,UseTime,EstimateRefundTime,UseUser,RealRefundTime,RefundDealUser,Status,Remark,LastUpdatedDate
                        from UseRefund ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            IList<UseRefundInfo> list = new List<UseRefundInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UseRefundInfo model = new UseRefundInfo();
                        model.Id = reader.GetGuid(0);
                        model.UsePerson = reader.GetString(1);
                        model.UseTime = reader.GetDateTime(2);
                        model.EstimateRefundTime = reader.GetDateTime(3);
                        model.UseUser = reader.GetString(4);
                        model.RealRefundTime = reader.GetDateTime(5);
                        model.RefundDealUser = reader.GetString(6);
                        model.Status = reader.GetString(7);
                        model.Remark = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<UseRefundInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,UsePerson,UseTime,EstimateRefundTime,UseUser,RealRefundTime,RefundDealUser,Status,Remark,LastUpdatedDate 
			            from UseRefund
					    order by LastUpdatedDate desc ");

            IList<UseRefundInfo> list = new List<UseRefundInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        UseRefundInfo model = new UseRefundInfo();
                        model.Id = reader.GetGuid(0);
                        model.UsePerson = reader.GetString(1);
                        model.UseTime = reader.GetDateTime(2);
                        model.EstimateRefundTime = reader.GetDateTime(3);
                        model.UseUser = reader.GetString(4);
                        model.RealRefundTime = reader.GetDateTime(5);
                        model.RefundDealUser = reader.GetString(6);
                        model.Status = reader.GetString(7);
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
