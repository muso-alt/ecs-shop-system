using UnityEngine;

namespace ShopComplex.Components
{
    public struct InventorEvent<T> where T : MonoBehaviour
    {
        public T View;
    }
}