using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DropDownManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown dropDown;

    private TransformBezierUtils utils;
    [SerializeField]
    private GameManager manager;

    private int selectedCurve;
    private BezierCurve currentCurve;

    private void Update()
    {
        if (manager.listCurves != null && manager.listCurves.Count > 0)
        {
            selectedCurve = manager.selectedCurve;
            currentCurve = manager.listCurves[selectedCurve];

            if (dropDown.captionText.text == "RotateX") OnRotateX();
            if (dropDown.captionText.text == "RotateY") OnRotateY();
            if (dropDown.captionText.text == "RotateZ") OnRotateZ();
            if (dropDown.captionText.text == "Scale") OnScale();
            if (dropDown.captionText.text == "Translate") OnTranslate();
        }
    }

    private void OnRotateX()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for(int i = 0; i < currentCurve.ControlPoints.Count; i++){
                currentCurve.ControlPoints[i] = utils.RotateX(currentCurve.ControlPoints[i], 10);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            for (int i = 0; i < currentCurve.ControlPoints.Count; i++)
            {
                currentCurve.ControlPoints[i] = utils.RotateXInvert(currentCurve.ControlPoints[i], 10);
            }
        }
    }
    private void OnRotateY()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            for (int i = 0; i < currentCurve.ControlPoints.Count; i++)
            {
                currentCurve.ControlPoints[i] = utils.RotateY(currentCurve.ControlPoints[i], 10);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            for (int i = 0; i < currentCurve.ControlPoints.Count; i++)
            {
                currentCurve.ControlPoints[i] = utils.RotateYInvert(currentCurve.ControlPoints[i], 10);
            }
        }
    }
    private void OnRotateZ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = 0; i < currentCurve.ControlPoints.Count; i++)
            {
                currentCurve.ControlPoints[i] = utils.RotateZ(currentCurve.ControlPoints[i], 10);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            for (int i = 0; i < currentCurve.ControlPoints.Count; i++)
            {
                currentCurve.ControlPoints[i] = utils.RotateZInvert(currentCurve.ControlPoints[i], 10);
            }
        }
    }
    private void OnScale()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = 0; i < currentCurve.ControlPoints.Count; i++)
            {
                currentCurve.ControlPoints[i] = utils.Scale(currentCurve.ControlPoints[i], 0.2f);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            for (int i = 0; i < currentCurve.ControlPoints.Count; i++)
            {
                currentCurve.ControlPoints[i] = utils.ScaleInvert(currentCurve.ControlPoints[i], 0.2f);
            }
        }
    }
    private void OnTranslate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            for (int i = 0; i < currentCurve.ControlPoints.Count; i++)
            {
                currentCurve.ControlPoints[i] = utils.Translate(currentCurve.ControlPoints[i], Vector3.up);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            for (int i = 0; i < currentCurve.ControlPoints.Count; i++)
            {
                currentCurve.ControlPoints[i] = utils.Translate(currentCurve.ControlPoints[i], Vector3.down);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            for (int i = 0; i < currentCurve.ControlPoints.Count; i++)
            {
                currentCurve.ControlPoints[i] = utils.Translate(currentCurve.ControlPoints[i], Vector3.left);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            for (int i = 0; i < currentCurve.ControlPoints.Count; i++)
            {
                currentCurve.ControlPoints[i]= utils.Translate(currentCurve.ControlPoints[i], Vector3.right);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            for (int i = 0; i < currentCurve.ControlPoints.Count; i++)
            {
                currentCurve.ControlPoints[i]=utils.Translate(currentCurve.ControlPoints[i], Vector3.forward);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            for (int i = 0; i < currentCurve.ControlPoints.Count; i++)
            {
                currentCurve.ControlPoints[i]=utils.Translate(currentCurve.ControlPoints[i], Vector3.back);
            }
        }
    }
    private void OnShear()
    {

    }
}
