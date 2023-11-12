using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShopComplex.Views
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private Button _buyButton;

        private void Awake()
        {
            _buyButton.onClick.AddListener(SendClickedEvent);
        }

        private void OnDestroy()
        {
            _buyButton.onClick.RemoveListener(SendClickedEvent);
        }

        public void SetCost(string cost)
        {
            _costText.text = cost;
        }

        public void SetName(string nameText)
        {
            _nameText.text = nameText;
        }
        
        private void SendClickedEvent()
        {
            Debug.Log("Clicked");
        }
    }
}