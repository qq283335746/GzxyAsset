using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    public partial class AssetInStore
    {
        #region AssetInStore Member

        public int InsertByOutput(AssetInStoreInfo model)
        {
            return dal.InsertByOutput(model);
        }

        public bool IsExist(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.IsExist(sqlWhere, cmdParms);
        }

        public AssetInStoreInfo GetModelByBarcode(string barcode)
        {
            return dal.GetModelByBarcode(barcode);
        }

        public IList<AssetInStoreInfo> GetListByJoin()
        {
            return dal.GetListByJoin();
        }

        public IList<AssetInStoreInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        #endregion
    }
}
