using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Data;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Systems
{
    public class ShopInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world;
        
        private EcsCustomInject<ItemsData> _data;
        private EcsCustomInject<ShopPanelView> _panelView;
        
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
                item.SetCost(valueItem.Price.ToString());
                item.SetName(valueItem.Name);
            }
        }
     }
}