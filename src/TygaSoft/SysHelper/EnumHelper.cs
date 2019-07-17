using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TygaSoft.SysHelper
{
    public class EnumHelper
    {
        public static int GetValue(Type enumType, string name)
        {
            var list = GetList(enumType);
            var item = list.FirstOrDefault(x => x.Value == name);
            if (item != null) return int.Parse(item.Key);

            return -1;
        }

        public static IList<KeyvalueInfo> GetList(Type enumType)
        {
            IList<KeyvalueInfo> list = new List<KeyvalueInfo>();
            var values = Enum.GetValues(enumType);
            foreach (var item in values)
            {
                list.Add(new KeyvalueInfo { Key = ((int)item).ToString(), Value = Enum.GetName(enumType, item) });
            }
            return list;
        }
    }

    public class KeyvalueInfo
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
