using Systems;
using Leopotam.EcsLite;
using UnityEngine;

public class EcsStartup : MonoBehaviour
{
    private EcsWorld _world;
    private IEcsSystems _systems;
    
    private void Awake()
    {
        _world = new EcsWorld();
        
        _systems = new EcsSystems(_world);
        _systems.Add(new ShopSystem());
        _systems.Init();
    }

    public void Update()
    {
        _systems.Run(); 
    }
}
