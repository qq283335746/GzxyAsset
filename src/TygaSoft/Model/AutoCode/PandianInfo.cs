using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class PandianInfo
    {
        public Guid Id { get; set; }

        public string Named { get; set; }

        public string AllowUsers { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid UserId { get; set; }

        public Int32 TotalQty { get; set; }

        public string Status { get; set; }

        public bool IsDown { get; set; }

        public string Remark { get; set; }

        public DateTime LastUpdatedDate { get; set; }
    }
}
