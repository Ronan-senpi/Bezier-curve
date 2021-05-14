using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStep : MonoBehaviour
{
    private BezierCurve bezier;

    private void Start()
    {
        bezier = FindObjectOfType<BezierCurve>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (bezier.Step <= 0.9f)
            {
                bezier.Step += 0.1f;
                Debug.Log("Increase");
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (bezier.Step >= 0.2f) {
                bezier.Step -= 0.1f;
                Debug.Log("Decrease");
            }
        }
    }
}
