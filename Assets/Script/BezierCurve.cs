using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    private LineRenderer curveLr;
    private LineRenderer controlLr;
    [SerializeField] private GameObject ControlPointGo;
    [SerializeField] private List<Vector3> controlPoints;
    [Header("Parameters B�zier")]
    [Range(0.00000001f, 1.0f)]
    [SerializeField] private float step;
    [SerializeField] private PrimitiveType drawer;

    [Header("Parameters others")]
    [SerializeField] private float rayDistance = 100f;

    [SerializeField] private LayerMask controlPointLayer;

    private ControlPointController dragControlPointIndex;
    private List<Vector3> curvePoints;

    private void Awake()
    {
        if (controlPoints != null && controlPoints.Count > 0)
        {
            curvePoints = DeCasteljauAlgorithmUtils.CalculateCurvePoints(controlPoints, step);
        }
        if (!TryGetComponent(out curveLr))
        {
            throw new System.Exception("Wsh met un LineRenderer avec le BezierCurve stp");
        }
        if (!GameObject.Find("OriginPointLine").TryGetComponent(out controlLr))
        {
            throw new System.Exception("Wsh met un LineRenderer sur un empty OriginPointLine stp");
        }
        if (ControlPointGo == null)
        {
            throw new System.Exception("Wsh donne moi un prefab pour le control point !");

        }
        if (controlLr != null)
        {
            Debug.Log("yo");
        }
    }

    private void ShowControlPoint()
    {
        for (int i = 0; i < controlPoints.Count; i++)
        {
            GameObject go = Instantiate(ControlPointGo, controlPoints[i], Quaternion.identity, transform);
            ControlPointController cpc = go.GetComponent<ControlPointController>();
            cpc.Index = i;
        }
    }
    private void ShowCurve()
    {
        curveLr.positionCount = curvePoints.Count;
        for (int i = 0; i < curvePoints.Count; i++)
        {
            curveLr.SetPosition(i, curvePoints[i]);
        }
    }
    private void ShowControlCurve()
    {
        controlLr.positionCount = controlPoints.Count;
        for (int i = 0; i < controlPoints.Count; i++)
        {
            controlLr.SetPosition(i, controlPoints[i]);
        }
    }

    void RemoveCurvePointData()
    {
        curvePoints = new List<Vector3>();
        controlPoints = new List<Vector3>();
    }
    private void RemoveCurvePoint()
    {

        ControlPointController cpc;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out cpc))
            {
                cpc.Destroy();
            }
        }
        curveLr.positionCount = 0;
        controlLr.positionCount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            dragControlPointIndex = null;
            DrawCurve();
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(r, out hit, rayDistance))
            {
                if ((controlPointLayer.value & (1 << hit.collider.gameObject.layer)) > 0)
                {
                    Debug.Log(hit.collider.gameObject.layer);

                    ControlPointController cpc;
                    if (hit.collider.gameObject.TryGetComponent(out cpc))
                    {
                        dragControlPointIndex = cpc;
                    }
                }
            }
            else
            {
                controlPoints.Add(GetWorldPos());
                DrawCurve();
            }
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    RemoveCurvePoint();
        //    RemoveCurvePointData();
        //}

        DragControlPoint();
    }


    void DrawCurve(bool RemoveControl = true)
    {
        if (controlPoints != null && controlPoints.Count > 0)
        {
            if (RemoveControl)
                RemoveCurvePoint();
            curvePoints = DeCasteljauAlgorithmUtils.CalculateCurvePoints(new List<Vector3>(controlPoints), step);
            if (RemoveControl)
                ShowControlPoint();
            ShowCurve();
            ShowControlCurve();
        }
    }

    public Vector3 GetWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        return worldPosition;
    }
    void DragControlPoint()
    {
        if (dragControlPointIndex != null)
        {
            dragControlPointIndex.transform.position = GetWorldPos();
            controlLr.SetPosition(dragControlPointIndex.Index, GetWorldPos());
            controlPoints[dragControlPointIndex.Index] = GetWorldPos();
            DrawCurve(false);
        }
    }


    [SerializeField] private List<Vector3> m_controlPoints;
    private List<Vector3> m_curvePoints;

    public void CreateCurve(List<Vector3> controlPoints, float step, PrimitiveType drawer)
    {
        m_controlPoints = controlPoints;
        m_curvePoints = DeCasteljauAlgorithmUtils.CalculateCurvePoints(m_controlPoints, step);
        GameObject go = GameObject.CreatePrimitive(drawer);
        foreach (Vector3 point in m_curvePoints)
        {
            Instantiate(go, point, Quaternion.identity, transform);
        }
        Destroy(go);
    }
}
