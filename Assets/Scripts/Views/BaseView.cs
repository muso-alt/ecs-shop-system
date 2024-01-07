using System;
using UnityEngine;

namespace ShopComplex.Views
{
    public class BaseView : MonoBehaviour
    {
        [SerializeField] private RectTransform _content;

        public RectTransform Content => _content;

        private void OnValidate()
        {
            _content = GetComponent<RectTransform>();
        }
    }
}