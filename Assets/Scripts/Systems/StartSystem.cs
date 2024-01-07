using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Components;
using ShopComplex.Data;
using ShopComplex.Tools;
using ShopComplex.Views;

namespace ShopComplex.Systems
{
    public class StartSystem : IEcsInitSystem
    {
        private EcsCustomInject<ItemsData> _data;
        private ObjectsPool<ItemView> _itemPool;
        
        private EcsCustomInject<ShopPanelView> _panelView;
        
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsWorldInject _eventWorld = "events";
        
        public void Init(IEcsSystems systems)
        {
            _itemPool = new ObjectsPool<ItemView>(_data.Value.View);
            
            foreach (var item in _data.Value.Items)
            {
                var itemEntity = _defaultWorld.Value.NewEntity();
                var itemsPool = _defaultWorld.Value.GetPool<ItemCmp>();
                ref var itemCmp = ref itemsPool.Add(itemEntity);

                itemCmp.Cost = item.Price;
                itemCmp.Name = item.Name;

                var view = _itemPool.GetItem(itemCmp, _panelView.Value.Content);
                
                view.EcsEventWorld = _eventWorld.Value;
                view.PackedEntityWithWorld = _defaultWorld.Value.PackEntityWithWorld(itemEntity);
            }
        }
    }
}