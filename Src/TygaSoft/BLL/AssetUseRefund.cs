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
    public partial class AssetUseRefund
    {
        #region AssetUseRefund Member

        public IList<AssetUseRefundInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, object useRefundId)
        {
            var parm = new SqlParameter("@UseRefundId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(useRefundId.ToString());
            return GetListByJoin(pageIndex, pageSize, out totalRecords, "and ur.Id = @UseRefundId", parm);
        }

        public IList<AssetUseRefundInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public int Delete(object useRefundId, object assetId)
        {
            return dal.Delete(useRefundId, assetId);
        }

        #endregion
    }
}
