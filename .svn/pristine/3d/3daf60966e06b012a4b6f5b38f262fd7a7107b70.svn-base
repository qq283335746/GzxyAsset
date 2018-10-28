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
    public partial class AssetInStore : IAssetInStore
    {
        #region IAssetInStore Member

        public int Insert(AssetInStoreInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"insert into AssetInStore (Barcode,CategoryId,UseCompanyId,UseDepmtId,OwnedCompanyId,RegionId,PictureId,Named,SpecModel,SNCode,Unit,Price,BuyDate,UsePerson,Manager,StoreLocation,UseExpireMonth,Supplier,RFID,IsDisable,Remark,LastUpdatedDate,UserId)
			            values
						(@Barcode,@CategoryId,@UseCompanyId,@UseDepmtId,@OwnedCompanyId,@RegionId,@PictureId,@Named,@SpecModel,@SNCode,@Unit,@Price,@BuyDate,@UsePerson,@Manager,@StoreLocation,@UseExpireMonth,@Supplier,@RFID,@IsDisable,@Remark,@LastUpdatedDate,@UserId)
			            ");

            SqlParameter[] parms = {
                                       new SqlParameter("@Barcode",SqlDbType.VarChar,36),
new SqlParameter("@CategoryId",SqlDbType.UniqueIdentifier),
new SqlParameter("@UseCompanyId",SqlDbType.UniqueIdentifier),
new SqlParameter("@UseDepmtId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OwnedCompanyId",SqlDbType.UniqueIdentifier),
new SqlParameter("@RegionId",SqlDbType.UniqueIdentifier),
new SqlParameter("@PictureId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@SpecModel",SqlDbType.NVarChar,256),
new SqlParameter("@SNCode",SqlDbType.VarChar,36),
new SqlParameter("@Unit",SqlDbType.NVarChar,20),
new SqlParameter("@Price",SqlDbType.Decimal),
new SqlParameter("@BuyDate",SqlDbType.DateTime),
new SqlParameter("@UsePerson",SqlDbType.NVarChar,50),
new SqlParameter("@Manager",SqlDbType.NVarChar,50),
new SqlParameter("@StoreLocation",SqlDbType.NVarChar,100),
new SqlParameter("@UseExpireMonth",SqlDbType.Int),
new SqlParameter("@Supplier",SqlDbType.NVarChar,50),
new SqlParameter("@RFID",SqlDbType.VarChar,36),
new SqlParameter("@IsDisable",SqlDbType.Bit),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = model.Barcode;
            parms[1].Value = model.CategoryId;
            parms[2].Value = model.UseCompanyId;
            parms[3].Value = model.UseDepmtId;
            parms[4].Value = model.OwnedCompanyId;
            parms[5].Value = model.RegionId;
            parms[6].Value = model.PictureId;
            parms[7].Value = model.Named;
            parms[8].Value = model.SpecModel;
            parms[9].Value = model.SNCode;
            parms[10].Value = model.Unit;
            parms[11].Value = model.Price;
            parms[12].Value = model.BuyDate;
            parms[13].Value = model.UsePerson;
            parms[14].Value = model.Manager;
            parms[15].Value = model.StoreLocation;
            parms[16].Value = model.UseExpireMonth;
            parms[17].Value = model.Supplier;
            parms[18].Value = model.RFID;
            parms[19].Value = model.IsDisable;
            parms[20].Value = model.Remark;
            parms[21].Value = model.LastUpdatedDate;
            parms[22].Value = model.UserId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Update(AssetInStoreInfo model)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"update AssetInStore set Barcode = @Barcode,CategoryId = @CategoryId,UseCompanyId = @UseCompanyId,UseDepmtId = @UseDepmtId,
                        OwnedCompanyId = @OwnedCompanyId,RegionId = @RegionId,PictureId = @PictureId,Named = @Named,SpecModel = @SpecModel,SNCode = @SNCode,
                        Unit = @Unit,Price = @Price,BuyDate = @BuyDate,UsePerson = @UsePerson,Manager = @Manager,StoreLocation = @StoreLocation,
                        UseExpireMonth = @UseExpireMonth,Supplier = @Supplier,RFID = @RFID,IsDisable = @IsDisable,Remark = @Remark,LastUpdatedDate = @LastUpdatedDate,UserId = @UserId 
			            where Id = @Id
					    ");

            SqlParameter[] parms = {
                                     new SqlParameter("@Id",SqlDbType.UniqueIdentifier),
new SqlParameter("@Barcode",SqlDbType.VarChar,36),
new SqlParameter("@CategoryId",SqlDbType.UniqueIdentifier),
new SqlParameter("@UseCompanyId",SqlDbType.UniqueIdentifier),
new SqlParameter("@UseDepmtId",SqlDbType.UniqueIdentifier),
new SqlParameter("@OwnedCompanyId",SqlDbType.UniqueIdentifier),
new SqlParameter("@RegionId",SqlDbType.UniqueIdentifier),
new SqlParameter("@PictureId",SqlDbType.UniqueIdentifier),
new SqlParameter("@Named",SqlDbType.NVarChar,50),
new SqlParameter("@SpecModel",SqlDbType.NVarChar,256),
new SqlParameter("@SNCode",SqlDbType.VarChar,36),
new SqlParameter("@Unit",SqlDbType.NVarChar,20),
new SqlParameter("@Price",SqlDbType.Decimal),
new SqlParameter("@BuyDate",SqlDbType.DateTime),
new SqlParameter("@UsePerson",SqlDbType.NVarChar,50),
new SqlParameter("@Manager",SqlDbType.NVarChar,50),
new SqlParameter("@StoreLocation",SqlDbType.NVarChar,100),
new SqlParameter("@UseExpireMonth",SqlDbType.Int),
new SqlParameter("@Supplier",SqlDbType.NVarChar,50),
new SqlParameter("@RFID",SqlDbType.VarChar,36),
new SqlParameter("@IsDisable",SqlDbType.Bit),
new SqlParameter("@Remark",SqlDbType.NVarChar,100),
new SqlParameter("@LastUpdatedDate",SqlDbType.DateTime),
new SqlParameter("@UserId",SqlDbType.UniqueIdentifier)
                                   };
            parms[0].Value = model.Id;
            parms[1].Value = model.Barcode;
            parms[2].Value = model.CategoryId;
            parms[3].Value = model.UseCompanyId;
            parms[4].Value = model.UseDepmtId;
            parms[5].Value = model.OwnedCompanyId;
            parms[6].Value = model.RegionId;
            parms[7].Value = model.PictureId;
            parms[8].Value = model.Named;
            parms[9].Value = model.SpecModel;
            parms[10].Value = model.SNCode;
            parms[11].Value = model.Unit;
            parms[12].Value = model.Price;
            parms[13].Value = model.BuyDate;
            parms[14].Value = model.UsePerson;
            parms[15].Value = model.Manager;
            parms[16].Value = model.StoreLocation;
            parms[17].Value = model.UseExpireMonth;
            parms[18].Value = model.Supplier;
            parms[19].Value = model.RFID;
            parms[20].Value = model.IsDisable;
            parms[21].Value = model.Remark;
            parms[22].Value = model.LastUpdatedDate;
            parms[23].Value = model.UserId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parms);
        }

        public int Delete(object Id)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append("delete from AssetInStore where Id = @Id");
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
                sb.Append(@"delete from AssetInStore where Id = @Id" + n + " ;");
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

        public AssetInStoreInfo GetModel(object Id)
        {
            AssetInStoreInfo model = null;

            StringBuilder sb = new StringBuilder(300);
            sb.Append(@"select top 1 Id,Barcode,CategoryId,UseCompanyId,UseDepmtId,OwnedCompanyId,RegionId,PictureId,Named,SpecModel,SNCode,Unit,Price,BuyDate,UsePerson,Manager,StoreLocation,UseExpireMonth,Supplier,RFID,IsDisable,Remark,LastUpdatedDate,UserId 
			            from AssetInStore
						where Id = @Id ");
            SqlParameter parm = new SqlParameter("@Id", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(Id.ToString());

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), parm))
            {
                if (reader != null)
                {
                    if (reader.Read())
                    {
                        model = new AssetInStoreInfo();
                        model.Id = reader.GetGuid(0);
                        model.Barcode = reader.GetString(1);
                        model.CategoryId = reader.GetGuid(2);
                        model.UseCompanyId = reader.GetGuid(3);
                        model.UseDepmtId = reader.GetGuid(4);
                        model.OwnedCompanyId = reader.GetGuid(5);
                        model.RegionId = reader.GetGuid(6);
                        model.PictureId = reader.GetGuid(7);
                        model.Named = reader.GetString(8);
                        model.SpecModel = reader.GetString(9);
                        model.SNCode = reader.GetString(10);
                        model.Unit = reader.GetString(11);
                        model.Price = reader.GetDecimal(12);
                        model.BuyDate = reader.GetDateTime(13);
                        model.UsePerson = reader.GetString(14);
                        model.Manager = reader.GetString(15);
                        model.StoreLocation = reader.GetString(16);
                        model.UseExpireMonth = reader.GetInt32(17);
                        model.Supplier = reader.GetString(18);
                        model.RFID = reader.GetString(19);
                        model.IsDisable = reader.GetBoolean(20);
                        model.Remark = reader.GetString(21);
                        model.LastUpdatedDate = reader.GetDateTime(22);
                        model.UserId = reader.GetGuid(23);
                    }
                }
            }

            return model;
        }

        public IList<AssetInStoreInfo> GetList(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select count(*) from AssetInStore ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            totalRecords = (int)SqlHelper.ExecuteScalar(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms);

            if (totalRecords == 0) return new List<AssetInStoreInfo>();

            sb.Clear();
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			          Id,Barcode,CategoryId,UseCompanyId,UseDepmtId,OwnedCompanyId,RegionId,PictureId,Named,SpecModel,SNCode,Unit,Price,
                      BuyDate,UsePerson,Manager,StoreLocation,UseExpireMonth,Supplier,RFID,IsDisable,Remark,LastUpdatedDate,UserId
					  from AssetInStore ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<AssetInStoreInfo> list = new List<AssetInStoreInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AssetInStoreInfo model = new AssetInStoreInfo();
                        model.Id = reader.GetGuid(1);
                        model.Barcode = reader.GetString(2);
                        model.CategoryId = reader.GetGuid(3);
                        model.UseCompanyId = reader.GetGuid(4);
                        model.UseDepmtId = reader.GetGuid(5);
                        model.OwnedCompanyId = reader.GetGuid(6);
                        model.RegionId = reader.GetGuid(7);
                        model.PictureId = reader.GetGuid(8);
                        model.Named = reader.GetString(9);
                        model.SpecModel = reader.GetString(10);
                        model.SNCode = reader.GetString(11);
                        model.Unit = reader.GetString(12);
                        model.Price = reader.GetDecimal(13);
                        model.BuyDate = reader.GetDateTime(14);
                        model.UsePerson = reader.GetString(15);
                        model.Manager = reader.GetString(16);
                        model.StoreLocation = reader.GetString(17);
                        model.UseExpireMonth = reader.GetInt32(18);
                        model.Supplier = reader.GetString(19);
                        model.RFID = reader.GetString(20);
                        model.IsDisable = reader.GetBoolean(21);
                        model.Remark = reader.GetString(22);
                        model.LastUpdatedDate = reader.GetDateTime(23);
                        model.UserId = reader.GetGuid(24);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AssetInStoreInfo> GetList(int pageIndex, int pageSize, string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            int startIndex = (pageIndex - 1) * pageSize + 1;
            int endIndex = pageIndex * pageSize;

            sb.Append(@"select * from(select row_number() over(order by LastUpdatedDate desc) as RowNumber,
			           Id,Barcode,CategoryId,UseCompanyId,UseDepmtId,OwnedCompanyId,RegionId,PictureId,Named,SpecModel,SNCode,Unit,Price,
                       BuyDate,UsePerson,Manager,StoreLocation,UseExpireMonth,Supplier,RFID,IsDisable,Remark,LastUpdatedDate,UserId
					   from AssetInStore ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);
            sb.AppendFormat(@")as objTable where RowNumber between {0} and {1} ", startIndex, endIndex);

            IList<AssetInStoreInfo> list = new List<AssetInStoreInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AssetInStoreInfo model = new AssetInStoreInfo();
                        model.Id = reader.GetGuid(1);
                        model.Barcode = reader.GetString(2);
                        model.CategoryId = reader.GetGuid(3);
                        model.UseCompanyId = reader.GetGuid(4);
                        model.UseDepmtId = reader.GetGuid(5);
                        model.OwnedCompanyId = reader.GetGuid(6);
                        model.RegionId = reader.GetGuid(7);
                        model.PictureId = reader.GetGuid(8);
                        model.Named = reader.GetString(9);
                        model.SpecModel = reader.GetString(10);
                        model.SNCode = reader.GetString(11);
                        model.Unit = reader.GetString(12);
                        model.Price = reader.GetDecimal(13);
                        model.BuyDate = reader.GetDateTime(14);
                        model.UsePerson = reader.GetString(15);
                        model.Manager = reader.GetString(16);
                        model.StoreLocation = reader.GetString(17);
                        model.UseExpireMonth = reader.GetInt32(18);
                        model.Supplier = reader.GetString(19);
                        model.RFID = reader.GetString(20);
                        model.IsDisable = reader.GetBoolean(21);
                        model.Remark = reader.GetString(22);
                        model.LastUpdatedDate = reader.GetDateTime(23);
                        model.UserId = reader.GetGuid(24);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AssetInStoreInfo> GetList(string sqlWhere, params SqlParameter[] cmdParms)
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,Barcode,CategoryId,UseCompanyId,UseDepmtId,OwnedCompanyId,RegionId,PictureId,Named,SpecModel,SNCode,Unit,
                        Price,BuyDate,UsePerson,Manager,StoreLocation,UseExpireMonth,Supplier,RFID,IsDisable,Remark,LastUpdatedDate,UserId
                        from AssetInStore ");
            if (!string.IsNullOrEmpty(sqlWhere)) sb.AppendFormat(" where 1=1 {0} ", sqlWhere);

            IList<AssetInStoreInfo> list = new List<AssetInStoreInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString(), cmdParms))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AssetInStoreInfo model = new AssetInStoreInfo();
                        model.Id = reader.GetGuid(0);
                        model.Barcode = reader.GetString(1);
                        model.CategoryId = reader.GetGuid(2);
                        model.UseCompanyId = reader.GetGuid(3);
                        model.UseDepmtId = reader.GetGuid(4);
                        model.OwnedCompanyId = reader.GetGuid(5);
                        model.RegionId = reader.GetGuid(6);
                        model.PictureId = reader.GetGuid(7);
                        model.Named = reader.GetString(8);
                        model.SpecModel = reader.GetString(9);
                        model.SNCode = reader.GetString(10);
                        model.Unit = reader.GetString(11);
                        model.Price = reader.GetDecimal(12);
                        model.BuyDate = reader.GetDateTime(13);
                        model.UsePerson = reader.GetString(14);
                        model.Manager = reader.GetString(15);
                        model.StoreLocation = reader.GetString(16);
                        model.UseExpireMonth = reader.GetInt32(17);
                        model.Supplier = reader.GetString(18);
                        model.RFID = reader.GetString(19);
                        model.IsDisable = reader.GetBoolean(20);
                        model.Remark = reader.GetString(21);
                        model.LastUpdatedDate = reader.GetDateTime(22);
                        model.UserId = reader.GetGuid(23);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        public IList<AssetInStoreInfo> GetList()
        {
            StringBuilder sb = new StringBuilder(250);
            sb.Append(@"select Id,Barcode,CategoryId,UseCompanyId,UseDepmtId,OwnedCompanyId,RegionId,PictureId,Named,SpecModel,SNCode,
                        Unit,Price,BuyDate,UsePerson,Manager,StoreLocation,UseExpireMonth,Supplier,RFID,IsDisable,Remark,LastUpdatedDate,UserId 
			            from AssetInStore
					    order by LastUpdatedDate desc ");

            IList<AssetInStoreInfo> list = new List<AssetInStoreInfo>();

            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.AssetConnString, CommandType.Text, sb.ToString()))
            {
                if (reader != null && reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AssetInStoreInfo model = new AssetInStoreInfo();
                        model.Id = reader.GetGuid(0);
                        model.Barcode = reader.GetString(1);
                        model.CategoryId = reader.GetGuid(2);
                        model.UseCompanyId = reader.GetGuid(3);
                        model.UseDepmtId = reader.GetGuid(4);
                        model.OwnedCompanyId = reader.GetGuid(5);
                        model.RegionId = reader.GetGuid(6);
                        model.PictureId = reader.GetGuid(7);
                        model.Named = reader.GetString(8);
                        model.SpecModel = reader.GetString(9);
                        model.SNCode = reader.GetString(10);
                        model.Unit = reader.GetString(11);
                        model.Price = reader.GetDecimal(12);
                        model.BuyDate = reader.GetDateTime(13);
                        model.UsePerson = reader.GetString(14);
                        model.Manager = reader.GetString(15);
                        model.StoreLocation = reader.GetString(16);
                        model.UseExpireMonth = reader.GetInt32(17);
                        model.Supplier = reader.GetString(18);
                        model.RFID = reader.GetString(19);
                        model.IsDisable = reader.GetBoolean(20);
                        model.Remark = reader.GetString(21);
                        model.LastUpdatedDate = reader.GetDateTime(22);
                        model.UserId = reader.GetGuid(23);

                        list.Add(model);
                    }
                }
            }

            return list;
        }

        #endregion
    }
}
