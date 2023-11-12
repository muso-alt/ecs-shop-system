using System;
using System.Collections.Generic;
using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Data
{
    [CreateAssetMenu(fileName = nameof(ItemsData), menuName = nameof(ShopComplex) + "/" + nameof(ItemsData), order = 0)]
    public class ItemsData : ScriptableObject
    {
        [SerializeField] private ItemView _view;
        [SerializeField] private Item[] _items;

        public ItemView View => _view;
        public IEnumerable<Item> Items => _items;
    }

    [Serializable]
    public struct Item
    {
        [SerializeField] private string _name;
        [SerializeField] private int _price;

        public string Name => _name;
        public int Price => _price;
    }
}