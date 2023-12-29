using System;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Components;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Systems
{
    public class HandleClickSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ClickEvent<ItemView>>> _clickFilter = "events";

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _clickFilter.Value)
            {
                var pool = _clickFilter.Pools.Inc1;
                ref var clickEvent = ref pool.Get(entity);
                var itemView = clickEvent.view;
                pool.Del(entity);

                if (!itemView.PackedEntityWithWorld.Unpack(out var world, out var itemEntity))
                {
                    continue;
                }
                
                var itemPool = world.GetPool<ItemCmp>();
                ref var itemCmp = ref itemPool.Get(itemEntity);
                    
                Debug.Log("Clicked: " + itemCmp.Name);
                Debug.Log("Item cost: " + itemCmp.Cost);
            }
        }
    }
}