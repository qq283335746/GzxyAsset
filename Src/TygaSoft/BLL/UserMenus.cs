using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TygaSoft.Model;

namespace TygaSoft.BLL
{
    [Serializable]
    public class UserMenus
    {
        private List<MenusInfo> list = new List<MenusInfo>();

        public void Insert(MenusInfo model)
        {
            if (!list.Contains(model))
            {
                list.Add(model);
            }
        }

        public void InsertBatch(IList<MenusInfo> currList)
        {
            foreach (var item in currList)
            {
                Insert(item);
            }
            //list = currList.ToList();
        }

        public void Update(MenusInfo model)
        {
            int i = list.FindIndex(delegate (MenusInfo m) { return m.Id == model.Id; });
            if (i >= 0)
            {
                list.IndexOf(model, i);
            }
        }

        public List<MenusInfo> GetList()
        {
            return list;
        }

        public MenusInfo GetModel(Guid Id)
        {
            return list.Find(delegate (MenusInfo m) { return m.Id == Id; });
        }

        public int Count
        {
            get { return list.Count(); }
        }

        public void Remove(Guid Id)
        {
            list.RemoveAll(delegate (MenusInfo m) { return m.Id == Id; });
        }

        public void Clear()
        {
            list.Clear();
        }
    }
}
