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
    private GameObject cube;
    [SerializeField]
    private GameObject container;
    int profileNbPoint = 3;


    public void CreatePointsForStep(Vector3 location, Vector3 nextLocation, bool closeProfile, Vector3 rotation,  Transform container)

    {
        GameObject cont = null;
        List<Vector3> vs = new List<Vector3>();

        cont = Instantiate(this.container, location, Quaternion.identity, container);
        Vector3? firstPoint = null;
        for (int i = 0; i < profileNbPoint; i++)
        {
            float angle = i * Mathf.PI * 2f / profileNbPoint;
            Vector3 newPos = (location + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius);
            if (closeProfile && i == 0)
            {
                firstPoint = newPos;
            }
            Instantiate(point, newPos, Quaternion.identity, cont.transform);

            vs.Add(newPos);
        }
        if (firstPoint.HasValue && closeProfile)
        {
            Instantiate(cube, firstPoint.Value, Quaternion.identity, cont.transform);
            vs.Add(firstPoint.Value);
        }
        cont.transform.rotation = Quaternion.LookRotation(rotation);
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Game Manager Children : " + GameManager.Instance.transform.childCount);
            Debug.Log("Cloud points Count : " + GameManager.Instance.listCurves[GameManager.Instance.selectedCurve].CloudsPoints.Count);
            CreateFace();
        }
    }
    public void CreateFace()
    {
        BezierCurve curveToDraw = GameManager.Instance.listCurves[GameManager.Instance.selectedCurve];

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        List<int> indices = new List<int>();

        for (int i = 0; i < profileNbPoint - 1; i++)
        {
            for (int j = 0; j < (curveToDraw.CloudsPoints.Count / profileNbPoint) - 1; j++)
            {
                //backface
                //indices.Add(i * profileNbPoint + j);
                //indices.Add(i * profileNbPoint + j + 1);
                //indices.Add((i + 1) * profileNbPoint + j + 1);
                //indices.Add((i + 1) * profileNbPoint + j);

                //frontface
                indices.Add(i * profileNbPoint + j);
                indices.Add((i + 1) * profileNbPoint + j);
                indices.Add((i + 1) * profileNbPoint + j + 1);
                indices.Add(i * profileNbPoint + j + 1);

                //indices.Add(i + j * profileNbPoint);
                //indices.Add(i + (j + 1) * profileNbPoint);
                //indices.Add((i + 1) + (j + 1) * profileNbPoint);
                //indices.Add((i + 1) + j * profileNbPoint);
            }

        }

        mesh.SetVertices(curveToDraw.CloudsPoints.ToArray());
        mesh.SetIndices(indices.ToArray(), MeshTopology.Quads, 0);
        mesh.RecalculateNormals();
    }
}
