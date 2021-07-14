using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangente : MonoBehaviour
{
    public List<Vector3> tan;

    public void GetTan(BezierCurve curve)
    {
        tan = new List<Vector3>();
        if (curve.ControlPoints.Count < 2) return;
        for (int i = 0; i < curve.CurvePoints.Count; i++)
        {
            Vector3 point = Vector3.zero;
            if (i == 0) point = curve.ControlPoints[0] - curve.ControlPoints[1];
            else if(i == curve.CurvePoints.Count - 1) point = 
                    curve.ControlPoints[curve.ControlPoints.Count - 1] - curve.ControlPoints[curve.ControlPoints.Count - 2];
            else point = curve.CurvePoints[i + 1] - curve.CurvePoints[i - 1];
            point = point.normalized;
            tan.Add(point);
        }
    }
}
