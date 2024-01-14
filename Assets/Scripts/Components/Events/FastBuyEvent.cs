using UnityEngine;

namespace ShopComplex.Components
{
    public struct FastBuyEvent<T>
    {
        public T Item;
        public RectTransform DraggedItem;
    }
}