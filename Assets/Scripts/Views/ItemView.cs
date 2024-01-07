using Leopotam.EcsLite;
using ShopComplex.Components;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ShopComplex.Views
{
    public class ItemView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private RectTransform _rect;

        public EcsPackedEntityWithWorld PackedEntityWithWorld { get; set; }
        public EcsWorld EcsEventWorld { get; set; }
        public RectTransform Rect => _rect;
        
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
            eventComponent.View = this;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            var entity = EcsEventWorld.NewEntity();
            ref var eventComponent = ref EcsEventWorld.GetPool<DragBeginEvent<ItemView>>().Add(entity);
            
            eventComponent.View = this;
        }

        public void OnDrag(PointerEventData eventData)
        {
           
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            var entity = EcsEventWorld.NewEntity();
            ref var eventComponent = ref EcsEventWorld.GetPool<DragEndEvent<ItemView>>().Add(entity);
            
            eventComponent.View = this;
        }
        
    }
}