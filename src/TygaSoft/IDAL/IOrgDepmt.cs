using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.Model;

namespace TygaSoft.IDAL
{
    public partial interface IOrgDepmt
    {
        #region IOrgDepmt Member

        OrgDepmtInfo GetModelByCode(string code);

        int InsertByOutput(OrgDepmtInfo model);

        #endregion
    }
}
