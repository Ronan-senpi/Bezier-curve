using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReload : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            GameManager.Instance.GetCurrentCurve().DrawCurve();
        }  
    }
}
