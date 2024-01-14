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

        public static bool IsHalfInsideOtherRect(this RectTransform rect, RectTransform other)
        {
            var childCorners = new Vector3[4];
            rect.GetWorldCorners(childCorners);
            var inCornerCount = 0;

            foreach (var childCorner in childCorners)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(other, childCorner))
                {
                    inCornerCount++;
                }
            }

            return inCornerCount >= 2;
        }

        public static bool IsInsideOtherRectByPosition(this RectTransform rect, RectTransform other)
        {
            var uiObjectCenter = rect.position;

            var parentCorners = new Vector3[4];
            other.GetWorldCorners(parentCorners);

            var parentBottomLeft = parentCorners[0];
            var parentTopRight = parentCorners[2];

            return uiObjectCenter.x > parentBottomLeft.x &&
                   uiObjectCenter.x < parentTopRight.x &&
                   uiObjectCenter.y > parentBottomLeft.y &&
                   uiObjectCenter.y < parentTopRight.y;
        }
    }
}