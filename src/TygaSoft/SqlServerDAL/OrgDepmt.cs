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
    public partial class OrgDepmt
    {
        #region IOrgDepmt Member

        public OrgDepmtInfo GetModelByCode(string code)
        {
            OrgDepmtInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,ParentId,Coded,Named,Remark,Sort,LastUpdatedDate,UserId 
			            from OrgDepmt
						where Coded = @Coded ");
            SqlParameter parm = new SqlParameter("@Coded", SqlDbType.VarChar,256);
            parm.Value = code;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new OrgDepmtInfo();
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

        public int InsertByOutput(OrgDepmtInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into OrgDepmt (Id,CompanyId,ParentId,Coded,Named,IdStep,CodeStep,Sort,Remark,LastUpdatedDate,UserId)
			            values
						(@Id,@CompanyId,@ParentId,@Coded,@Named,@IdStep,@CodeStep,@Sort,@Remark,@LastUpdatedDate,@UserId)
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

        #endregion
    }
}
