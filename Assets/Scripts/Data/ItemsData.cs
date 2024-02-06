using System.Collections.Generic;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Data
{
    [CreateAssetMenu(fileName = nameof(ItemsData), menuName = nameof(ShopComplex) + "/" + nameof(ItemsData), order = 0)]
    public class ItemsData : ScriptableObject
    {
        [SerializeField] private ItemView _view;
        [SerializeField] private ItemStruct[] _items;

        public ItemView View => _view;
        public IEnumerable<ItemStruct> Items => _items;
    }
}