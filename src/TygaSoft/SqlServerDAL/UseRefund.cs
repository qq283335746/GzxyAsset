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
    public partial class UseRefund
    {
        #region IUseRefund Member

        public int InsertByOutput(UseRefundInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into UseRefund (Id,UsePerson,UseTime,EstimateRefundTime,UseUser,RealRefundTime,RefundDealUser,Status,Remark,LastUpdatedDate)
			            values
						(@Id,@UsePerson,@UseTime,@EstimateRefundTime,@UseUser,@RealRefundTime,@RefundDealUser,@Status,@Remark,@LastUpdatedDate)
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
                                        new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
                                        new SqlParameter("@Id",SqlDbType.UniqueIdentifier)
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
            parms[9].Value = model.Id;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        #endregion
    }
}
