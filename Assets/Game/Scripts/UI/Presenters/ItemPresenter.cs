using UnityEngine;
using Zenject;

namespace InventoryUI
{
    public class ItemPresenter
    {
        private readonly IItemView _view;
        private readonly InventoryData _inventory;
        private readonly Item _item;

        public ItemPresenter(Item item, IItemView view, InventoryData inventoryData)
        {
            _item = item;
            _view = view;
            _inventory = inventoryData;
        }

        public void Initialize()
        {
            OnShow();
        }

        public void Dispose()
        {
            OnHide();
        }

        private void OnShow()
        {
            _view.SetTitle(_item.title);
            _view.SetCount(_inventory.GetCount(_item));
            _view.SetColor(_item.color);

            _inventory.OnEntryChanged += OnInventoryChanged;
        }

        private void OnInventoryChanged(InventoryData.Entry obj)
        {
            if (obj.item == _item) 
                _view.SetCount(obj.count);
        }

        private void OnHide()
        {
            _inventory.OnEntryChanged -= OnInventoryChanged;
        }
        
        public sealed class Factory : PlaceholderFactory<Item, ItemView, ItemPresenter>
        {
        }
    }
}