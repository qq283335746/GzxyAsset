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
    public partial class PandianAsset
    {
        #region IPandianAsset Member

        public int[] GetTotal(object pandianId)
        {
            var cmdText = @"select count(1) as Total from PandianAsset where PandianId = @PandianId and Status = '已盘点' 
                union all select count(1) as Total from PandianAsset where PandianId = @PandianId and Status = '盘盈' 
                union all select count(1) as Total from PandianAsset where PandianId = @PandianId and Status = '未盘点' ";

            var parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(pandianId.ToString());

            var list = new List<int>(3);

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, cmdText, parm))
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

        public bool IsExist(string barcode)
        {
            var cmdText = @"select 1 from Pandian pd 
                            join PandianAsset pda on pda.PandianId = pd.Id
                            join AssetInStore ais on ais.Id = pda.AssetId
                            where ais.Barcode = @Barcode
                            ";
            var parm = new SqlParameter("@Barcode", SqlDbType.VarChar, 36);
            parm.Value = barcode;

            object obj = SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, cmdText, parm);
            if (obj != null) return true;

            return false;
        }

        public IList<PandianAssetInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder();
            sb.Append(@"select count(*)
                        from Pandian pd 
                          join PandianAsset pda on pda.PandianId = pd.Id
                          left join AssetInStore ais on ais.Id = pda.AssetId
                          left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = pd.UserId
                          left join Region r1 on r1.Id = pda.UpdatedRegionId
                          left join Company cpn on cpn.Id = pda.UpdatedUseCompanyId
                          left join OrgDepmt orgd3 on orgd3.Id = pda.UpdatedUseDepmtId
                          left join OrgDepmt orgd on orgd.Id = ais.UseDepmtId
                          left join Category c on c.Id = ais.CategoryId
                          left join Company cpn2 on cpn2.Id = ais.UseCompanyId
                          left join Company cpn3 on cpn3.Id = ais.OwnedCompanyId
                          left join Region r on r.Id = ais.RegionId
                     ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<PandianAssetInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by pd.LastUpdatedDate desc) as RowNumber,
                      pd.Named
			          ,pda.PandianId,pda.AssetId,pda.UpdatedStoreLocation,pda.UpdatedUsePerson,pda.Status,pda.Remark
                      ,r1.Named UpdatedRegion,cpn.Named UpdatedUseCompany,orgd3.Named UpdatedUseDepmt,u.UserName
                      ,ais.Barcode,ais.Named AssetName,ais.SpecModel,ais.SNCode,ais.Unit,ais.Price,ais.BuyDate,ais.UsePerson,ais.Manager,
                      ais.StoreLocation,ais.UseExpireMonth,ais.Supplier
					  ,c.Named Category,c.Id CategoryId,cpn2.Named UseCompany,orgd.Named UseDepmt,cpn3.Named OwnedCompany,r.Named Region
                      from Pandian pd 
                      join PandianAsset pda on pda.PandianId = pd.Id
                      left join AssetInStore ais on ais.Id = pda.AssetId
                      left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = pd.UserId
                      left join Region r1 on r1.Id = pda.UpdatedRegionId
                      left join Company cpn on cpn.Id = pda.UpdatedUseCompanyId
                      left join OrgDepmt orgd3 on orgd3.Id = pda.UpdatedUseDepmtId
                      left join OrgDepmt orgd on orgd.Id = ais.UseDepmtId
                      left join Category c on c.Id = ais.CategoryId
                      left join Company cpn2 on cpn2.Id = ais.UseCompanyId
                      left join Company cpn3 on cpn3.Id = ais.OwnedCompanyId
                      left join Region r on r.Id = ais.RegionId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            var list = new List<PandianAssetInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PandianAssetInfo model = new PandianAssetInfo();
                        model.Named = reader.GetString(1);
                        model.PandianId = reader.GetGuid(2);
                        model.AssetId = reader.GetGuid(3);
                        model.UpdatedStoreLocation = reader.GetString(4);
                        model.UpdatedUsePerson = reader.GetString(5);
                        model.Status = reader.GetString(6);
                        model.Remark = reader.GetString(7);

                        model.UpdatedRegion = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        model.UpdatedUseCompany = reader.IsDBNull(9) ? "" : reader.GetString(9);
                        model.UpdatedUseDepmt = reader.IsDBNull(10) ? "" : reader.GetString(10);
                        model.UserName = reader.IsDBNull(11) ? "" : reader.GetString(11);

                        model.Barcode = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.AssetName = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.SpecModel = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        model.SNCode = reader.IsDBNull(15) ? "" : reader.GetString(15);
                        model.Unit = reader.IsDBNull(16) ? "" : reader.GetString(16);
                        model.Price = reader.IsDBNull(17) ? 0 : reader.GetDecimal(17);
                        model.SBuyDate = reader.IsDBNull(18) ? "" : reader.GetDateTime(18).ToString("yyyy-MM-dd");
                        model.UsePerson = reader.IsDBNull(19) ? "" : reader.GetString(19);
                        model.Manager = reader.IsDBNull(20) ? "" : reader.GetString(20);
                        model.StoreLocation = reader.IsDBNull(21) ? "" : reader.GetString(21);
                        model.UseExpireMonth = reader.IsDBNull(22) ? 0 : reader.GetInt32(22);
                        model.Supplier = reader.IsDBNull(23) ? "" : reader.GetString(23);
                        model.Category = reader.IsDBNull(24) ? "" : reader.GetString(24);
                        model.CategoryId = reader.IsDBNull(25) ? Guid.Empty : reader.GetGuid(25);
                        model.UseCompany = reader.IsDBNull(26) ? "" : reader.GetString(26);
                        model.UseDepmt = reader.IsDBNull(27) ? "" : reader.GetString(27);
                        model.OwnedCompany = reader.IsDBNull(28) ? "" : reader.GetString(28);
                        model.Region = reader.IsDBNull(29) ? "" : reader.GetString(29);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<PandianAssetInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            var sb = new StringBuilder();

            sb.Append(@"select pd.Named,pda.PandianId,pda.AssetId,pda.UpdatedStoreLocation,pda.UpdatedUsePerson,pda.Status,pda.Remark
                      ,r1.Named UpdatedRegion,cpn.Named UpdatedUseCompany,orgd3.Named UpdatedUseDepmt,u.UserName
                      ,ais.Barcode,ais.Named AssetName,ais.SpecModel,ais.SNCode,ais.Unit,ais.Price,ais.BuyDate,ais.UsePerson,ais.Manager,
                      ais.StoreLocation,ais.UseExpireMonth,ais.Supplier
					  ,c.Named Category,cpn2.Named UseCompany,orgd.Named UseDepmt,cpn3.Named OwnedCompany,r.Named Region
                      from Pandian pd 
                      join PandianAsset pda on pda.PandianId = pd.Id
                      left join AssetInStore ais on ais.Id = pda.AssetId
                      left join TygaSoftAspnetDb.dbo.aspnet_Users u on u.UserId = pd.UserId
                      left join Region r1 on r1.Id = pda.UpdatedRegionId
                      left join Company cpn on cpn.Id = pda.UpdatedUseCompanyId
                      left join OrgDepmt orgd3 on orgd3.Id = pda.UpdatedUseDepmtId
                      left join OrgDepmt orgd on orgd.Id = ais.UseDepmtId
                      left join Category c on c.Id = ais.CategoryId
                      left join Company cpn2 on cpn2.Id = ais.UseCompanyId
                      left join Company cpn3 on cpn3.Id = ais.OwnedCompanyId
                      left join Region r on r.Id = ais.RegionId
                      ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            var list = new List<PandianAssetInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var model = new PandianAssetInfo();
                        model.Named = reader.GetString(0);
                        model.PandianId = reader.GetGuid(1);
                        model.AssetId = reader.GetGuid(2);
                        model.UpdatedStoreLocation = reader.GetString(3);
                        model.UpdatedUsePerson = reader.GetString(4);
                        model.Status = reader.GetString(5);
                        model.Remark = reader.GetString(6);

                        model.UpdatedRegion = reader.IsDBNull(7) ? "" : reader.GetString(7);
                        model.UpdatedUseCompany = reader.IsDBNull(8) ? "" : reader.GetString(8);
                        model.UpdatedUseDepmt = reader.IsDBNull(9) ? "" : reader.GetString(9);
                        model.UserName = reader.IsDBNull(10) ? "" : reader.GetString(10);

                        model.Barcode = reader.IsDBNull(11) ? "" : reader.GetString(11);
                        model.AssetName = reader.IsDBNull(12) ? "" : reader.GetString(12);
                        model.SpecModel = reader.IsDBNull(13) ? "" : reader.GetString(13);
                        model.SNCode = reader.IsDBNull(14) ? "" : reader.GetString(14);
                        model.Unit = reader.IsDBNull(15) ? "" : reader.GetString(15);
                        model.Price = reader.IsDBNull(16) ? 0 : reader.GetDecimal(16);
                        model.SBuyDate = reader.IsDBNull(17) ? "" : reader.GetDateTime(17).ToString("yyyy-MM-dd");
                        model.UsePerson = reader.IsDBNull(18) ? "" : reader.GetString(18);
                        model.Manager = reader.IsDBNull(19) ? "" : reader.GetString(19);
                        model.StoreLocation = reader.IsDBNull(20) ? "" : reader.GetString(20);
                        model.UseExpireMonth = reader.IsDBNull(21) ? 0 : reader.GetInt32(21);
                        model.Supplier = reader.IsDBNull(22) ? "" : reader.GetString(22);
                        model.Category = reader.IsDBNull(23) ? "" : reader.GetString(23);
                        model.UseCompany = reader.IsDBNull(24) ? "" : reader.GetString(24);
                        model.UseDepmt = reader.IsDBNull(25) ? "" : reader.GetString(25);
                        model.OwnedCompany = reader.IsDBNull(26) ? "" : reader.GetString(26);
                        model.Region = reader.IsDBNull(27) ? "" : reader.GetString(27);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public int Delete(object pandianId,object assetId)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from PandianAsset where PandianId = @PandianId and AssetId = @AssetId ");
            SqlParameter[] parms = {
                new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier),
                new SqlParameter("@AssetId", SqlDbType.UniqueIdentifier)
            };
            parms[0].Value = Guid.Parse(pandianId.ToString());
            parms[1].Value = Guid.Parse(assetId.ToString());

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        #endregion
    }
}
