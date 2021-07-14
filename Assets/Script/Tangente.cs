using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangente : MonoBehaviour
{
    public List<Vector3> tan;

    public void GetTan(BezierCurve curve)
    {
        tan = new List<Vector3>();

        for (int i = 1; i < curve.CurvePoints.Count - 1; i++)
        {
            Vector3 point = curve.CurvePoints[i + 1] - curve.CurvePoints[i - 1];
            tan.Add(point);
        }
    }
}
