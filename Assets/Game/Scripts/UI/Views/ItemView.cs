using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace InventoryUI
{
    public sealed class ItemView : MonoBehaviour, IItemView
    {
        [SerializeField] private TMP_Text _title;
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _count;

        public void SetColor(Color color)
        {
            _icon.color = color;
        }

        public void SetTitle(string title)
        {
            _title.text = title;
        }

        public void SetCount(int coubt)
        {
            _count.text = coubt.ToString();
        }
        
        public class Pool : MonoMemoryPool<ItemView>
        {
        }
    }

    public interface IItemView
    {
        void SetTitle(string title);
        void SetCount(int count);
        void SetColor(Color color);
    }
}