using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "MenusModel")]
    public class MenusModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public object ParentId { get; set; }

        [DataMember]
        public string IdStep { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public string Url { get; set; }

        [DataMember]
        public string Descr { get; set; }

        [DataMember]
        public string AllowRoles { get; set; }

        [DataMember]
        public string DenyUsers { get; set; }

        [DataMember]
        public int Sort { get; set; }
    }
}
