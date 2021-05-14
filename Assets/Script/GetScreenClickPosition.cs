using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GetScreenClickPosition : MonoBehaviour
{
    [SerializeField]
    PrimitiveType drawer;
    GameObject go;

    private void Awake()
    {
        go = GameObject.CreatePrimitive(drawer);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (go != null)
            {
                Instantiate(go, getWorldPos(), Quaternion.identity);
            }
        }
    }
    public Vector3 getWorldPos()
    {
        Vector3 mousePos = Input.mousePosition;
        Debug.Log(mousePos);
        mousePos.z = 10;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.Log(worldPosition);
        return worldPosition;
    }
}


