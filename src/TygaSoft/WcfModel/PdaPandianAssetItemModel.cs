using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "PdaPandianAssetItemModel")]
    public class PdaPandianAssetItemModel
    {
        [DataMember]
        public object AssetId { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public object Region { get; set; }

        [DataMember]
        public object UseCompany { get; set; }

        [DataMember]
        public object UseDepmt { get; set; }

        [DataMember]
        public object OwnedCompany { get; set; }

        [DataMember]
        public object Category { get; set; }

        [DataMember]
        public string StoreLocation { get; set; }

        [DataMember]
        public string UsePerson { get; set; }

        [DataMember]
        public string Remark { get; set; }

        [DataMember]
        public string Barcode { get; set; }

        [DataMember]
        public string AssetName { get; set; }

        [DataMember]
        public string SpecModel { get; set; }

        [DataMember]
        public string Unit { get; set; }
    }
}
