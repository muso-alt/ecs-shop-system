using Leopotam.EcsLite;
using ShopComplex.Components;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Tools
{
    public class ObjectsPool<T> where T : ItemView
    {
        private T _item;

        public ObjectsPool(T spawnItem)
        {
            _item = spawnItem;
        }

        public T GetItem(ItemCmp cmp, Transform parent)
        {
            var view = Object.Instantiate(_item, parent);
            
            view.SetName(cmp.Name);
            view.SetCost(cmp.Cost.ToString());
            
            return view;
        }
    }
}