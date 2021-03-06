using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class BezierCurve : MonoBehaviour
{
    private LineRenderer curveLr;
    private LineRenderer controlLr;
    private LineRenderer convLr;
    private Transform cloudPointContainer;
    [SerializeField]
    private List<Vector3> controlPoints;
    [SerializeField]
    private LayerMask controlPointLayer;
    private ControlPointController dragControlPointIndex;

    private List<Vector3> curvePoints;
    public List<Vector3> ControlPoints { get => controlPoints; set => controlPoints = value; }
    public List<Vector3> CurvePoints { get => curvePoints; set => curvePoints = value; }
    public List<Vector3> CloudsPoints { get; private set; } = new List<Vector3>();
    public Transform CloudPointContainer { get => cloudPointContainer; set => cloudPointContainer = value; }

    public List<Vector3> NormalsTab = new List<Vector3>();

    
    [SerializeField]
    private Tangente tan;
    private bool CloseCurve = true;


    private void Awake()
    {
        cloudPointContainer = transform.GetChild(0);
        if (!TryGetComponent(out curveLr))
        {
            throw new System.Exception("Wsh met un LineRenderer avec le BezierCurve stp");
        }
        if (!transform.GetChild(1).TryGetComponent(out controlLr))
        {
            throw new System.Exception("Wsh met un LineRenderer sur l'enfant stp");
        }
        if (!controlLr.transform.GetChild(0).TryGetComponent(out convLr))
        {
            throw new System.Exception("Wsh met un LineRenderer sur l'enfant de l'enfant stp");
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
        tan.GetTan(this);
        curveLr.positionCount = curvePoints.Count;
        for (int i = 0; i < curvePoints.Count; i++)
        {
            curveLr.SetPosition(i, curvePoints[i]);
            if (i + 1 < curvePoints.Count)
            {
                //if (curvePoints[i] != curvePoints[i + 1])
                {

                    Extrusion.Instance.CreatePointsForStep(curvePoints[i], curvePoints[i + 1], CloseCurve, tan.tan[i],/* NormalsTab,*/ cloudPointContainer);

                }
            }
        }
        //clearGM();
        GetCloudPoint();
        Extrusion.Instance.CreateFace(NormalsTab);
    }

    private void GetCloudPoint()
    {
        for (int i = 0; i < cloudPointContainer.childCount; i++)
        {
            Transform child = cloudPointContainer.GetChild(i);
            for (int j = 0; j < child.childCount; j++)
            {
                Transform grandChild = child.GetChild(j);
                CloudsPoints.Add(transform.TransformPoint(grandChild.position));
            }
        }
        //    for (int i = 0; i <cloudPointContainer.childCount; i++)
        //{
        //    Transform child = cloudPointContainer.GetChild(i);
        //    CloudsPoints.Add(child.position);
        //}
    }

    private void ShowControlCurve()
    {
        controlLr.positionCount = controlPoints.Count;
        for (int i = 0; i < controlPoints.Count; i++)
        {
            Vector3 v = controlPoints[i];
            controlLr.SetPosition(i, v);
        }
    }

    private void ShowConvexHullCurve()
    {
        List<Vector3> conv = GiftWrappingAlgorithm.CalculateConvexHull(ControlPoints);
        convLr.positionCount = conv.Count;
        for (int i = 0; i < conv.Count; i++)
            convLr.SetPosition(i, conv[i]);
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
        CloudsPoints = new List<Vector3>();

        ControlPointController cp;
        for (int i = 0; i <cloudPointContainer.childCount; i++)
        {
            if (cloudPointContainer.GetChild(i).TryGetComponent(out cp))
            {
                cp.Destroy();
            }
        }
        NormalsTab = new List<Vector3>();
    }

    private void Update()
    {
        Matrix4x4 m = new Matrix4x4(new Vector4(1,2,5,6), new Vector4(8, 0, 4, 0), new Vector4(6,2,4,7), new Vector4(8,9,2,1));
        Vector3 ouais = new Vector3(4, 5, 9);
        Vector4 quatre = new Vector4(4, 5, 9, 1);

        Vector3 res = m.MultiplyPoint(ouais);
        Vector3 res2 = m.MultiplyVector(ouais);

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

        if (Input.GetMouseButtonDown(2))
        {
            RaycastHit hit;
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!IsPointerOverUIObject())
            {
                if (Physics.Raycast(r, out hit, GameManager.Instance.RayDistance))
                {
                    if ((controlPointLayer.value & (1 << hit.collider.gameObject.layer)) > 0)
                    {
                        ControlPointController cpc;
                        if (hit.collider.gameObject.TryGetComponent(out cpc))
                        {
                            controlPoints.Insert(cpc.Index, cpc.transform.position);
                            DrawCurve();
                        }
                    }
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

    public void DrawCurve(bool RemoveControl = true)
    {
        //Debug.Log("when is this called");
        if (controlPoints != null && controlPoints.Count > 0)
        {
            curvePoints = DeCasteljauAlgorithmUtils.CalculateCurvePoints(new List<Vector3>(controlPoints), GameManager.Instance.Step).Distinct().ToList();
            if (RemoveControl)
                RemoveCurvePoint();
            if (RemoveControl)
                ShowControlPoint();
            if (controlPoints.Count > 1)
            {
                ShowCurve();
                ShowControlCurve();
                ShowConvexHullCurve();
            }
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
            //DrawCurve(false);
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
            for (int i = 1; i < transform.childCount; i++)
            {
                Renderer renderer = transform.GetChild(i).GetComponent<Renderer>();
                renderer.sharedMaterial = mat;
            }
        }
    }
    //private void clearGM()
    //{
    //    for (int i = 0; i <cloudPointContainer.childCount; i++)
    //    {
    //        Transform child =cloudPointContainer.GetChild(i);
    //        for (int j = 0; j < child.childCount; j++)
    //        {
    //            Transform grandChild = child.GetChild(j);
    //            grandChild.parent =cloudPointContainer;
    //        }

    //        Transform child2 =cloudPointContainer.GetChild(i);
    //        for (int j = 0; j < child2.childCount; j++)
    //        {
    //            Transform grandChild = child2.GetChild(j);
    //            grandChild.parent =cloudPointContainer;
    //        }
    //        Transform child3 =cloudPointContainer.GetChild(i);
    //        for (int j = 0; j < child3.childCount; j++)
    //        {
    //            Transform grandChild = child3.GetChild(j);
    //            grandChild.parent =cloudPointContainer;
    //        }
    //    }
    //    for (int i = 0; i <cloudPointContainer.childCount; i++)
    //    {
    //        Transform child =cloudPointContainer.GetChild(i);
    //        if (child.name == "Container(Clone)")
    //        {
    //            child.GetComponent<ControlPointController>().Destroy();
    //        }
    //    }
    //}
}
