using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using ShopComplex.Data;
using ShopComplex.Services;
using ShopComplex.Systems;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private SceneService _sceneService;
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
            _systems.Add(new InventorySystem());
            _systems.Add(new FastBuySystem());
            _systems.Add(new RequestSystem());
            
            _systems.AddWorld(new EcsWorld(), "events");  
            
            _systems.Inject(_items);
            _systems.Inject(_panelView);
            _systems.Inject(_inventoryView);
            _systems.Inject(_fastBuyView);
            _systems.Inject(_canvasParent);
            _systems.Inject(_sceneService);
            
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