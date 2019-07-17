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
    public partial class Category
    {
        #region Category Member

        public CategoryInfo GetModelByCode(string code)
        {
            return dal.GetModelByCode(code);
        }

        public int InsertByOutput(CategoryInfo model)
        {
            return dal.InsertByOutput(model);
        }

        //public string GetTreeNotRoot()
        //{
        //    StringBuilder jsonAppend = new StringBuilder();
        //    var list = GetList().ToList<CategoryInfo>();
        //    if (list != null && list.Count > 0)
        //    {
        //        CreateTreeJson(list, Guid.Empty, ref jsonAppend);
        //    }
        //    else
        //    {
        //        jsonAppend.Append("[{\"id\":\"" + Guid.Empty + "\",\"text\":\"请选择\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + Guid.Empty + "\",\"parentName\":\"请选择\"}}]");
        //    }

        //    return jsonAppend.ToString();
        //}

        public string GetTreeJson()
        {
            StringBuilder jsonAppend = new StringBuilder();
            var list = GetList().ToList<CategoryInfo>();
            if (list != null && list.Count > 0)
            {
                CreateTreeJson(list, Guid.Empty, ref jsonAppend);
            }
            else
            {
                jsonAppend.Append("[{\"id\":\"" + Guid.Empty + "\",\"text\":\"请选择\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + Guid.Empty + "\",\"parentName\":\"请选择\"}}]");
            }

            return jsonAppend.ToString();
        }

        private void CreateTreeJson(List<CategoryInfo> list, object parentId, ref StringBuilder jsonAppend)
        {
            jsonAppend.Append("[");
            var childList = list.FindAll(x => x.ParentId.Equals(parentId));
            if (childList.Count > 0)
            {
                int index = 0;
                foreach (var model in childList)
                {
                    jsonAppend.Append("{\"id\":\"" + model.Id + "\",\"text\":\"" + string.Format("{0}{1}", model.Coded, model.Named) + "\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + model.ParentId + "\"}");
                    if (list.Any(r => r.ParentId.Equals(model.Id)))
                    {
                        jsonAppend.Append(",\"children\":");
                        CreateTreeJson(list, model.Id, ref jsonAppend);
                    }
                    jsonAppend.Append("}");
                    if (index < childList.Count - 1) jsonAppend.Append(",");
                    index++;
                }
            }
            jsonAppend.Append("]");
        }

        #endregion
    }
}
