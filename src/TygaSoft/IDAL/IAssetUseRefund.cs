using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IAssetUseRefund
    {
        #region IAssetUseRefund Member

        IList<AssetUseRefundInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms);

        int Delete(object useRefundId, object assetId);

        #endregion
    }
}
