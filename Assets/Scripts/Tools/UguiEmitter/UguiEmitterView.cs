using System;

using Tools.AB_Utility;

using UnityEngine;
using UnityEngine.EventSystems;

namespace Runtime.Core.Views
{
    public class UguiEmitterView : EcsView, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        protected event Action<PointerEventData> Clicked = delegate { };
        protected event Action<PointerEventData> BeginDrag = delegate { };
        protected event Action<PointerEventData> Drag = delegate { };
        protected event Action<PointerEventData> EndDrag = delegate { };

        public void OnPointerClick(PointerEventData eventData)
        {
            Clicked.Invoke(eventData);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDrag.Invoke(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Drag.Invoke(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDrag.Invoke(eventData);
        }

        protected void SendEvent<T>(PointerEventData eventData) where T : struct, IPointerEvent
        {
            ref var evt = ref World.SendEvent<T>();
            evt.Entity = UnpackedEntity;
            evt.EventData = eventData;
        }
    }
}