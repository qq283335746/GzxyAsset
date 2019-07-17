using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class MenusInfo
    {
        public MenusInfo() { }

        public MenusInfo(Guid applicationId, Guid id, Guid parentId, string idStep, string title, string url, string descr, string allowRoles, string denyUsers, int sort, DateTime lastUpdatedDate)
        {
            this.ApplicationId = applicationId;
            this.Id = id;
            this.ParentId = parentId;
            this.IdStep = idStep;
            this.Title = title;
            this.Url = url;
            this.Descr = descr;
            this.AllowRoles = allowRoles;
            this.DenyUsers = denyUsers;
            this.Sort = sort;
            this.LastUpdatedDate = lastUpdatedDate;
        }

        public Guid ApplicationId { get; set; }
        public Guid Id { get; set; }
        public Guid ParentId { get; set; }
        public string IdStep { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Descr { get; set; }
        public string AllowRoles { get; set; }
        public string DenyUsers { get; set; }
        public int Sort { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
