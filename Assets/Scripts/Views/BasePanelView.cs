using UnityEngine;

namespace ShopComplex.Views
{
    public class BasePanelView : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;

        public RectTransform Content => _content;

        private void OnValidate()
        {
            _content = GetComponent<RectTransform>();
        }
    }
}