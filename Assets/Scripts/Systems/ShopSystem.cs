using Leopotam.EcsLite;
using UnityEngine;

namespace Systems
{
    public class ShopSystem : IEcsInitSystem, IEcsRunSystem
    {
        public void Init(IEcsSystems systems)
        {
            Debug.Log("Hello World");
        }

        public void Run(IEcsSystems systems)
        {
            Debug.Log("Update");
        }
    }
}