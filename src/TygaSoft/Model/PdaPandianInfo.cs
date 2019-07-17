using System;

namespace TygaSoft.Model
{
    [Serializable]
    public class PdaPandianInfo
    {
        public object Id { get; set; }
        public string Name { get; set; }
        public string SCreateDate { get; set; }
        public string CreateUserName { get; set; }
        public bool IsDown { get; set; }
        public int TotalQty { get; set; }
    }
}
