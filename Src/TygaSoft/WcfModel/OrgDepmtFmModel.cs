using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "OrgDepmtFmModel")]
    public class OrgDepmtFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public object CompanyId { get; set; }

        [DataMember]
        public object ParentId { get; set; }

        [DataMember]
        public string Coded { get; set; }

        [DataMember]
        public string Named { get; set; }

        [DataMember]
        public string IdStep { get; set; }

        [DataMember]
        public string CodeStep { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public Int32 Sort { get; set; }
    }
}
