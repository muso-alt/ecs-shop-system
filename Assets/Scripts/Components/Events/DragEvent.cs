using UnityEngine;
using UnityEngine.EventSystems;

namespace ShopComplex.Components
{
    public struct DragEvent<T> where T : MonoBehaviour
    {
        public T View;
        public Vector3 Position;
    }
}