using System;

namespace TygaSoft.Model
{
    [Serializable]
    public partial class RoleMenuInfo
    {
        public RoleMenuInfo() { }

        public RoleMenuInfo(Guid roleId, Guid menuId, string operationAccess)
        {
            this.RoleId = roleId;
            this.MenuId = menuId;
            this.OperationAccess = operationAccess;
        }

        public Guid RoleId { get; set; }
        public Guid MenuId { get; set; }
        public string OperationAccess { get; set; }
    }
}
