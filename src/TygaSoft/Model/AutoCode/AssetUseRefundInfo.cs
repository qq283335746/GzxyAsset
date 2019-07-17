using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class AssetUseRefundInfo
    {
	    public Guid UseRefundId { get; set; }
        public Guid AssetId { get; set; } 
        public string OwnedCompany { get; set; }
        public string UseCompany { get; set; }
        public string UseCompanyDepmt { get; set; }
        public string StoreLocation { get; set; }
        public string AssetUsePerson { get; set; }
    }
}
