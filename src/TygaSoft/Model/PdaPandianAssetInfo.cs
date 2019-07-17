using System;

namespace TygaSoft.Model
{
    [Serializable]
    public class PdaPandianAssetInfo
    {
        public object PandianId { get; set; }
        public object AssetId { get; set; }
        public string Named { get; set; }
        public string PandianUser { get; set; }
        public int TotalQty { get; set; }
        public string PandianAssetStatus { get; set; }
        public string Remark { get; set; }

        public string PictureUrl { get; set; }
        public string AssetName { get; set; }
        public string Barcode { get; set; }
        public string SNCode { get; set; }
        public string Category { get; set; }
        public object CategoryId { get; set; }
        public string SpecModel { get; set; }
        public string OwnedCompany { get; set; }
        public string UseCompany { get; set; }
        public string UseDepmt { get; set; }
        public string Region { get; set; }
        public string StoreLocation { get; set; }
        public string UsePerson { get; set; }
        public string Unit { get; set; }

        public object UpdatedRegionId { get; set; }
        public object UpdatedUseCompanyId { get; set; }
        public object UpdatedUseDepmtId { get; set; }
        public string UpdatedStoreLocation { get; set; }
        public string UpdatedUsePerson { get; set; }
    }
}
