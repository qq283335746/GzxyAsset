using System;

namespace TygaSoft.Model
{
    [Serializable]
    public class RoleMenuFmInfo
    {
        public object MenuId { get; set; }
        public bool IsView { get; set; }
        public bool IsAdd { get; set; }
        public bool IsEdit { get; set; }
        public bool IsDelete { get; set; }
    }
}
