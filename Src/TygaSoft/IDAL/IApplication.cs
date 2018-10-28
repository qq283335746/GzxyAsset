using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.IDAL
{
    public partial interface IApplication
    {
        #region IApplication Member

        object GetApplicationId(string appName);

        #endregion
    }
}
