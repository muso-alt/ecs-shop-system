using UnityEngine;

namespace ShopComplex.Views
{
    public class ShopPanelView : MonoBehaviour
    {
        [SerializeField] private Transform _content;

        public Transform Content => _content;
    }
}