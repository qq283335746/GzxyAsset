using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "UserModel")]
    public class UserFmModel
    {
        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string CfmPsw { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string RoleName { get; set; }

        [DataMember]
        public bool IsApproved { get; set; }
    }
}
