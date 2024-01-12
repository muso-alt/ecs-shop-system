using UnityEngine;

namespace ShopComplex.Components
{
    public struct InventoryEvent<T> where T : MonoBehaviour
    {
        public T View;
        public RectTransform DraggedItem;
    }
}
