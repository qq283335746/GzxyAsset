using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace TygaSoft.BLL
{
    public class ParamsHelper
    {
        List<SqlParameter> list = new List<SqlParameter>();

        public void Add(SqlParameter parameter)
        {
            list.Add(parameter);
        }

        public int Count()
        {
            return list.Count;
        }

        public void Clear()
        {
            list.Clear();
        }

        public SqlParameter this[int index]
        {
            get
            {
                return list[index] as SqlParameter;
            }
        }

        public SqlParameter this[string name]
        {
            get
            {
                return list.Find(item => item.ParameterName == name);
            }
        }

        public SqlParameter[] ToArray()
        {
            //if (list == null) return null;
            return list.ToArray();
        }
    }
}
