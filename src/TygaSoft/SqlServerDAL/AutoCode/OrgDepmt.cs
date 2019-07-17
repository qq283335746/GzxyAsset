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
    public partial class OrgDepmt : IOrgDepmt
    {
        #region IOrgDepmt Member

        public int Insert(OrgDepmtInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into OrgDepmt (CompanyId,ParentId,Coded,Named,IdStep,CodeStep,Sort,Remark,LastUpdatedDate,UserId)
			            values
						(@CompanyId,@ParentId,@Coded,@Named,@IdStep,@CodeStep,@Sort,@Remark,@LastUpdatedDate,@UserId)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@CompanyId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,50),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@IdStep",SqlDbType.VarChar,1000),
new SqlParameter("@CodeStep",SqlDbType.VarChar,1000),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,50),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = model.CompanyId;
            parms[1].Value = model.ParentId;
            parms[2].Value = model.Coded;
            parms[3].Value = model.Named;
            parms[4].Value = model.IdStep;
            parms[5].Value = model.CodeStep;
            parms[6].Value = model.Sort;
            parms[7].Value = model.Remark;
            parms[8].Value = model.LastUpdatedDate;
            parms[9].Value = model.UserId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(OrgDepmtInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update OrgDepmt set CompanyId = @CompanyId,ParentId = @ParentId,Coded = @Coded,Named = @Named,IdStep = @IdStep,CodeStep = @CodeStep,Sort = @Sort,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate,UserId = @UserId 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@CompanyId",SqlDbType.UniqueIdentifier),
new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Coded",SqlDbType.VarChar,50),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@IdStep",SqlDbType.VarChar,1000),
new SqlParameter("@CodeStep",SqlDbType.VarChar,1000),
new SqlParameter("@Sort",SqlDbType.Int),
new SqlParameter("@Remark",SqlDbType.NVarChar,50),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.CompanyId;
            parms[2].Value = model.ParentId;
            parms[3].Value = model.Coded;
            parms[4].Value = model.Named;
            parms[5].Value = model.IdStep;
            parms[6].Value = model.CodeStep;
            parms[7].Value = model.Sort;
            parms[8].Value = model.Remark;
            parms[9].Value = model.LastUpdatedDate;
            parms[10].Value = model.UserId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(object Id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from OrgDepmt where Id = @Id");
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
                sb.Append(@"delete from OrgDepmt where Id = @Id" + n + " ;");
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

        public OrgDepmtInfo GetModel(object Id)
        {
            OrgDepmtInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,CompanyId,ParentId,Coded,Named,IdStep,CodeStep,Sort,Remark,LastUpdatedDate,UserId 
			            from OrgDepmt
						where Id = @Id ");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new OrgDepmtInfo();
                        model.Id = reader.GetGuid(0);
                        model.CompanyId = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.Coded = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.IdStep = reader.GetString(5);
                        model.CodeStep = reader.GetString(6);
                        model.Sort = reader.GetInt32(7);
                        model.Remark = reader.GetString(8);
                        model.LastUpdatedDate = reader.GetDateTime(9);
                        model.UserId = reader.GetGuid(10);
                    }
                }
            }

            return model;
        }

        public IList<OrgDepmtInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from OrgDepmt ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<OrgDepmtInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,CompanyId,ParentId,Coded,Named,IdStep,CodeStep,Sort,Remark,LastUpdatedDate,UserId
					  from OrgDepmt ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrgDepmtInfo> list = new List<OrgDepmtInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrgDepmtInfo model = new OrgDepmtInfo();
                        model.Id = reader.GetGuid(1);
                        model.CompanyId = reader.GetGuid(2);
                        model.ParentId = reader.GetGuid(3);
                        model.Coded = reader.GetString(4);
                        model.Named = reader.GetString(5);
                        model.IdStep = reader.GetString(6);
                        model.CodeStep = reader.GetString(7);
                        model.Sort = reader.GetInt32(8);
                        model.Remark = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);
                        model.UserId = reader.GetGuid(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrgDepmtInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,CompanyId,ParentId,Coded,Named,IdStep,CodeStep,Sort,Remark,LastUpdatedDate,UserId
					   from OrgDepmt ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<OrgDepmtInfo> list = new List<OrgDepmtInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrgDepmtInfo model = new OrgDepmtInfo();
                        model.Id = reader.GetGuid(1);
                        model.CompanyId = reader.GetGuid(2);
                        model.ParentId = reader.GetGuid(3);
                        model.Coded = reader.GetString(4);
                        model.Named = reader.GetString(5);
                        model.IdStep = reader.GetString(6);
                        model.CodeStep = reader.GetString(7);
                        model.Sort = reader.GetInt32(8);
                        model.Remark = reader.GetString(9);
                        model.LastUpdatedDate = reader.GetDateTime(10);
                        model.UserId = reader.GetGuid(11);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<OrgDepmtInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,CompanyId,ParentId,Coded,Named,IdStep,CodeStep,Sort,Remark,LastUpdatedDate,UserId
                        from OrgDepmt ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            IList<OrgDepmtInfo> list = new List<OrgDepmtInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrgDepmtInfo model = new OrgDepmtInfo();
                        model.Id = reader.GetGuid(0);
                        model.CompanyId = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.Coded = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.IdStep = reader.GetString(5);
                        model.CodeStep = reader.GetString(6);
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

        public IList<OrgDepmtInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,CompanyId,ParentId,Coded,Named,IdStep,CodeStep,Sort,Remark,LastUpdatedDate,UserId 
			            from OrgDepmt
					    order by LastUpdatedDate desc ");

            IList<OrgDepmtInfo> list = new List<OrgDepmtInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        OrgDepmtInfo model = new OrgDepmtInfo();
                        model.Id = reader.GetGuid(0);
                        model.CompanyId = reader.GetGuid(1);
                        model.ParentId = reader.GetGuid(2);
                        model.Coded = reader.GetString(3);
                        model.Named = reader.GetString(4);
                        model.IdStep = reader.GetString(5);
                        model.CodeStep = reader.GetString(6);
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

        #endregion
    }
}
