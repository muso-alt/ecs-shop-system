using ShopComplex.Views;
using UnityEngine;

namespace ShopComplex.Components
{
    public struct InventoryEvent<T>
    {
        public T Item;
        public ItemView View;
        public RectTransform DraggedItem;
    }
}
