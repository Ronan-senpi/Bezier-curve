using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeStep : MonoBehaviour
{
    private GameManager manager;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        StepModifier(manager.Step);
    }

    void StepModifier(float step)
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if (step <= 0.9f)
            {
                step += 0.1f;
            }
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            if (step >= 0.2f)
            {
                step -= 0.1f;
            }
        }
    }
}
