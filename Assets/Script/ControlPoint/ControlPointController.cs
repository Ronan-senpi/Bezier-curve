using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPointController : MonoBehaviour
{
    public int Index { get; set; }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
