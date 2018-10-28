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
    public partial class Pandian
    {
        #region IPandian Member

        public int UpdateIsDown(object Id)
        {
            var cmdText = @"update Pandian Set IsDown = @IsDown where Id = @Id ";

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
                                     new SqlParameter("@IsDown",SqlDbType.Bit)
                                   };
            parms[0].Value = Id;
            parms[1].Value = true;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, cmdText, parms);
        }

        public int[] GetTotal()
        {
            var cmdText = @"select count(1) as Total from Pandian  
                union all select count(1) as Total from Pandian where Status = '已完成' 
                union all select count(1) as Total from Pandian where Status = '未完成' ";

            var list = new List<int>(3);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, cmdText))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        list.Add(reader.GetInt32(0));
                    }
                }
            }

            return list.ToArray();
        }

        public int InsertByOutput(PandianInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into Pandian (Id,Named,AllowUsers,CreateDate,UserId,TotalQty,Status,IsDown,Remark,LastUpdatedDate)
			            values
						(@Id,@Named,@AllowUsers,@CreateDate,@UserId,@TotalQty,@Status,@IsDown,@Remark,@LastUpdatedDate)
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
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
                                        
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

        public IList<PandianInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from Pandian pd
                        left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = pd.UserId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PandianInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by pd.LastUpdatedDate desc) as RowNumber,
			          pd.Id,pd.Named,pd.AllowUsers,pd.CreateDate,pd.UserId,pd.TotalQty,pd.Status,pd.IsDown,pd.Remark,pd.LastUpdatedDate
                      ,u.UserName
					  from Pandian pd 
                      left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = pd.UserId
                      ");
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
                        model.UserName = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.SCreateDate = model.CreateDate.ToString("yyyy-MM-dd");

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
