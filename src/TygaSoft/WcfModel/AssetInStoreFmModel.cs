using System;
using System.Runtime.Serialization;

namespace TygaSoft.WcfModel
{
    [DataContract(Name = "AssetInStoreFmModel")]
    public class AssetInStoreFmModel
    {
        [DataMember]
        public object Id { get; set; }

        [DataMember]
        public string Barcode { get; set; }

        [DataMember]
        public object CategoryId { get; set; }

        [DataMember]
        public object UseCompanyId { get; set; }

        [DataMember]
        public object UseDepmtId { get; set; }

        [DataMember]
        public object OwnedCompanyId { get; set; }

        [DataMember]
        public object RegionId { get; set; }

        [DataMember]
        public object PictureId { get; set; }

        [DataMember]
        public string Named { get; set; }

        [DataMember]
        public string SpecModel { get; set; }

        [DataMember]
        public string SNCode { get; set; }

        [DataMember]
        public string Unit { get; set; }

        [DataMember]
        public Decimal Price { get; set; }

        [DataMember]
        public string SBuyDate { get; set; }

        [DataMember]
        public string UsePerson { get; set; }

        [DataMember]
        public string Manager { get; set; }

        [DataMember]
        public string StoreLocation { get; set; }

        [DataMember]
        public Int32 UseExpireMonth { get; set; }

        [DataMember]
        public string Supplier { get; set; }

        [DataMember]
        public string RFID { get; set; }

        [DataMember]
        public string Remark { get; set; }
    }
}
