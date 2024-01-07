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
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private FastBuyView _fastBuyView;
        [SerializeField] private Transform _canvasParent;
        
        private EcsWorld _world;
        private IEcsSystems _systems;

        private void Awake()
        {
            _world = new EcsWorld();
            
            _systems = new EcsSystems(_world);
            
            _systems.Add(new StartSystem());
            
            _systems.Add(new HandleClickSystem());
            _systems.Add(new DragSystem());
            
            _systems.AddWorld(new EcsWorld(), "events");  
            
            _systems.Inject(_items);
            _systems.Inject(_panelView);
            _systems.Inject(_inventoryView);
            _systems.Inject(_fastBuyView);
            _systems.Inject(_canvasParent);
            
            _systems.Init();
        }

        private void Update()
        {
            _systems?.Run();
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