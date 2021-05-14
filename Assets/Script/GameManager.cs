using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    [SerializeField] private List<BezierCurve> listCurves = new List<BezierCurve>();
    [SerializeField] private List<Vector3> selectedPoints;

    [Header("Parameters")]
    [Range(0.00000001f, 1.0f)]
    [SerializeField] private float m_step;
    [SerializeField] private PrimitiveType m_drawer;

    public void GenerateCurve()
    {
        GameObject curveObject = new GameObject();
        BezierCurve newCurve = curveObject.AddComponent<BezierCurve>();
        newCurve.CreateCurve(selectedPoints, m_step, m_drawer);
        listCurves.Add(newCurve);
        selectedPoints.Clear();
    }

    public void RemoveCurveAt(int index)
    {
        listCurves.RemoveAt(index);
    }

    public void SelectPoint(Vector3 point)
    {
        selectedPoints.Add(point);
    }

    public void RemoveSelection(Vector3 point)
    {
        selectedPoints.Remove(point);
    }
}
