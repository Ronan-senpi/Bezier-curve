using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Extrusion : MonoBehaviour
{
    [SerializeField]
    Mesh mesh;
    
    private BezierCurve curve;
    private List<Vector3> vertex;

    // Start is called before the first frame update
    void Start()
    {
        vertex = GetVertices(mesh);
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Vector3 points in curve.ControlPoints)
        {
            Mesh m = Instantiate(mesh, points, Quaternion.identity);
        }
    }

    private List<Vector3> GetVertices(Mesh mesh)
    {
        Vector3[] vert = mesh.vertices;

        List<Vector3> VertexList = new List<Vector3>();
        foreach (Vector3 vertice in vert)
        {
            VertexList.Add(vertice);
        }

        VertexList = VertexList.Distinct().ToList();

        return VertexList;
    }

    private Vector3 GetTangent(BezierCurve curve, int n)
    {
        Vector3 tangent;

        if (n == 0) tangent = curve.ControlPoints[1] - curve.ControlPoints[0];
        else if (n == curve.ControlPoints.Count - 1) tangent = curve.ControlPoints[curve.ControlPoints.Count - 1] - curve.ControlPoints[curve.ControlPoints.Count - 2];
        else tangent = curve.ControlPoints[n + 1] - curve.ControlPoints[n - 1];

        return tangent.normalized;
    }
}
