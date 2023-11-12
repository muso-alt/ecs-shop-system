﻿using UnityEngine.EventSystems;

namespace Runtime.Core.Views
{
    public interface IPointerEvent
    {
        public int Entity { get; set; }
        public PointerEventData EventData { get; set; }
    }
}