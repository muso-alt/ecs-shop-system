using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Components;
using ShopComplex.Services;
using ShopComplex.Tools;
using ShopComplex.Views;

namespace ShopComplex.Systems
{
    public class StartSystem : IEcsInitSystem
    {
        private EcsCustomInject<SceneService> _sceneService;
        private ObjectsPool<ItemView> _itemPool;
        
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsWorldInject _eventWorld = "events";
        
        public void Init(IEcsSystems systems)
        {
            _itemPool = new ObjectsPool<ItemView>(_sceneService.Value.Data.View);
            
            foreach (var item in _sceneService.Value.Data.Items)
            {
                var itemEntity = _defaultWorld.Value.NewEntity();
                
                var itemsPool = _defaultWorld.Value.GetPool<ItemCmp>();
                var dragPool = _defaultWorld.Value.GetPool<DragCmp>();
                
                ref var itemCmp = ref itemsPool.Add(itemEntity);
                ref var dragCmp = ref dragPool.Add(itemEntity);

                itemCmp.Cost = item.Price;
                itemCmp.Name = item.Name;
                itemCmp.ItemPlace = Place.Market;
                
                dragCmp.CanDrag = true;

                var view = _itemPool.GetItem(itemCmp, _sceneService.Value.ShopPanel.Content);
                
                view.EcsEventWorld = _eventWorld.Value;
                view.PackedEntityWithWorld = _defaultWorld.Value.PackEntityWithWorld(itemEntity);
            }
        }
    }
}