using System;
using System.Text;
using UnityEngine;
using Zenject;

namespace InventoryUI
{ 
    public class InventoryDebugPanelPresenter : IInitializable, IDisposable
    {
        private readonly IInventoryDebugPanelView _debugPanelView;
        private readonly InventoryData _inventory;
        private readonly ItemDatabase _database;

        public InventoryDebugPanelPresenter(IInventoryDebugPanelView panelView,
            InventoryData inventory,
            ItemDatabase database)
        {
            _debugPanelView  = panelView;
            _inventory = inventory;
            _database   = database;
        }

        void IInitializable.Initialize()
        {
            _debugPanelView.OnAddClicked += OnAddClicked;
            _debugPanelView.OnRemoveClicked += OnRemoveClicked;
            _debugPanelView.OnLogClicked += OnLogClicked;
        }

        private void OnRemoveClicked(string id)
        {
            if (!TryParseId(id, out var parsedId)) return;
            if (_database.TryGetItem(parsedId, out var item))
                _inventory.TryRemove(item);
        }

        private void OnAddClicked(string id)
        {
            if (!TryParseId(id, out var parsedId)) return;
            if (!_database.TryGetItem(parsedId, out var item))
            {
                return;
            }
            _inventory.TryAdd(item);
        }

        private void OnLogClicked()
        {
            Debug.Log(_inventory.BuildLogString());
        }

        void IDisposable.Dispose()
        {
            _debugPanelView.OnAddClicked -= OnAddClicked;
            _debugPanelView.OnRemoveClicked -= OnRemoveClicked;
            _debugPanelView.OnLogClicked -= OnLogClicked;
        }
        
        private static bool TryParseId(string raw, out int id)
        {
            if (int.TryParse(raw, out id))
                return true;

            Debug.LogWarning($"[InventoryDebugPanel] '{raw}' is not a valid integer id");
            return false;
        }
    }
}