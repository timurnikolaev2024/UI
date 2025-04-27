using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InventoryUI
{
    public class InventoryDebugPanelView : MonoBehaviour, IInventoryDebugPanelView
    {
        [SerializeField] private Button _addItemButton;
        [SerializeField] private Button _removeItemButton;
        [SerializeField] private Button _debugLogButton;
        [SerializeField] private TMP_InputField _idInput;

        public event Action<string> OnAddClicked;
        public event Action<string> OnRemoveClicked;
        public event Action OnLogClicked;

        void Awake()
        {
            _addItemButton.onClick.AddListener(() => OnAddClicked?.Invoke(_idInput.text));
            _removeItemButton.onClick.AddListener(() => OnRemoveClicked?.Invoke(_idInput.text));
            _debugLogButton.onClick.AddListener(() => OnLogClicked?.Invoke());
        }
    }

    public interface IInventoryDebugPanelView
    {
        event Action<string> OnAddClicked;
        event Action<string> OnRemoveClicked;
        event Action OnLogClicked;
    }
}