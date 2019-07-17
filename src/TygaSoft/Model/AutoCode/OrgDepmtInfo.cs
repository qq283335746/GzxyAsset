using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class OrgDepmtInfo
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        public Guid ParentId { get; set; }

        public string Coded { get; set; }

        public string Named { get; set; }

        public string IdStep { get; set; }

        public string CodeStep { get; set; }

        public Int32 Sort { get; set; }

        public string Remark { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public Guid UserId { get; set; }
    }
}
