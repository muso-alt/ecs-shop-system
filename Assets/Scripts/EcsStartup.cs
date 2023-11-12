using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Data;
using ShopComplex.Systems;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private ItemsData _items;
        [SerializeField] private ShopPanelView _panelView;
        
        private EcsWorld _world;
        private IEcsSystems _systems;

        private void Awake()
        {
            _world = new EcsWorld();

            _systems = new EcsSystems(_world);
            _systems.Add(new ShopInitSystem());
            _systems.Inject(_items);
            _systems.Inject(_panelView);
            _systems.Init();
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}