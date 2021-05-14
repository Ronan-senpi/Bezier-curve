using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DeCasteljauAlgorithmUtils
{
    public static List<Vector3> CalculateCurvePoints(List<Vector3> controlPoints, float step)
    {
        List<Vector3> curvePoints = new List<Vector3>();
        List<Vector3> barycentrePoints = new List<Vector3>();
        for (float t = 0; t < 1; t += step)
        {
            barycentrePoints = controlPoints;
            for (int i = 1; i < controlPoints.Count; i++)
            {
                for (int j = 0; j < controlPoints.Count - i; j++)
                {
                    barycentrePoints[j] = (1 - t) * controlPoints[j] + t * controlPoints[j + 1];
                }
            }
            curvePoints.Add(barycentrePoints[0]);
        }
        return curvePoints;
    }
}
