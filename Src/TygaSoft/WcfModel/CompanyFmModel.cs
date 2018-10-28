using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "CompanyFmModel")]
    public class CompanyFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string Coded { get; set; }

        [DataMember]
        public string Named { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Phone { get; set; }

        [DataMember]
        public string TelPhone { get; set; }

        [DataMember]
        public Int32 Sort { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}
