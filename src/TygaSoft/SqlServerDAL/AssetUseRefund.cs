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
    public partial class AssetUseRefund
    {
        #region IAssetUseRefund Member

        public IList<AssetUseRefundInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from UseRefund ur 
                        left join AssetUseRefund aur on aur.UseRefundId = ur.Id
                        left join AssetInStore ais on ais.Id = aur.AssetId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<AssetUseRefundInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by ur.LastUpdatedDate desc) as RowNumber,
			          ur.Id,ur.UsePerson,ur.UseTime,ur.EstimateRefundTime,ur.UseUser,ur.RealRefundTime,ur.RefundDealUser,ur.Status,ur.Remark
                       ,ais.Id AssetId,ais.Barcode,c.Named CategoryName,ais.Named AssetName,ais.SpecModel,ais.SNCode,ais.Unit,ais.Price,ais.Named OwnedCompanyName
                       ,com.Named UseCompanyName,orgd.Named OrgDepmtName,ais.UsePerson AssetUsePerson,ais.StoreLocation
					  from UseRefund ur
                      left join AssetUseRefund aur on aur.UseRefundId = ur.Id
                      left join AssetInStore ais on ais.Id = aur.AssetId
                      left join Category c on ais.CategoryId = c.Id
                      left join Company com on ais.UseCompanyId = com.Id
                      left join Company com2 on ais.OwnedCompanyId = com2.Id
                      left join OrgDepmt orgd on ais.UseDepmtId = orgd.Id
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<AssetUseRefundInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new AssetUseRefundInfo();
                        model.UseRefundId = reader.GetGuid(1);
                        model.UsePerson = reader.GetString(2);
                        model.SUseTime = reader.GetDateTime(3).ToString("yyyy-MM-dd").Replace("1754-01-01","");
                        model.SEstimateRefundTime = reader.GetDateTime(4).ToString("yyyy-MM-dd").Replace("1754-01-01", "");
                        model.UseUserName = reader.GetString(5);
                        model.SRealRefundTime = reader.GetDateTime(6).ToString("yyyy-MM-dd").Replace("1754-01-01", "");
                        model.RefundDealUserName = reader.GetString(7);
                        model.Status = reader.GetString(8);
                        model.Remark = reader.GetString(9);

                        model.AssetId = reader.IsDBNull(10) ? Guid.Empty : reader.GetGuid(10);
                        model.Barcode = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.CategoryName = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.AssetName = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.SpecModel = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        model.SNCode = reader.IsDBNull(15) ? "" : reader.GetString(15);
                        model.Unit = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.Price = reader.IsDBNull(17) ? 0 : reader.GetDecimal(17);
                        model.OwnedCompany = reader.IsDBNull(18) ? "" : reader.GetString(18);
                        model.UseCompany = reader.IsDBNull(19) ? "" : reader.GetString(19);
                        model.UseCompanyDepmt = reader.IsDBNull(20) ? "" : reader.GetString(20);
                        model.AssetUsePerson = reader.IsDBNull(21) ? "" : reader.GetString(21);
                        model.StoreLocation = reader.IsDBNull(22) ? "" : reader.GetString(22);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public int Delete(object useRefundId,object assetId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from AssetUseRefund where UseRefundId = @UseRefundId and AssetId = @AssetId ");
            SqlParameter[] parms = {
                new SqlParameter("@UseRefundId", SqlDbType.UniqueIdentifier),
                new SqlParameter("@AssetId", SqlDbType.UniqueIdentifier)
            };
            parms[0].Value = Guid.Parse(useRefundId.ToString());
            parms[1].Value = Guid.Parse(assetId.ToString());

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        #endregion
    }
}
