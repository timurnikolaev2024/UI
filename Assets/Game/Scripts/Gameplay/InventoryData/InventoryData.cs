using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace InventoryUI
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Inventory/New Inventory")]
    public class InventoryData : ScriptableObject
    {
        [Serializable]
        public class Entry
        {
            public Item item;
            public int count;
        }

        public event Action<Entry> OnEntryChanged;
        public event Action<Item> OnItemAdded;
        public event Action<Item> OnItemRemoved;

        [SerializeField] private List<Entry> _items = new();

        public IReadOnlyList<Entry> Items => _items;

        private void Add(Item item, int amount = 1)
        {
            var entry = _items.FirstOrDefault(e => e.item == item);
            if (entry == null)
            {
                entry = new Entry { item = item, count = 0 };
                _items.Add(entry);
                OnItemAdded?.Invoke(item);
            }

            entry.count += amount;
            OnEntryChanged?.Invoke(entry);
        }
        
        private void Remove(Item item, int amount = 1)
        {
            if (amount <= 0) return;

            var entry = _items.FirstOrDefault(e => e.item == item);
            if (entry == null) return;

            entry.count -= amount;

            if (entry.count <= 0)
            {
                _items.Remove(entry);
                OnItemRemoved?.Invoke(item);
            }
            else
            {
                OnEntryChanged?.Invoke(entry);
            }
        }
        
        public string BuildLogString()
        {
            var sb = new StringBuilder();
            foreach (var e in _items) sb.Append($"{e.item.title} x{e.count}, ");
            if (sb.Length > 2) sb.Length -= 2;
            return sb.ToString();
        }
        
        public bool TryAdd(Item item, int amount = 1)
        {
            if (amount <= 0) return false;
            Add(item, amount);
            return true;
        }

        public bool TryRemove(Item item, int amount = 1)
        {
            if (amount <= 0) return false;
            var entry = _items.FirstOrDefault(e => e.item == item);
            if (entry == null) return false;
            Remove(item, amount);
            return true;
        }

        public int GetCount(Item item) =>
            _items.FirstOrDefault(e => e.item == item)?.count ?? 0;
    }
}