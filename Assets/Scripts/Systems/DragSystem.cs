using System.Linq;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Components;
using ShopComplex.Data;
using ShopComplex.Tools;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Systems
{
    public class DragSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<Transform> _canvasParent;
        private EcsCustomInject<FastBuyView> _fastBuyView;
        private ObjectsPool<ItemView> _itemPool;
        private EcsCustomInject<ItemsData> _data;
        
        private readonly EcsWorldInject _eventWorld = "events";
        private readonly EcsWorldInject _defaultWorld = default;
        
        private readonly EcsFilterInject<Inc<DragBeginEvent<ItemView>>> _dragBeginFilter = "events";
        private readonly EcsFilterInject<Inc<DragEndEvent<ItemView>>> _dragEndFilter = "events";

        private ItemView _activeDragItem;
        private Camera _camera;
        
        public void Init(IEcsSystems systems)
        {
            _itemPool = new ObjectsPool<ItemView>(_data.Value.View);
            _camera = Camera.main;
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _dragBeginFilter.Value)
            {
                var pool = _dragBeginFilter.Pools.Inc1;
                ref var dragEvent = ref pool.Get(entity);
                
                var itemView = dragEvent.View;
            
                pool.Del(entity);

                if (!itemView.PackedEntityWithWorld.Unpack(out var world, out var itemEntity))
                {
                    continue;
                }
                
                var itemPool = world.GetPool<ItemCmp>();
                
                ref var itemCmp = ref itemPool.Get(itemEntity);
                
                if (itemCmp.ItemPlace == Place.FastBuy)
                {
                    continue;
                }
                
                _activeDragItem = _itemPool.GetItem(itemCmp, _canvasParent.Value);

                _activeDragItem.name = "Draggable";

                var newPosition = Input.mousePosition;
                newPosition.z = 0;
                _activeDragItem.Rect.position = newPosition;
            }

            if (_activeDragItem is null)
            {
                return;
            }
            
            var position = _camera.ScreenToWorldPoint(Input.mousePosition);
            position.z = 0;
            _activeDragItem.Rect.position = position;
            
            foreach (var entity in _dragEndFilter.Value)
            {
                var pool = _dragEndFilter.Pools.Inc1;
                ref var dragEvent = ref pool.Get(entity);
                var itemView = dragEvent.View;
            
                pool.Del(entity);

                if (!itemView.PackedEntityWithWorld.Unpack(out var world, out var itemEntity))
                {
                    continue;
                }
                
                var itemPool = world.GetPool<ItemCmp>();
                
                ref var itemCmp = ref itemPool.Get(itemEntity);

                switch (itemCmp.ItemPlace)
                {
                    case Place.FastBuy:
                        continue;
                    case Place.Inventory:
                        SendInventoryEntity(itemCmp, itemView);
                        break;
                    case Place.Market:
                        SendFastBuyEntity(itemView);
                        break;
                }

                if (_activeDragItem is null)
                {
                    continue;
                }

                Object.Destroy(_activeDragItem.gameObject);
                
                _activeDragItem = null;
            }
        }

        private void SendInventoryEntity(ItemCmp itemCmp, ItemView view)
        {
            var entity = _eventWorld.Value.NewEntity();
            ref var eventComponent = ref _eventWorld.Value.GetPool<InventoryEvent<ItemCmp>>().Add(entity);
            eventComponent.Item = itemCmp;
            eventComponent.View = view;
            eventComponent.DraggedItem = _activeDragItem.Rect;
        }

        private void SendFastBuyEntity(ItemView view)
        {
            var entity = _eventWorld.Value.NewEntity();
            ref var eventComponent = ref _eventWorld.Value.GetPool<FastBuyEvent<ItemView>>().Add(entity);
            eventComponent.Item = view;
            eventComponent.DraggedItem = _activeDragItem.Rect;
        }
    }
}