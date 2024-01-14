
using System;
using TMPro;
using UnityEngine;

namespace ShopComplex.Views
{
    public class InventoryView : BasePanelView
    {
        [SerializeField] private InventoryPlace[] _places;
        public InventoryPlace[] Places => _places;
    }

    [Serializable]
    public struct InventoryPlace
    {
        [SerializeField] private RectTransform _place;
        [SerializeField] private TMP_Text _stackCountText;
        
        public RectTransform Place => _place;

        public void SetCount(int count)
        {
            _stackCountText.text = count.ToString();
        }
        
        public void ToggleStackText(bool toggleValue)
        {
            _stackCountText.gameObject.SetActive(toggleValue);
        }
    }
}