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

    public void Set2DProfileNbPoint(int value)
    {
        profileNbPoint = value < 3 ? 3 : value;
    }

    public List<Vector3> CreatePointsForStep(Vector3 location, Vector3 nextLocation, bool showPoint = false)
    {
        GameObject cont = null;
        Debug.Log(profileNbPoint);
        List<Vector3> vs = new List<Vector3>();
        if (showPoint)
            cont = Instantiate(this.container, location, Quaternion.identity, gameObject.transform);

        for (int i = 0; i < profileNbPoint; i++)
        {
            float angle = i * Mathf.PI * 2f / profileNbPoint;
            Vector3 newPos = (location + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius);
            if (showPoint)
                Instantiate(point, newPos, Quaternion.identity, cont.transform);

            vs.Add(newPos);
        }
        if (showPoint)
            cont.transform.LookAt(nextLocation);
        return vs;
    }

}