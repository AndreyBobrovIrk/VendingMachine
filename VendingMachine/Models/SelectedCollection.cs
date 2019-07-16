using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachine.Models
{
    public class SelectedCollection<T> where T : IIdentified, IValuable
    {
        private class SelectedItem
        {
            private SelectedItem() { }
            public SelectedItem(T a_item)
            {
                Item = a_item;
            }

            public T Item { get; private set; }
            public int Count { get; set; }
        }

        private SelectedCollection()
        {
        }

        public SelectedCollection(IEnumerable<T> a_list)
        {
            //foreach(var item in a_list) {
            //    m_collection.Add(item.Id, new SelectedItem(item));
            //}
        }

        Dictionary<int, SelectedCollection<T>.SelectedItem> m_collection = new Dictionary<int, SelectedItem>();

        public void AddItem(T a_item)
        {
            if (m_collection.Keys.All(o => o != a_item.Id)) {
                m_collection.Add(a_item.Id, new SelectedItem(a_item));
            }

            ++m_collection[a_item.Id].Count;
        }

        public void RemoveItem(T a_item)
        {
            if (m_collection.Keys.Any(o => o == a_item.Id))
            {
                m_collection.Remove(a_item.Id);
                m_collection[a_item.Id].Count = Math.Max(0, --m_collection[a_item.Id].Count);
            }
        }

        public void Clear()
        {
            m_collection.Clear();
        }

        public int Total
        {
            get
            {
                return m_collection.Values.Sum(o => o.Item.Value * o.Count);
            }
        }
    }
}