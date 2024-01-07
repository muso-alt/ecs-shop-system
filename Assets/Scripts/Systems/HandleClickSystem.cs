using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Components;
using ShopComplex.Data;
using ShopComplex.Tools;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Systems
{
    public class HandleClickSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<InventoryView> _inventoryView;
        private ObjectsPool<ItemView> _itemPool;
        private EcsCustomInject<ItemsData> _data;
        
        private readonly EcsWorldInject _eventWorld = "events";
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsFilterInject<Inc<ClickEvent<ItemView>>> _clickFilter = "events";

        public void Init(IEcsSystems systems)
        {
            _itemPool = new ObjectsPool<ItemView>(_data.Value.View);
        }
        
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

                Debug.Log("Clicked: " + itemCmp.Name);
                Debug.Log("Item cost: " + itemCmp.Cost);

                var view = _itemPool.GetItem(itemCmp, _inventoryView.Value.Content);
                
                view.EcsEventWorld = _eventWorld.Value;
                view.PackedEntityWithWorld = _defaultWorld.Value.PackEntityWithWorld(entity);
            }
        }
    }
}