using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TygaSoft.DALFactory;
using TygaSoft.IDAL;

namespace TygaSoft.BLL
{
    public partial class Application
    {
        private static readonly IApplication dal = DataAccess.CreateApplication();

        #region Application Member

        public object GetApplicationId(string appName)
        {
            return dal.GetApplicationId(appName);
        }

        #endregion
    }
}
