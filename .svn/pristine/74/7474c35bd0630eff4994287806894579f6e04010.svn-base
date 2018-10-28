using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using TygaSoft.IDAL;
using TygaSoft.DBUtility;

namespace TygaSoft.SqlServerDAL
{
    public partial class Application: IApplication
    {
        #region IApplication Member

        public object GetApplicationId(string appName)
        {
            string cmdText = @"select ApplicationId from TygaSoftAspnetDb.dbo.aspnet_Applications 
                             where LOWER(@AppName) = LoweredApplicationName";
            SqlParameter parm = new SqlParameter("@AppName", SqlDbType.NVarChar, 256);
            parm.Value = appName;

            return SqlHelper.ExecuteScalar(SqlHelper.TygaAspnetDbConnString, CommandType.Text, cmdText, parm);
        }

        #endregion
    }
}
