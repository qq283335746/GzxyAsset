using System;
using System.Runtime.Serialization;
namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PandianAssetFmModel")]
    public class PandianAssetFmModel
    {
        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Named { get; set; }

        [DataMember]
        public string AllowUsers { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string BuyStartDate { get; set; }

        [DataMember]
        public string BuyEndDate { get; set; }

        [DataMember]
        public string UseCompany { get; set; }

        [DataMember]
        public string UseDepmt { get; set; }

        [DataMember]
        public string OwnedCompany { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string Region { get; set; }

        [DataMember]
        public string Manager { get; set; }

        [DataMember]
        public bool IsConfirm { get; set; }
    }
}
