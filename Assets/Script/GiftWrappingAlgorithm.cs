using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class GiftWrappingAlgorithm
{
    public static List<Vector3> CalculateConvexHull(List<Vector3> points)
    {
        List<Vector3> result = new List<Vector3>();
        Vector3 mostLeftPoint = FindMostLeftPoint(points);
        Vector3 end = Vector3.zero;

        do
        {
            result.Add(mostLeftPoint);
            end = points[0];

            for (int i = 1; i < points.Count; i++)
            {
                if ((end == mostLeftPoint) || (counterclockwise(mostLeftPoint, end, points[i]) < 0))
                    end = points[i];
            }
            mostLeftPoint = end;

            if (end == result[0])
            {
                break;
            }
        } while ((end != result[0]));

        result.Add(result[0]);
        return result;
    }

    private static Vector3 FindMostLeftPoint(List<Vector3> points)
    {
        return points.Where(p => p.x == (points.Min(y => y.x))).First();
    }

    public static float counterclockwise(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Mathf.Sign((p2.x - p1.x) * (p3.y - p1.y) - (p3.x - p1.x) * (p2.y - p1.y));
    }

}