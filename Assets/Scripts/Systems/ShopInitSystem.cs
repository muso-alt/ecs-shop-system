using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Components;
using ShopComplex.Data;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Systems
{
    public class ShopInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        
        private EcsCustomInject<ItemsData> _data;
        private EcsCustomInject<ShopPanelView> _panelView;
        
        private readonly EcsWorldInject _defaultWorld = default;
        private readonly EcsWorldInject _eventWorld = "events";
        
        public void Init(IEcsSystems systems)
        {
            Debug.Log("Hello World");
            
            CreateItems();
        }

        public void Run(IEcsSystems systems)
        {
            //Some update
        }

        private void CreateItems()
        {
            foreach (var valueItem in _data.Value.Items)
            {
                var item = Object.Instantiate(_data.Value.View, _panelView.Value.Content);
                var entity = _defaultWorld.Value.NewEntity();
                
                item.SetCost(valueItem.Price.ToString());
                item.SetName(valueItem.Name);
                
                item.PackedEntityWithWorld = _defaultWorld.Value.PackEntityWithWorld(entity);

                var itemsPool = _defaultWorld.Value.GetPool<ItemCmp>();
                ref var itemCmp = ref itemsPool.Add(entity);

                itemCmp.View = item;
                itemCmp.Cost = valueItem.Price;
                itemCmp.Name = valueItem.Name;
                itemCmp.View.EcsEventWorld = _eventWorld.Value;
            }
        }
     }
}