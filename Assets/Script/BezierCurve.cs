using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    private LineRenderer curveLr; 
    private LineRenderer controlLr;
    [SerializeField] private GameObject ControlPointGo;
    [SerializeField] private List<Vector3> controlPoints;
    [Header("Parameters Bézier")]
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
        //GameObject go = GameObject.CreatePrimitive(drawer);
        for (int i = 0; i < controlPoints.Count; i++) { 
            GameObject go = Instantiate(ControlPointGo, controlPoints[i], Quaternion.identity, transform);
            ControlPointController cpc = go.GetComponent<ControlPointController>();
            cpc.Index = i;
        }
        //Destroy(go);
    }
    private void ShowCurve()
    {
        curveLr.positionCount = curvePoints.Count;
        for (int i = 0; i < curvePoints.Count; i++)
        {
            curveLr.SetPosition(i, curvePoints[i]);
        }

        controlLr.positionCount = controlPoints.Count;
        for (int i = 0; i < controlPoints.Count; i++)
        {
            Debug.Log(controlPoints[i]);
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
            if(transform.GetChild(i).TryGetComponent(out cpc))
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
                    if(hit.collider.gameObject.TryGetComponent(out cpc))
                    {
                        dragControlPointIndex = cpc;
                    }
                }
            }
            else
            {
                controlPoints.Add(getWorldPos());
                if (controlPoints != null && controlPoints.Count > 0)
                {
                    RemoveCurvePoint();
                    curvePoints = DeCasteljauAlgorithmUtils.CalculateCurvePoints(new List<Vector3>(controlPoints), step);
                    ShowControlPoint();
                    ShowCurve();
                    dragControlPointIndex = null;
                }
            }
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    RemoveCurvePoint();
        //    RemoveCurvePointData();
        //}

        DagControlPoint();
    }

    public Vector3 getWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        return worldPosition;
    }
    void DagControlPoint()
    {
        if (dragControlPointIndex != null)
        {
            
            controlPoints[dragControlPointIndex.Index] = getWorldPos();

        }
    }
}
