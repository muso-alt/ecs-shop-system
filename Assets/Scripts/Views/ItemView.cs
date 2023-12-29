using Leopotam.EcsLite;
using ShopComplex.Components;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

namespace ShopComplex.Views
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private Button _buyButton;

        public EcsPackedEntityWithWorld PackedEntityWithWorld { get; set; }
        public EcsWorld EcsEventWorld { get; set; }
        
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
            var entity = EcsEventWorld.NewEntity();
            ref var eventComponent = ref EcsEventWorld.GetPool<ClickEvent<ItemView>>().Add(entity);
            eventComponent.view = this;
        }
    }
}