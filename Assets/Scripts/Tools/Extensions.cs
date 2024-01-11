using System.Linq;
using Leopotam.EcsLite;
using ShopComplex.Components;
using UnityEngine;

namespace ShopComplex.Tools
{
    public static class Extensions
    {
        public static ref ItemCmp ConfigureItemEntity(this int entity, EcsWorld ecsWorld)
        {
            var itemsPool = ecsWorld.GetPool<ItemCmp>();
            ref var itemCmp = ref itemsPool.Add(entity);
            itemCmp.ItemPlace = Place.FastBuy;
            return ref itemCmp;
        }
        
        public static ref DragCmp ConfigureDragEntity(this int entity, EcsWorld ecsWorld)
        {
            var dragPool = ecsWorld.GetPool<DragCmp>();
            ref var dragCmp = ref dragPool.Add(entity);
            dragCmp.CanDrag = false;
            return ref dragCmp;
        }

        public static bool IsInsideOtherRect(this RectTransform rect, RectTransform other)
        {
            var childCorners = new Vector3[4];
            rect.GetWorldCorners(childCorners);

            return childCorners.All(corner =>
                RectTransformUtility.RectangleContainsScreenPoint(other, corner));
        }
    }
}