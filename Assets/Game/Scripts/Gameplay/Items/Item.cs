using UnityEngine;

namespace InventoryUI
{
    [CreateAssetMenu(fileName = "Item", menuName = "UI/New Item")]
    public class Item : ScriptableObject
    {
        [SerializeField] private Color _color;
        [SerializeField] private string _title;

        public Color color => _color;
        public string title => _title;
    }
}