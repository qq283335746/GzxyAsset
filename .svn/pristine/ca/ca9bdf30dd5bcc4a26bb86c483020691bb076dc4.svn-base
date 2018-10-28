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
    public partial class OrgDepmt
    {
        #region OrgDepmt Member

        public string GetTreeByCompany(object companyId)
        {
            StringBuilder jsonAppend = new StringBuilder();

            var sqlWhere = @"and CompanyId = @CompanyId ";
            var parm = new SqlParameter("@CompanyId", SqlDbType.UniqueIdentifier);
            parm.Value = Guid.Parse(companyId.ToString());

            var list = dal.GetList(sqlWhere, parm);
            if (list == null || list.Count == 0)
            {
                jsonAppend.Append("[{\"id\":\"" + Guid.Empty + "\",\"text\":\"请选择\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + Guid.Empty + "\",\"parentName\":\"请选择\"}}]");
            }
            else
            {
                CreateTreeJson(list.ToList<OrgDepmtInfo>(), Guid.Empty, ref jsonAppend);
            }

            return jsonAppend.ToString();
        }

        public IList<OrgDepmtInfo> GetListByCompany(object companyId, object parentId)
        {
            var gId = Guid.Empty;
            if (parentId != null) Guid.TryParse(parentId.ToString(), out gId);
            var sqlWhere = new StringBuilder(100);
            sqlWhere.Append(@"and CompanyId = @CompanyId and ParentId = @ParentId ");
            SqlParameter[] parms = {
                new SqlParameter("@CompanyId",SqlDbType.UniqueIdentifier),
                new SqlParameter("@ParentId",SqlDbType.UniqueIdentifier)
            };
            parms[0].Value = Guid.Parse(companyId.ToString());
            parms[1].Value = Guid.Parse(gId.ToString());

            return dal.GetList(sqlWhere.ToString(), parms);
        }

        public OrgDepmtInfo GetModelByCode(string code)
        {
            return dal.GetModelByCode(code);
        }

        public IList<OrgDepmtInfo> GetCompany()
        {
            var sqlWhere = @"and ParentId = (select Id from dbo.OrgDepmt where ParentId = '" + Guid.Empty.ToString() + "')";
            return dal.GetList(sqlWhere, null);
        }

        public int InsertByOutput(OrgDepmtInfo model)
        {
            return dal.InsertByOutput(model);
        }

        public string GetTreeJson()
        {
            StringBuilder jsonAppend = new StringBuilder();
            var list = GetList().ToList<OrgDepmtInfo>();
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

        private void CreateTreeJson(List<OrgDepmtInfo> list, object parentId, ref StringBuilder jsonAppend)
        {
            jsonAppend.Append("[");
            var childList = list.FindAll(x => x.ParentId.Equals(parentId));
            if (childList.Count > 0)
            {
                int index = 0;
                foreach (var model in childList)
                {
                    jsonAppend.Append("{\"id\":\"" + model.Id + "\",\"text\":\"" + string.Format("{0}{1}", model.Coded,model.Named) + "\",\"state\":\"open\",\"attributes\":{\"parentId\":\"" + model.ParentId + "\"}");
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
