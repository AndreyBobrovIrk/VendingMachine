using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VendingMachine.Models
{
    public interface ISelectedItem
    {
        int Count { get; }
    }

    public class SelectedDrinkCollection
    {
        private class SelectedDrink : ISelectedItem
        {
            private SelectedDrink() { }
            public SelectedDrink(Drink a_item)
            {
                Item = a_item;
            }

            public Drink Item { get; private set; }
            public int Count { get; set; }
        }

        public SelectedDrinkCollection()
        {
        }

        Dictionary<int, SelectedDrink> m_collection = new Dictionary<int, SelectedDrink>();

        public int GetSelected(int a_id)
        {
            var res = m_collection.FirstOrDefault(o=>o.Key == a_id);
            if(res.Value == null)
            {
                return 0;
            }

            return res.Value.Count;
        }

        public int AddItem(Drink a_item)
        {
            if (m_collection.Keys.All(o => o != a_item.Id)) {
                m_collection.Add(a_item.Id, new SelectedDrink(a_item));
            }

            return ++m_collection[a_item.Id].Count;
        }

        public int RemoveItem(Drink a_item)
        {
            if (m_collection.Keys.Any(o => o == a_item.Id))
            {
                m_collection.Remove(a_item.Id);
                return m_collection[a_item.Id].Count = Math.Max(0, --m_collection[a_item.Id].Count);
            }

            return 0;
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