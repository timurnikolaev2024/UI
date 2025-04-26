using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace InventoryUI
{
    [CreateAssetMenu(fileName = "ItemDatabase", menuName = "Inventory/Item Database")]
    public class ItemDatabase : ScriptableObject
    {
        [Serializable]
        public struct ItemRecord
        {
            public int  id;
            public Item item;
        }
        
        [SerializeField] private List<ItemRecord> items = new();

        private Dictionary<int, Item> _lookup;

        private void Initialize()
        {
            if (_lookup != null)
                return;
            
            _lookup = new Dictionary<int, Item>();

            foreach (var record in items)
            {
                if (record.item == null)
                {
                    Debug.LogWarning($"[ItemDatabase] Item with ID {record.id} is null.");
                    continue;
                }

                if (_lookup.ContainsKey(record.id))
                {
                    Debug.LogWarning($"[ItemDatabase] Duplicate ID detected: {record.id}. Skipping.");
                    continue;
                }

                _lookup.Add(record.id, record.item);
            }
        }

        public bool TryGetItem(int id, out Item item)
        {
            Initialize();
            return _lookup.TryGetValue(id, out item);
        }

        public IEnumerable<int> AllIds
        {
            get
            {
                Initialize();
                return _lookup.Keys;
            }
        }
    }   
}