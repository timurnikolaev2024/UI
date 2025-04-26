using System;
using System.Collections.Generic;
using UnityEditor;
using Zenject;

namespace InventoryUI
{
    public class InventoryPresenter : IInitializable, IDisposable
    {
        private readonly IInventoryView _view;
        private readonly InventoryData _inventoryData;
        private readonly ItemPresenter.Factory _factory;
        private readonly Dictionary<Item, (ItemPresenter p, ItemView v)> _map = new();

        public InventoryPresenter(
            IInventoryView view,
            InventoryData inventoryData,
            ItemPresenter.Factory presenterFactory
        )
        {
            _view = view;
            _inventoryData = inventoryData;
            _factory = presenterFactory;
        }

        void IInitializable.Initialize()
        {
            _inventoryData.OnItemAdded += Show;
            _inventoryData.OnItemRemoved += Hide;

            foreach (var entry in _inventoryData.Items)
                Show(entry.item);
        }

        void IDisposable.Dispose()
        {
            _inventoryData.OnItemAdded -= Show;
            _inventoryData.OnItemRemoved -= Hide;

            foreach (var kv in _map.Values)
            {
                kv.p.Dispose();
            }
            _map.Clear();
        }

        private void Show(Item item)
        {
            if (_map.ContainsKey(item)) return;

            var view = _view.CreateItem();
            var presenter = _factory.Create(item, view);
            presenter.Initialize();

            _map[item] = (presenter, view);
        }

        private void Hide(Item item)
        {
            if (!_map.Remove(item, out var tuple)) return;

            tuple.p.Dispose();
            _view.DestroyItem(tuple.v);
        }
    }
}