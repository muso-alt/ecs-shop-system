
using UnityEngine;

namespace ShopComplex.Views
{
    public class InventoryView : BasePanelView
    {
        [SerializeField] private RectTransform[] _places;
        public RectTransform[] Places => _places;
    }
}