using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IRegion
    {
        #region IRegion Member

        RegionInfo GetModelByCode(string code);

        int InsertByOutput(RegionInfo model);

        #endregion
    }
}
