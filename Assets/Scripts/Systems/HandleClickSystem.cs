using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Components;
using ShopComplex.Views;
using Object = UnityEngine.Object;

namespace ShopComplex.Systems
{
    public class HandleClickSystem : IEcsRunSystem
    {
        private EcsCustomInject<InventoryView> _inventoryView;
        
        private readonly EcsWorldInject _eventWorld = "events";
        private readonly EcsFilterInject<Inc<ClickEvent<ItemView>>> _clickFilter = "events";

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _clickFilter.Value)
            {
                var pool = _clickFilter.Pools.Inc1;
                ref var clickEvent = ref pool.Get(entity);
                var itemView = clickEvent.View;
                pool.Del(entity);

                if (!itemView.PackedEntityWithWorld.Unpack(out var world, out var itemEntity))
                {
                    continue;
                }
                
                var itemPool = world.GetPool<ItemCmp>();
                ref var itemCmp = ref itemPool.Get(itemEntity);

                if (itemCmp.ItemPlace != Place.Inventory)
                {
                    SetToInventory(itemCmp);
                }
                
                if (itemCmp.ItemPlace == Place.FastBuy)
                {
                    Object.Destroy(itemView.gameObject);
                    itemPool.Del(itemEntity);
                }
            }
        }
        
        private void SetToInventory(ItemCmp baseCmp)
        {
            var entity = _eventWorld.Value.NewEntity();
            ref var eventComponent = ref _eventWorld.Value.GetPool<InventoryEvent<ItemCmp>>().Add(entity);
            eventComponent.Item = baseCmp;
        }
    }
}