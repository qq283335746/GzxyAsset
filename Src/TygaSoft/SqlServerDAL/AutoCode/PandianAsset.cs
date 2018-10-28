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
    public partial class PandianAsset : IPandianAsset
    {
        #region IPandianAsset Member

        public int Insert(PandianAssetInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into PandianAsset (PandianId,AssetId,UpdatedRegionId,UpdatedUseCompanyId,UpdatedUseDepmtId,UpdatedStoreLocation,UpdatedUsePerson,UserId,Status,Remark,LastUpdatedDate)
			            values
						(@PandianId,@AssetId,@UpdatedRegionId,@UpdatedUseCompanyId,@UpdatedUseDepmtId,@UpdatedStoreLocation,@UpdatedUsePerson,@UserId,@Status,@Remark,@LastUpdatedDate)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@AssetId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UpdatedRegionId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UpdatedUseCompanyId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UpdatedUseDepmtId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@UpdatedStoreLocation",SqlDbType.NVarChar,50),
                                        new SqlParameter("@UpdatedUsePerson",SqlDbType.NVarChar,50),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Status",SqlDbType.NVarChar,20),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,300),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
                                        new SqlParameter("@PandianId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = model.AssetId;
            parms[1].Value = model.UpdatedRegionId;
            parms[2].Value = model.UpdatedUseCompanyId;
            parms[3].Value = model.UpdatedUseDepmtId;
            parms[4].Value = model.UpdatedStoreLocation;
            parms[5].Value = model.UpdatedUsePerson;
            parms[6].Value = model.UserId;
            parms[7].Value = model.Status;
            parms[8].Value = model.Remark;
            parms[9].Value = model.LastUpdatedDate;
            parms[10].Value = model.PandianId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(PandianAssetInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update PandianAsset set UpdatedRegionId = @UpdatedRegionId,UpdatedUseCompanyId = @UpdatedUseCompanyId,UpdatedUseDepmtId = @UpdatedUseDepmtId,UpdatedStoreLocation = @UpdatedStoreLocation,UpdatedUsePerson = @UpdatedUsePerson,UserId = @UserId,Status = @Status,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate 
			            where PandianId = @PandianId and AssetId = @AssetId
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@PandianId",SqlDbType.UniqueIdentifier),
new SqlParameter("@AssetId",SqlDbType.UniqueIdentifier),
new SqlParameter("@UpdatedRegionId",SqlDbType.UniqueIdentifier),
new SqlParameter("@UpdatedUseCompanyId",SqlDbType.UniqueIdentifier),
new SqlParameter("@UpdatedUseDepmtId",SqlDbType.UniqueIdentifier),
new SqlParameter("@UpdatedStoreLocation",SqlDbType.NVarChar,50),
new SqlParameter("@UpdatedUsePerson",SqlDbType.NVarChar,50),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Status",SqlDbType.NVarChar,20),
new SqlParameter("@Remark",SqlDbType.NVarChar,300),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime)
                                   };
            parms[0].Value = model.PandianId;
            parms[1].Value = model.AssetId;
            parms[2].Value = model.UpdatedRegionId;
            parms[3].Value = model.UpdatedUseCompanyId;
            parms[4].Value = model.UpdatedUseDepmtId;
            parms[5].Value = model.UpdatedStoreLocation;
            parms[6].Value = model.UpdatedUsePerson;
            parms[7].Value = model.UserId;
            parms[8].Value = model.Status;
            parms[9].Value = model.Remark;
            parms[10].Value = model.LastUpdatedDate;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(object PandianId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from PandianAsset where PandianId = @PandianId");
            SqlParameter parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(PandianId.ToString());

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
                sb.Append(@"delete from PandianAsset where PandianId = @PandianId" + n + " ;");
                SqlParameter parm = new SqlParameter("@PandianId" + n + "", SqlDbType.UniqueIdentifier);
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

        public PandianAssetInfo GetModel(object pandianId,object assetId)
        {
            PandianAssetInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 PandianId,AssetId,UpdatedRegionId,UpdatedUseCompanyId,UpdatedUseDepmtId,UpdatedStoreLocation,UpdatedUsePerson,UserId,Status,Remark,LastUpdatedDate 
			            from PandianAsset
						where PandianId = @PandianId and AssetId = @AssetId ");
            SqlParameter[] parms = {
                new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier),
                new SqlParameter("@AssetId", SqlDbType.UniqueIdentifier)
            };
            parms[0].Value = Guid.Parse(pandianId.ToString());
            parms[1].Value = Guid.Parse(assetId.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new PandianAssetInfo();
                        model.PandianId = reader.GetGuid(0);
                        model.AssetId = reader.GetGuid(1);
                        model.UpdatedRegionId = reader.GetGuid(2);
                        model.UpdatedUseCompanyId = reader.GetGuid(3);
                        model.UpdatedUseDepmtId = reader.GetGuid(4);
                        model.UpdatedStoreLocation = reader.GetString(5);
                        model.UpdatedUsePerson = reader.GetString(6);
                        model.UserId = reader.GetGuid(7);
                        model.Status = reader.GetString(8);
                        model.Remark = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);
                    }
                }
            }

            return model;
        }

        public IList<PandianAssetInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from PandianAsset ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PandianAssetInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          PandianId,AssetId,UpdatedRegionId,UpdatedUseCompanyId,UpdatedUseDepmtId,UpdatedStoreLocation,UpdatedUsePerson,UserId,Status,Remark,LastUpdatedDate
					  from PandianAsset ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PandianAssetInfo> list = new List<PandianAssetInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianAssetInfo model = new PandianAssetInfo();
                        model.PandianId = reader.GetGuid(1);
                        model.AssetId = reader.GetGuid(2);
                        model.UpdatedRegionId = reader.GetGuid(3);
                        model.UpdatedUseCompanyId = reader.GetGuid(4);
                        model.UpdatedUseDepmtId = reader.GetGuid(5);
                        model.UpdatedStoreLocation = reader.GetString(6);
                        model.UpdatedUsePerson = reader.GetString(7);
                        model.UserId = reader.GetGuid(8);
                        model.Status = reader.GetString(9);
                        model.Remark = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianAssetInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           PandianId,AssetId,UpdatedRegionId,UpdatedUseCompanyId,UpdatedUseDepmtId,UpdatedStoreLocation,UpdatedUsePerson,UserId,Status,Remark,LastUpdatedDate
					   from PandianAsset ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<PandianAssetInfo> list = new List<PandianAssetInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianAssetInfo model = new PandianAssetInfo();
                        model.PandianId = reader.GetGuid(1);
                        model.AssetId = reader.GetGuid(2);
                        model.UpdatedRegionId = reader.GetGuid(3);
                        model.UpdatedUseCompanyId = reader.GetGuid(4);
                        model.UpdatedUseDepmtId = reader.GetGuid(5);
                        model.UpdatedStoreLocation = reader.GetString(6);
                        model.UpdatedUsePerson = reader.GetString(7);
                        model.UserId = reader.GetGuid(8);
                        model.Status = reader.GetString(9);
                        model.Remark = reader.GetString(10);
                        model.LastUpdatedDate = reader.GetDateTime(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianAssetInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select PandianId,AssetId,UpdatedRegionId,UpdatedUseCompanyId,UpdatedUseDepmtId,UpdatedStoreLocation,UpdatedUsePerson,UserId,Status,Remark,LastUpdatedDate
                        from PandianAsset ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            IList<PandianAssetInfo> list = new List<PandianAssetInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianAssetInfo model = new PandianAssetInfo();
                        model.PandianId = reader.GetGuid(0);
                        model.AssetId = reader.GetGuid(1);
                        model.UpdatedRegionId = reader.GetGuid(2);
                        model.UpdatedUseCompanyId = reader.GetGuid(3);
                        model.UpdatedUseDepmtId = reader.GetGuid(4);
                        model.UpdatedStoreLocation = reader.GetString(5);
                        model.UpdatedUsePerson = reader.GetString(6);
                        model.UserId = reader.GetGuid(7);
                        model.Status = reader.GetString(8);
                        model.Remark = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianAssetInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select PandianId,AssetId,UpdatedRegionId,UpdatedUseCompanyId,UpdatedUseDepmtId,UpdatedStoreLocation,UpdatedUsePerson,UserId,Status,Remark,LastUpdatedDate 
			            from PandianAsset
					    order by LastUpdatedDate desc ");

            IList<PandianAssetInfo> list = new List<PandianAssetInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianAssetInfo model = new PandianAssetInfo();
                        model.PandianId = reader.GetGuid(0);
                        model.AssetId = reader.GetGuid(1);
                        model.UpdatedRegionId = reader.GetGuid(2);
                        model.UpdatedUseCompanyId = reader.GetGuid(3);
                        model.UpdatedUseDepmtId = reader.GetGuid(4);
                        model.UpdatedStoreLocation = reader.GetString(5);
                        model.UpdatedUsePerson = reader.GetString(6);
                        model.UserId = reader.GetGuid(7);
                        model.Status = reader.GetString(8);
                        model.Remark = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
