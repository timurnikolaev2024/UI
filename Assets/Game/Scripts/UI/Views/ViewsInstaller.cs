using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace InventoryUI
{
    public class ViewsInstaller : MonoInstaller
    {
        [FormerlySerializedAs("_productListView")] [SerializeField] private InventoryView productView;
        [FormerlySerializedAs("_debugView")] [SerializeField] private InventoryDebugPanelView debugPanelView;
        [SerializeField] private ItemView productCardPrefab;        
        [SerializeField] private Transform poolContainer;
        
        public override void InstallBindings()
        {
            Container
                .Bind<IInventoryView>()
                .FromInstance(productView)
                .AsSingle();
            
            Container
                .Bind<IInventoryDebugPanelView>()
                .FromInstance(debugPanelView)
                .AsSingle();
            
            Container
                .Bind<IItemView>()
                .FromInstance(productCardPrefab)
                .AsSingle();
            
            Container
                .BindMemoryPool<ItemView, ItemView.Pool>()
                .WithInitialSize(5)
                .FromComponentInNewPrefab(productCardPrefab)
                .UnderTransform(poolContainer);

        }
    }
}