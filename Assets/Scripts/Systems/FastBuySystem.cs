using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Components;
using ShopComplex.Services;
using ShopComplex.Tools;
using ShopComplex.Views;

namespace ShopComplex.Systems
{
    public class FastBuySystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<SceneService> _sceneService;
        private ObjectsPool<ItemView> _itemPool;
        
        private readonly EcsWorldInject _eventWorld = "events";
        private readonly EcsWorldInject _defaultWorld = default;
        
        private readonly EcsFilterInject<Inc<FastBuyEvent<ItemView>>> _fastBuyFilter = "events";
        
        public void Init(IEcsSystems systems)
        {
            _itemPool = new ObjectsPool<ItemView>(_sceneService.Value.Data.View);
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _fastBuyFilter.Value)
            {
                var pool = _fastBuyFilter.Pools.Inc1;
                ref var eventCmp = ref pool.Get(entity);
                
                var itemView = eventCmp.Item; 
                var draggedItem = eventCmp.DraggedItem; 
                pool.Del(entity);

                if (!itemView.PackedEntityWithWorld.Unpack(out var world, out var itemEntity))
                {
                    continue;
                }
                
                var itemPool = world.GetPool<ItemCmp>();
                
                ref var itemCmp = ref itemPool.Get(itemEntity);
                
                if (draggedItem.IsInsideOtherRectByPosition(_sceneService.Value.FastBuy.Content) &&
                    itemCmp.ItemPlace == Place.Market)
                {
                    CreateNewEntity(itemCmp);
                }
            }
        }
        
        private void CreateNewEntity(ItemCmp baseCmp)
        {
            var itemEntity = _defaultWorld.Value.NewEntity();
            
            ref var itemCmp = ref itemEntity.ConfigureItemEntity(_defaultWorld.Value);
            
            itemCmp.Cost = baseCmp.Cost;
            itemCmp.Name = baseCmp.Name;
            itemCmp.ItemPlace = Place.FastBuy;

            var view = _itemPool.GetItem(itemCmp, _sceneService.Value.FastBuy.Content);
                
            view.EcsEventWorld = _eventWorld.Value;
            view.PackedEntityWithWorld = _defaultWorld.Value.PackEntityWithWorld(itemEntity);
        }
    }
}