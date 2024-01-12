using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Components;
using ShopComplex.Tools;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Systems
{
    public class InventorySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<InventoryView> _inventorView;
        private readonly EcsFilterInject<Inc<InventoryEvent<ItemView>>> _inventorFilter = "events";

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
                ref var inventoryEvent = ref pool.Get(entity);
                var itemView = inventoryEvent.View;
                var dragItem = inventoryEvent.DraggedItem;
                pool.Del(entity);
                
                if (dragItem != null)
                {
                    TrySetToPlaceByPosition(dragItem, itemView);
                    continue;
                }
                
                for (var i = 0; i < _inventoryByIndex.Count; i++)
                {
                    if (_inventoryByIndex[i] != null)
                    {
                        continue;
                    }

                    SetItemToPlace(itemView, i);
                    break;
                }
            }
        }

        private void TrySetToPlaceByPosition(RectTransform dragItem, ItemView view)
        {
            for (var i = 0; i < _inventoryByIndex.Count; i++)
            {
                var place = _inventorView.Value.Places[i];

                if (!dragItem.IsInsideOtherRectByPosition(place))
                {
                    continue;
                }

                if (_inventoryByIndex[i] != null)
                {
                    SwapEachOther(view, _inventoryByIndex[i]);
                    return;
                }
                
                SetItemToPlace(view, i);
                return;
            }
        }

        private void SwapEachOther(ItemView first, ItemView second)
        {
            var firstIndex = 0;
            var secondIndex = 0;
            
            for (var i = 0; i < _inventoryByIndex.Count; i++)
            {
                if (_inventoryByIndex[i] == first)
                {
                    firstIndex = i;
                }

                if (_inventoryByIndex[i] == second)
                {
                    secondIndex = i;
                }
            }
            
            SetItemToPlace(first, secondIndex);
            SetItemToPlace(second, firstIndex);
        }

        private void SetItemToPlace(ItemView view, int index)
        {
            view.Rect.SetParent(_inventorView.Value.Places[index]);
            view.Rect.anchoredPosition = Vector2.zero;

            for (var i = 0; i < _inventoryByIndex.Count; i++)
            {
                if (_inventoryByIndex[i] == view)
                {
                    _inventoryByIndex[i] = null;
                }
            }
            
            _inventoryByIndex[index] = view;
        }
    }
}