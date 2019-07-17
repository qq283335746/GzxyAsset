using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class AssetInStoreInfo
    {
        public Guid Id { get; set; }

        public string Barcode { get; set; }

        public Guid CategoryId { get; set; }

        public Guid UseCompanyId { get; set; }

        public Guid UseDepmtId { get; set; }

        public Guid OwnedCompanyId { get; set; }

        public Guid RegionId { get; set; }

        public Guid PictureId { get; set; }

        public string Named { get; set; }

        public string SpecModel { get; set; }

        public string SNCode { get; set; }

        public string Unit { get; set; }

        public Decimal Price { get; set; }

        public DateTime BuyDate { get; set; }

        public string UsePerson { get; set; }

        public string Manager { get; set; }

        public string StoreLocation { get; set; }

        public Int32 UseExpireMonth { get; set; }

        public string Supplier { get; set; }

        public string RFID { get; set; }

        public Boolean IsDisable { get; set; }

        public string Remark { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public Guid UserId { get; set; }
    }
}
