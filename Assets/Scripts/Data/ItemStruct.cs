using System;
using Newtonsoft.Json;
using UnityEngine;

namespace ShopComplex.Data
{
    [Serializable]
    public struct ItemStruct
    {
        public ItemStruct(string name, int price, bool canCollect, int stackSize)
        {
            Name = name;
            Price = price;
            CanCollect = canCollect;
            StackSize = stackSize;
        }
        
        [field: SerializeField, JsonProperty] public string Name { get; set; }
        [field: SerializeField, JsonProperty] public int Price { get; set; }
        [field: SerializeField, JsonProperty] public bool CanCollect { get; set; }
        [field: SerializeField, JsonProperty] public int StackSize { get; set; }
    }
}