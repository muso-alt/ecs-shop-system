using System;
using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Components;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Systems
{
    public class InventorySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<InventoryView> _inventorView;
        private readonly EcsFilterInject<Inc<InventorEvent<ItemView>>> _inventorFilter = "events";

        private readonly Dictionary<int, ItemView> _inventoryByIndex = new Dictionary<int, ItemView>();
        
        public void Init(IEcsSystems systems)
        {
            for (var i = 0; i < _inventorView.Value.Places.Length; i++)
            {
                _inventoryByIndex.Add(i, null);
            }
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _inventorFilter.Value)
            {
                var pool = _inventorFilter.Pools.Inc1;
                ref var clickEvent = ref pool.Get(entity);
                var itemView = clickEvent.View;
                pool.Del(entity);
                
                for (var i = 0; i < _inventoryByIndex.Count; i++)
                {
                    if (_inventoryByIndex[i] != null)
                    {
                        continue;
                    }

                    var place = _inventorView.Value.Places[i];
                    itemView.Rect.SetParent(place);
                    _inventoryByIndex[i] = itemView;
                    itemView.Rect.anchoredPosition = Vector2.zero;
                    break;
                }
            }
        }
    }
}