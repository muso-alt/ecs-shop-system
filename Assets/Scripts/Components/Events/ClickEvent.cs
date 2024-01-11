using UnityEngine;

namespace ShopComplex.Components
{
    public struct ClickEvent<T> where T : MonoBehaviour
    {
        public T View;
    }
}