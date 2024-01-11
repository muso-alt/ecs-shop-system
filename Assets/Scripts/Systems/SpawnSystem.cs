using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Data;
using ShopComplex.Tools;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Systems
{
    public class SpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<Transform> _canvasParent;
        private EcsCustomInject<FastBuyView> _fastBuyView;
        private ObjectsPool<ItemView> _itemPool;
        private EcsCustomInject<ItemsData> _data;

        private readonly EcsWorldInject _eventWorld = "events";
        private readonly EcsWorldInject _defaultWorld = default;
        
        public void Init(IEcsSystems systems)
        {
            _itemPool = new ObjectsPool<ItemView>(_data.Value.View);
        }

        public void Run(IEcsSystems systems)
        {
            throw new System.NotImplementedException();
        }
    }
}