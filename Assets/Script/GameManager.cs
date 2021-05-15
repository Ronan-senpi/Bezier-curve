using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    public List<BezierCurve> listCurves = new List<BezierCurve>();

    [Header("Parameters")]
    [SerializeField] private GameObject bezierCurvePrefab;
    [SerializeField] private GameObject convexHullPrefab;
    [Range(0.001f, 1.0f)]
    [SerializeField] private float m_step;
    public float Step { get => m_step; set => m_step = value; }
    [SerializeField] private float rayDistance = 100f;
    public float RayDistance { get { return rayDistance; } }
    [SerializeField] private GameObject controlPointGo;
    public GameObject ControlPointGo { get { return controlPointGo; } }
    public int selectedCurve = 0;

    [SerializeField] private Material selectMaterial;
    [SerializeField] private Material unselectMaterial;
    [SerializeField] private Slider sliderStep;
    public void SaveCurveAndStartNew()
    {
        BezierCurve currentCurve = Instantiate(bezierCurvePrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<BezierCurve>();
        if (selectedCurve < listCurves.Count && listCurves.Count > 0)
        {
            listCurves[selectedCurve].enabled = false;
            listCurves[selectedCurve].OnSelectionChange(unselectMaterial);
        }
        listCurves.Add(currentCurve);
        selectedCurve = listCurves.Count - 1;
    }


    public void RemoveSelectedCurve()
    {
        if (listCurves.Count > 0)
        {
            BezierCurve curveToDestroy = listCurves[selectedCurve];
            listCurves.RemoveAt(selectedCurve);
            Destroy(curveToDestroy.gameObject);
            selectedCurve = Mathf.Clamp(selectedCurve, 0, listCurves.Count - 1);
        }

    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.KeypadPlus))
        //{
        //    if (m_step <= 0.9f)
        //    {
        //        m_step += 0.1f;
        //        Debug.Log("Increase");
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.KeypadMinus))
        //{
        //    if (m_step >= 0.2f)
        //    {
        //        m_step -= 0.1f;
        //        Debug.Log("Decrease");
        //    }
        //}

        m_step = sliderStep.value;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            listCurves[selectedCurve].OnSelectionChange(unselectMaterial);
            listCurves[selectedCurve].enabled = false;
            if (selectedCurve > 0)
                selectedCurve--;
            else
                selectedCurve = listCurves.Count - 1;
            listCurves[selectedCurve].OnSelectionChange(selectMaterial);
            listCurves[selectedCurve].enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            listCurves[selectedCurve].OnSelectionChange(unselectMaterial);
            listCurves[selectedCurve].enabled = false;
            if (selectedCurve < listCurves.Count - 1)
                selectedCurve++;
            else
                selectedCurve = 0;
            listCurves[selectedCurve].OnSelectionChange(selectMaterial);
            listCurves[selectedCurve].enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            RemoveSelectedCurve();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            DrawConvexHull();
        }
    }

    private void DrawConvexHull()
    {
        LineRenderer convLr = Instantiate(convexHullPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<LineRenderer>();
        List<Vector3> conv = GiftWrappingAlgorithm.CalculateConvexHull(listCurves[selectedCurve].ControlPoints);
        convLr.positionCount = conv.Count;
        for (int i = 0; i < conv.Count; i++)
            convLr.SetPosition(i, conv[i]);
    }
}
