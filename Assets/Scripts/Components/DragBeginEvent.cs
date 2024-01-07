using UnityEngine;
using UnityEngine.EventSystems;

namespace ShopComplex.Components
{
    public struct DragBeginEvent<T> where T : MonoBehaviour
    {
        public T View;
    }
}