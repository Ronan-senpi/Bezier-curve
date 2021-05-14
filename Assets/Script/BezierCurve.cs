using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{

    [SerializeField] private List<Vector3> controlPoints;
    [Header("Parameters")]
    [Range(0.00000001f,1.0f)]
    [SerializeField] private float step;
    [SerializeField] private PrimitiveType drawer;

    private List<Vector3> curvePoints;

    private void Awake()
    {
        curvePoints = DeCasteljauAlgorithmUtils.CalculateCurvePoints(controlPoints, step);
        GameObject go = GameObject.CreatePrimitive(drawer);
        foreach (Vector3 point in curvePoints)
        {
            Instantiate(go, point, Quaternion.identity, transform);
        }
        Destroy(go);
    }
}
