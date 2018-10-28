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
    public partial class Region : IRegion
    {
        #region IRegion Member

        public int Insert(RegionInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into Region (ParentId,Coded,Named,Remark,Sort,LastUpdatedDate,UserId)
			            values
						(@ParentId,@Coded,@Named,@Remark,@Sort,@LastUpdatedDate,@UserId)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,50),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@Remark",SqlDbType.NVarChar,50),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = model.ParentId;
            parms[1].Value = model.Coded;
            parms[2].Value = model.Named;
            parms[3].Value = model.Remark;
            parms[4].Value = model.Sort;
            parms[5].Value = model.LastUpdatedDate;
            parms[6].Value = model.UserId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(RegionInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update Region set ParentId = @ParentId,Coded = @Coded,Named = @Named,Remark = @Remark,Sort = @Sort,LastUpdatedDate = @LastUpdatedDate,UserId = @UserId 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,50),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@Remark",SqlDbType.NVarChar,50),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.ParentId;
            parms[2].Value = model.Coded;
            parms[3].Value = model.Named;
            parms[4].Value = model.Remark;
            parms[5].Value = model.Sort;
            parms[6].Value = model.LastUpdatedDate;
            parms[7].Value = model.UserId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(object Id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from Region where Id = @Id");
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
                sb.Append(@"delete from Region where Id = @Id" + n + " ;");
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

        public RegionInfo GetModel(object Id)
        {
            RegionInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,ParentId,Coded,Named,Remark,Sort,LastUpdatedDate,UserId 
			            from Region
						where Id = @Id ");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new RegionInfo();
                        model.Id = reader.GetGuid(0);
                        model.ParentId = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.Remark = reader.GetString(4);
                        model.Sort = reader.GetInt32(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);
                        model.UserId = reader.GetGuid(7);
                    }
                }
            }

            return model;
        }

        public IList<RegionInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from Region ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<RegionInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			          Id,ParentId,Coded,Named,Remark,Sort,LastUpdatedDate,UserId
					  from Region ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<RegionInfo> list = new List<RegionInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RegionInfo model = new RegionInfo();
                        model.Id = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.Coded = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.Remark = reader.GetString(5);
                        model.Sort = reader.GetInt32(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);
                        model.UserId = reader.GetGuid(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<RegionInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by Sort) as RowNumber,
			           Id,ParentId,Coded,Named,Remark,Sort,LastUpdatedDate,UserId
					   from Region ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<RegionInfo> list = new List<RegionInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RegionInfo model = new RegionInfo();
                        model.Id = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.Coded = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.Remark = reader.GetString(5);
                        model.Sort = reader.GetInt32(6);
                        model.LastUpdatedDate = reader.GetDateTime(7);
                        model.UserId = reader.GetGuid(8);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<RegionInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,ParentId,Coded,Named,Remark,Sort,LastUpdatedDate,UserId
                        from Region ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.Append("order by Sort ");

            IList<RegionInfo> list = new List<RegionInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RegionInfo model = new RegionInfo();
                        model.Id = reader.GetGuid(0);
                        model.ParentId = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.Remark = reader.GetString(4);
                        model.Sort = reader.GetInt32(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);
                        model.UserId = reader.GetGuid(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<RegionInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,ParentId,Coded,Named,Remark,Sort,LastUpdatedDate,UserId 
			            from Region
					    order by Sort ");

            IList<RegionInfo> list = new List<RegionInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RegionInfo model = new RegionInfo();
                        model.Id = reader.GetGuid(0);
                        model.ParentId = reader.GetGuid(1);
                        model.Coded = reader.GetString(2);
                        model.Named = reader.GetString(3);
                        model.Remark = reader.GetString(4);
                        model.Sort = reader.GetInt32(5);
                        model.LastUpdatedDate = reader.GetDateTime(6);
                        model.UserId = reader.GetGuid(7);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
