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
    public partial class UseRefund
    {
        #region UseRefund Member

        public int InsertByOutput(UseRefundInfo model)
        {
            return dal.InsertByOutput(model);
        }

        #endregion
    }
}
