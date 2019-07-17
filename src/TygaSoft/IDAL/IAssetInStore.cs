using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IAssetInStore
    {
        #region IAssetInStore Member

        int InsertByOutput(AssetInStoreInfo model);

        bool IsExist(string sqlWhere, params SqlParameter[] cmdParms);

        AssetInStoreInfo GetModelByBarcode(string barcode);

        IList<AssetInStoreInfo> GetListByJoin();

        IList<AssetInStoreInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        #endregion
    }
}
