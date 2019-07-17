using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.Model
{
    [Serializable]
    public class JeasyuiTreeInfo
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public string state { get; set; }
        public JeasyuiTreeAttributesInfo attributes { get; set; }
        public List<JeasyuiTreeInfo> children { get; set; }
    }

    [Serializable]
    public class JeasyuiTreeAttributesInfo
    {
        public string Url { get; set; }
        public bool HasChild { get; set; }
    }
}
