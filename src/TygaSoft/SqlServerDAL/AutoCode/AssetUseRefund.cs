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
    public partial class AssetUseRefund : IAssetUseRefund
    {
        #region IAssetUseRefund Member

        public int Insert(AssetUseRefundInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into AssetUseRefund (UseRefundId,AssetId)
			            values
						(@UseRefundId,@AssetId)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@UseRefundId",SqlDbType.UniqueIdentifier),
                                       new SqlParameter("@AssetId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = model.UseRefundId;
            parms[1].Value = model.AssetId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(AssetUseRefundInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update AssetUseRefund set AssetId = @AssetId 
			            where UseRefundId = @UseRefundId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@UseRefundId",SqlDbType.UniqueIdentifier),
new SqlParameter("@AssetId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = model.UseRefundId;
            parms[1].Value = model.AssetId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(object UseRefundId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from AssetUseRefund where UseRefundId = @UseRefundId");
            SqlParameter parm = new SqlParameter("@UseRefundId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(UseRefundId.ToString());

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
                sb.Append(@"delete from AssetUseRefund where UseRefundId = @UseRefundId" + n + " ;");
                SqlParameter parm = new SqlParameter("@UseRefundId" + n + "", SqlDbType.UniqueIdentifier);
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

        public AssetUseRefundInfo GetModel(object UseRefundId)
        {
            AssetUseRefundInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 UseRefundId,AssetId 
			            from AssetUseRefund
						where UseRefundId = @UseRefundId ");
            SqlParameter parm = new SqlParameter("@UseRefundId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(UseRefundId.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new AssetUseRefundInfo();
                        model.UseRefundId = reader.GetGuid(0);
                        model.AssetId = reader.GetGuid(1);
                    }
                }
            }

            return model;
        }

        public IList<AssetUseRefundInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from AssetUseRefund ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<AssetUseRefundInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          UseRefundId,AssetId
					  from AssetUseRefund ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<AssetUseRefundInfo> list = new List<AssetUseRefundInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AssetUseRefundInfo model = new AssetUseRefundInfo();
                        model.UseRefundId = reader.GetGuid(1);
                        model.AssetId = reader.GetGuid(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AssetUseRefundInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           UseRefundId,AssetId
					   from AssetUseRefund ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<AssetUseRefundInfo> list = new List<AssetUseRefundInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AssetUseRefundInfo model = new AssetUseRefundInfo();
                        model.UseRefundId = reader.GetGuid(1);
                        model.AssetId = reader.GetGuid(2);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AssetUseRefundInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select UseRefundId,AssetId
                        from AssetUseRefund ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            IList<AssetUseRefundInfo> list = new List<AssetUseRefundInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AssetUseRefundInfo model = new AssetUseRefundInfo();
                        model.UseRefundId = reader.GetGuid(0);
                        model.AssetId = reader.GetGuid(1);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AssetUseRefundInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select UseRefundId,AssetId 
			            from AssetUseRefund
					    order by LastUpdatedDate desc ");

            IList<AssetUseRefundInfo> list = new List<AssetUseRefundInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AssetUseRefundInfo model = new AssetUseRefundInfo();
                        model.UseRefundId = reader.GetGuid(0);
                        model.AssetId = reader.GetGuid(1);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
