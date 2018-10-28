using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "RoleModel")]
    public class RoleModel
    {
        [DataMember]
        public object RoleId { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public bool IsInRole { get; set; }
    }
}
