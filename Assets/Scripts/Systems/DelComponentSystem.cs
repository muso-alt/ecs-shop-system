using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ShopComplex.Systems
{
    public class DelComponentSystem<T> : IEcsRunSystem where T : struct
    {
        private EcsFilterInject<Inc<T>> _filter;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc1.Del(entity);
            }
        }
    }
}