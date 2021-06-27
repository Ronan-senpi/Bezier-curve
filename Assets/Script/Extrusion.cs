using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Extrusion : MonoBehaviour
{
    private static Extrusion instance;
    public static Extrusion Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<Extrusion>();
            return instance;
        }
    }

    [SerializeField]
    private MeshFilter meshFilter;
    [SerializeField]
    private float radius;
    [SerializeField]
    private GameObject point;
    [SerializeField]
    private GameObject container;
    int profileNbPoint = 3;


    public List<Vector3> CreatePointsForStep(Vector3 location, Vector3 nextLocation, bool showPoint = false)
    {
        GameObject cont = null;
        Debug.Log(profileNbPoint);
        List<Vector3> vs = new List<Vector3>();

        cont = Instantiate(this.container, location, Quaternion.identity, gameObject.transform);

        for (int i = 0; i < profileNbPoint; i++)
        {
            float angle = i * Mathf.PI * 2f / profileNbPoint;
            Vector3 newPos = (location + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius);
            Instantiate(point, newPos, Quaternion.identity, cont.transform);

            vs.Add(newPos);
        }

        cont.transform.LookAt(nextLocation);

        return vs;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            CreateFace();
    }
    public void CreateFace()
    {
        BezierCurve curveToDraw = GameManager.Instance.listCurves[GameManager.Instance.selectedCurve];

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        //Vector3[] vertices =
        //{
        //    new Vector3(0,0,1),
        //    new Vector3(0,1,1),
        //    new Vector3(1,1,1),
        //    new Vector3(1,0,1),

        //    new Vector3(0,0,0), //0
        //    new Vector3(0,1,0), //1
        //    new Vector3(1,1,0), //3
        //    new Vector3(1,0,0), //2
        //};

        List<int> indices = new List<int>();

        for (int i = 0; i < curveToDraw.CurvePoints.Count - 2; i++)
        {
            for (int j = 0; j < profileNbPoint - 1; j++)
            {
                indices.Add(i * profileNbPoint + j);
                indices.Add(i * profileNbPoint + j + 1);
                indices.Add((i + 1) * profileNbPoint + j + 1);

                indices.Add((i + 1) * profileNbPoint + j + 1);
                indices.Add((i + 1) * profileNbPoint + j);
                indices.Add(i * profileNbPoint + j);
            }
        }
        //int bezierPoint = 2;
        //int profilPoint = 4;
        //for (int i = 0; i < bezierPoint - 1; i++)
        //{
        //    for (int j = 0; j < profilPoint - 1; j++)
        //    {
        //        //indices.Add(i * profilPoint + j);
        //        //indices.Add(i * profilPoint + j + 1);
        //        //indices.Add((i + 1) * profilPoint + j + 1);

        //        //indices.Add((i + 1) * profilPoint + j + 1);
        //        //indices.Add((i + 1) * profilPoint + j);
        //        //indices.Add(i * profilPoint + j);

        //        indices.Add(i * profilPoint + j);
        //        indices.Add(i * profilPoint + j + 1);
        //        indices.Add((i + 1) * profilPoint + j + 1);
        //        indices.Add((i + 1) * profilPoint + j);

        //        indices.Add((i + 1) * profilPoint + j);
        //        indices.Add((i + 1) * profilPoint + j + 1);
        //        indices.Add(i * profilPoint + j + 1);
        //        indices.Add(i * profilPoint + j);
        //    }
        //}
        //Debug.Log(indices);
        Debug.Log("Size bezier : " + curveToDraw.CurvePoints.Count);
        Debug.Log("Size indices : " + indices.Count);
        mesh.SetVertices(curveToDraw.CloudsPoint.ToArray());
        mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
        //mesh.SetVertices(vertices);
        //mesh.SetIndices(indices, MeshTopology.Quads, 0);
        mesh.RecalculateNormals();
    }
}
