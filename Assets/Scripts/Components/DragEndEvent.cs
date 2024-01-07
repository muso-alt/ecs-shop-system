using UnityEngine;
using UnityEngine.EventSystems;

namespace ShopComplex.Components
{
    public struct DragEndEvent<T> where T : MonoBehaviour
    {
        public T View;
        public PointerEventData EventData;
    }
}