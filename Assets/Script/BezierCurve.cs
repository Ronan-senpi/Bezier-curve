using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{

    [SerializeField] private List<Vector3> m_controlPoints;
    private List<Vector3> m_curvePoints;
    
    public float Step { get => step; set => step = value; }

    public void CreateCurve(List<Vector3> controlPoints, float step, PrimitiveType drawer){
        m_controlPoints=controlPoints;
        m_curvePoints = DeCasteljauAlgorithmUtils.CalculateCurvePoints(m_controlPoints, step);
    private List<Vector3> curvePoints;

    

    private void Awake()
    {
        curvePoints = DeCasteljauAlgorithmUtils.CalculateCurvePoints(controlPoints, Step);
        GameObject go = GameObject.CreatePrimitive(drawer);
        foreach (Vector3 point in m_curvePoints)
        {
            Instantiate(go, point, Quaternion.identity, transform);
        }
        Destroy(go);
    }
}
