using UnityEngine;
using Zenject;

namespace InventoryUI
{
    public class PresentersInstaller : MonoInstaller
    {
        [SerializeField] private InventoryData _inventory;
        [SerializeField] private ItemDatabase _itemDatabase;
        public override void InstallBindings()
        {
            Container.Bind<InventoryData>()
                .FromInstance(_inventory)
                .AsSingle();

            Container.Bind<ItemDatabase>()
                .FromInstance(_itemDatabase)
                .AsSingle();

            Container.BindInterfacesAndSelfTo<InventoryPresenter>()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<InventoryDebugPanelPresenter>()
                .AsSingle()
                .NonLazy();

            Container.BindFactory<Item, ItemView, ItemPresenter, ItemPresenter.Factory>()
                .AsTransient();
        }
    }
}