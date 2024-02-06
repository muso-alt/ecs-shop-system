using System;
using Newtonsoft.Json;
using UnityEngine;

namespace ShopComplex.Data
{
    [Serializable]
    public struct ItemStruct
    {
        [field: SerializeField, JsonProperty] public string Name { get; set; }
        [field: SerializeField, JsonProperty] public int Price { get; set; }
        [field: SerializeField, JsonProperty] public bool CanCollect { get; set; }
        [field: SerializeField, JsonProperty] public int StackSize { get; set; }
    }
}