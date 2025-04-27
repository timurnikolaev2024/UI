using System;
using UnityEngine;
using Zenject;

namespace InventoryUI
{
    public class InventoryView : MonoBehaviour, IInventoryView
    {
        [SerializeField] private Transform _content;
        [SerializeField] private ItemView _itemPrefab;
        
        [Inject] private ItemView.Pool _itemPool;

        public ItemView CreateItem()
        {
            ItemView view = _itemPool.Spawn();
            view.transform.SetParent(_content, false);
            return view;
        }

        public void DestroyItem(ItemView item)
        {
            _itemPool.Despawn(item);
        }
    }

    public interface IInventoryView
    {
        ItemView CreateItem();
        void DestroyItem(ItemView view);
    }
}