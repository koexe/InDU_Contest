using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ParkMandooUtility 
{
    public static Rect GetOverlapRect(Rect rectA, Rect rectB)
    {
        if (rectA.Overlaps(rectB))
        {
            float minX = Mathf.Max(rectA.xMin, rectB.xMin);
            float maxX = Mathf.Min(rectA.xMax, rectB.xMax);
            float minY = Mathf.Max(rectA.yMin, rectB.yMin);
            float maxY = Mathf.Min(rectA.yMax, rectB.yMax);

            return new Rect(minX, minY, maxX - minX, maxY - minY);
        }

        // 겹치는 게 없으면 크기 0짜리 Rect 반환
        return Rect.zero;
    }
}
