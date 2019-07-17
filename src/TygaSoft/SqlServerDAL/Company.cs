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
    public partial class Company
    {
        #region ICompany Member

        public int InsertByOutput(CompanyInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into Company (Id,Coded,Named,Address,Phone,TelPhone,Sort,Remark,LastUpdatedDate,UserId)
			            values
						(@Id,@Coded,@Named,@Address,@Phone,@TelPhone,@Sort,@Remark,@LastUpdatedDate,@UserId)
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

        #endregion
    }
}
