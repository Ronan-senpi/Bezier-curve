using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScreenClickPosition : MonoBehaviour
{
    [SerializeField]
    GameObject go;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (go != null)
            {
                go = Instantiate(go, getWorldPos(), Quaternion.identity);
                GameManager.Instance.SelectPoint(go.transform.position);
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


