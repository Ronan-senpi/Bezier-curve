using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStep : MonoBehaviour
{
    float step;

    private void Start()
    {
        step = FindObjectOfType<BezierCurve>().Step;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (step <= 0.9f)
            {
                step += 0.1f;
                Debug.Log("Increase");
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (step > 0.1f) {
                step -= 0.1f;
                Debug.Log("Decrease");
            }
        }
    }
}
