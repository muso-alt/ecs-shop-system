using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Components;
using ShopComplex.Data;
using ShopComplex.Tools;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Systems
{
    public class InventorySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<InventoryView> _inventorView;
        private ObjectsPool<ItemView> _itemPool;
        private EcsCustomInject<ItemsData> _data;
        
        private readonly EcsFilterInject<Inc<InventoryEvent<ItemCmp>>> _inventorFilter = "events";
        private readonly Dictionary<int, ItemView> _inventoryByIndex = new Dictionary<int, ItemView>();
        
        private readonly EcsWorldInject _eventWorld = "events";
        private readonly EcsWorldInject _defaultWorld = default;
        
        public void Init(IEcsSystems systems)
        {
            for (var i = 0; i < _inventorView.Value.Places.Length; i++)
            {
                _inventoryByIndex.Add(i, null);
            }
            
            _itemPool = new ObjectsPool<ItemView>(_data.Value.View);
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _inventorFilter.Value)
            {
                var pool = _inventorFilter.Pools.Inc1;
                ref var inventoryEvent = ref pool.Get(entity);
                
                var itemCmp = inventoryEvent.Item;
                var itemView = inventoryEvent.View;
                var dragItem = inventoryEvent.DraggedItem;
                
                pool.Del(entity);
                
                if (dragItem != null && itemView != null)
                {
                    TrySetToPlaceByPosition(dragItem, itemView);
                    continue;
                }
                
                FindPlaceAndSet(itemCmp);
            }
        }

        private void FindPlaceAndSet(ItemCmp itemCmp)
        {
            for (var i = 0; i < _inventoryByIndex.Count; i++)
            {
                var item = _inventoryByIndex[i];
                if (item != null)
                {
                    if (item.PackedEntityWithWorld.Unpack(out var world, out var entity))
                    {
                        var itemPool = world.GetPool<ItemCmp>();
                        ref var cmp = ref itemPool.Get(entity);

                        if (cmp.Name == itemCmp.Name)
                        {
                            _inventorView.Value.Places[i].SetCount(2);
                            break;
                        }
                    }
                    
                    continue;
                } 

                var view = GetItemView(itemCmp);
                SetItemToPlace(view, i);
                break;
            }
        }

        private ItemView GetItemView(ItemCmp baseItem)
        {
            var itemEntity = _defaultWorld.Value.NewEntity();
            
            ref var itemCmp = ref itemEntity.ConfigureItemEntity(_defaultWorld.Value);
            
            itemCmp.Cost = baseItem.Cost;
            itemCmp.Name = baseItem.Name;
            
            itemCmp.ItemPlace = Place.Inventory;

            var view = _itemPool.GetItem(itemCmp, _inventorView.Value.Content);
            
            view.EcsEventWorld = _eventWorld.Value;
            view.PackedEntityWithWorld = _defaultWorld.Value.PackEntityWithWorld(itemEntity);
            view.name = itemCmp.Name;
            view.Rect.SetAsFirstSibling();

            return view;
        }

        private void TrySetToPlaceByPosition(RectTransform dragItem, ItemView view)
        {
            for (var i = 0; i < _inventoryByIndex.Count; i++)
            {
                var place = _inventorView.Value.Places[i];

                if (!dragItem.IsInsideOtherRectByPosition(place.Place))
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
            view.Rect.SetParent(_inventorView.Value.Places[index].Place);
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