using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BezierCurve : MonoBehaviour
{
    private LineRenderer curveLr;
    private LineRenderer controlLr;
    [SerializeField] private List<Vector3> controlPoints;
    [SerializeField] private LayerMask controlPointLayer;
    private ControlPointController dragControlPointIndex;
    private List<Vector3> curvePoints;

    public List<Vector3> ControlPoints { get => controlPoints; set => controlPoints = value; }

    private void Awake()
    {
        if (!TryGetComponent(out curveLr))
        {
            throw new System.Exception("Wsh met un LineRenderer avec le BezierCurve stp");
        }
        if (!transform.GetChild(0).TryGetComponent(out controlLr))
        {
            throw new System.Exception("Wsh met un LineRenderer sur un empty OriginPointLine stp");
        }
        if (GameManager.Instance.ControlPointGo == null)
        {
            throw new System.Exception("Wsh donne moi un prefab pour le control point !");

        }
    }

    private void ShowControlPoint()
    {
        for (int i = 0; i < controlPoints.Count; i++)
        {
            GameObject go = Instantiate(GameManager.Instance.ControlPointGo, controlPoints[i], Quaternion.identity, transform);
            go.name = i.ToString();
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
            Vector3 v = controlPoints[i];
            v.z = .1f;
            controlLr.SetPosition(i, v);
        }
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
            if (!IsPointerOverUIObject())
            {
                if (Physics.Raycast(r, out hit, GameManager.Instance.RayDistance))
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
        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!IsPointerOverUIObject())
            {
                if (Physics.Raycast(r, out hit, GameManager.Instance.RayDistance))
                {
                    if ((controlPointLayer.value & (1 << hit.collider.gameObject.layer)) > 0)
                    {
                        Debug.Log(hit.collider.gameObject.layer);

                        ControlPointController cpc;
                        if (hit.collider.gameObject.TryGetComponent(out cpc))
                        {
                            int i = cpc.Index;
                            cpc.Destroy();
                            controlPoints.RemoveAt(i);
                            DrawCurve();
                        }
                    }
                }
            }
        }

        DragControlPoint();
    }


    void DrawCurve(bool RemoveControl = true)
    {
        if (controlPoints != null && controlPoints.Count > 0)
        {
            if (RemoveControl)
                RemoveCurvePoint();
            curvePoints = DeCasteljauAlgorithmUtils.CalculateCurvePoints(new List<Vector3>(controlPoints), GameManager.Instance.Step);
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

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void OnSelectionChange(Material mat)
    {
        if (transform.childCount > 1)
        {
            Debug.Log("On est la");
            for (int i = 1; i < transform.childCount; i++)
            {
                Renderer renderer = transform.GetChild(i).GetComponent<Renderer>();
                renderer.sharedMaterial = mat;
            }
        }

    }
}
