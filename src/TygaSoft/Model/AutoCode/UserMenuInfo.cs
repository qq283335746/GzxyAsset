using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class UserMenuInfo
    {
        public UserMenuInfo() { }

        public UserMenuInfo(Guid userId, Guid menuId, string operationAccess)
        {
            this.UserId = userId;
            this.MenuId = menuId;
            this.OperationAccess = operationAccess;
        }

        public Guid UserId { get; set; }
        public Guid MenuId { get; set; }
        public string OperationAccess { get; set; }
    }
}
