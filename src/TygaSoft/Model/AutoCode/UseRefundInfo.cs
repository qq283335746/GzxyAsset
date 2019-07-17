using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class UseRefundInfo
    {
        public Guid Id { get; set; }

        public string UsePerson { get; set; }

        public DateTime UseTime { get; set; }

        public DateTime EstimateRefundTime { get; set; }

        public string UseUser { get; set; }

        public DateTime RealRefundTime { get; set; }

        public string RefundDealUser { get; set; }

        public string Status { get; set; }

        public string Remark { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
