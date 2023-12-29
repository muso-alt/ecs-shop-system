using UnityEngine;

namespace ShopComplex.Views
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Transform _content;

        public Transform Content => _content;
    }
}