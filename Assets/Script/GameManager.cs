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
            if (instance == null) instance = FindObjectOfType<GameManager>();
            return instance;
        }
    }

    public List<GameObject> listCurves = new List<GameObject>();
    public List<Vector3> selectedPoints;

    [Header("Parameters")]
    [Range(0.00000001f, 1.0f)]
    [SerializeField] private float m_step;
    public float Step { get => m_step; set => m_step = value; }
    [SerializeField] private PrimitiveType m_drawer;

    public int selectedCurve = 0;

    public void GenerateCurve()
    {
        GameObject curveObject = new GameObject();
        BezierCurve newCurve = curveObject.AddComponent<BezierCurve>();
        newCurve.CreateCurve(selectedPoints, m_step, m_drawer);
        listCurves.Add(curveObject);
        selectedCurve = listCurves.Count - 1;
        selectedPoints.Clear();
    }

    public void RemoveSelectedCurve()
    {
        if (listCurves.Count > 0)
        {
            GameObject curveToDestroy = listCurves[selectedCurve];
            listCurves.RemoveAt(selectedCurve);
            Destroy(curveToDestroy);
            selectedCurve = Mathf.Clamp(selectedCurve, 0, listCurves.Count - 1);
        }

    }

    public void SelectPoint(Vector3 point)
    {
        selectedPoints.Add(point);
    }

    public void RemoveSelection(Vector3 point)
    {
        selectedPoints.Remove(point);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (m_step <= 0.9f)
            {
                m_step += 0.1f;
                Debug.Log("Increase");
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (m_step >= 0.2f)
            {
                m_step -= 0.1f;
                Debug.Log("Decrease");
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (selectedCurve > 0)
                selectedCurve--;
            else
                selectedCurve = listCurves.Count - 1;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (selectedCurve < listCurves.Count - 1)
                selectedCurve++;
            else
                selectedCurve = 0;
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            RemoveSelectedCurve();
        }
    }
}
