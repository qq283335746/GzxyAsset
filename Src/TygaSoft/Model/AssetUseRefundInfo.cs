using System;

namespace TygaSoft.Model
{
    public partial class AssetUseRefundInfo
    {
        #region UseRefundInfo

        public string UsePerson { get; set; }

        public string SUseTime { get; set; }

        public string SEstimateRefundTime { get; set; }

        public string UseUserName { get; set; }

        public string SRealRefundTime { get; set; }

        public string RefundDealUserName { get; set; }

        public string Status { get; set; }

        public string Remark { get; set; }

        #endregion

        #region AssetInStoreInfo

        public string PictureUrl { get; set; }

        public string Barcode { get; set; }

        public string CategoryName { get; set; }

        public string AssetName { get; set; }

        public string SpecModel { get; set; }

        public string SNCode { get; set; }

        public string Unit { get; set; }

        public Decimal Price { get; set; }
        
        #endregion
    }
}
