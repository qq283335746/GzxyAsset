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
    public partial class Category
    {
        #region ICategory Member

        public CategoryInfo GetModelByCode(string code)
        {
            CategoryInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,ParentId,Coded,Named,Remark,Sort,LastUpdatedDate,UserId 
			            from Category
						where Coded = @Coded ");
            SqlParameter parm = new SqlParameter("@Coded", SqlDbType.VarChar,256);
            parm.Value = code;

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new CategoryInfo();
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

        public int InsertByOutput(CategoryInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into Category (Id,ParentId,Coded,Named,Remark,Sort,LastUpdatedDate,UserId)
			            values
						(@Id,@ParentId,@Coded,@Named,@Remark,@Sort,@LastUpdatedDate,@UserId)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Coded",SqlDbType.VarChar,50),
                                        new SqlParameter("@Named",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Remark",SqlDbType.NVarChar,50),
                                        new SqlParameter("@Sort",SqlDbType.Int),
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
                                        new SqlParameter("@UserId",SqlDbType.UniqueIdentifier),
                                        new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                   };
            parms[0].Value = model.ParentId;
            parms[1].Value = model.Coded;
            parms[2].Value = model.Named;
            parms[3].Value = model.Remark;
            parms[4].Value = model.Sort;
            parms[5].Value = model.LastUpdatedDate;
            parms[6].Value = model.UserId;
            parms[7].Value = model.Id;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        #endregion
    }
}
