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
    public partial class PandianAsset
    {
        #region PandianAsset Member

        public int[] GetTotal(object pandianId)
        {
            return dal.GetTotal(pandianId);
        }

        public bool IsExist(string barcode)
        {
            return dal.IsExist(barcode);
        }

        public IList<PandianAssetInfo> GetListByPandianId(object pandianId)
        {
            var sqlWhere = "and PandianId = @PandianId ";
            var parm = new SqlParameter("@PandianId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(pandianId.ToString());

            return dal.GetList(sqlWhere, parm);
        }

        public IList<PandianAssetInfo> GetListByJoin(int pageIndex, int pageSize, out int totalRecords, string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(pageIndex, pageSize, out totalRecords, sqlWhere, cmdParms);
        }

        public IList<PandianAssetInfo> GetListByJoin(string sqlWhere, params SqlParameter[] cmdParms)
        {
            return dal.GetListByJoin(sqlWhere, cmdParms);
        }

        public int Delete(object pandianId, object assetId)
        {
            return dal.Delete(pandianId, assetId);
        }

        #endregion
    }
}
