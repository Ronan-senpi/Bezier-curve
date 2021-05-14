using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformBezierUtils : MonoBehaviour
{
    Vector3 RotateX(Vector3 point, float angle)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[1, 1] = Mathf.Cos(angle);
        mat[1, 2] = -Mathf.Sin(angle);
        mat[2, 1] = Mathf.Sin(angle);
        mat[2, 2] = Mathf.Cos(angle);

        mat[0, 2] = mat[3, 3] = 1;

        point = mat.MultiplyPoint(point);

        return point;
    }

    Vector3 RotateY(Vector3 point, float angle)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[0, 0] = Mathf.Cos(angle);
        mat[0, 2] = Mathf.Sin(angle);
        mat[2, 0] = -Mathf.Sin(angle);
        mat[2, 2] = Mathf.Cos(angle);

        mat[1, 2] = mat[3, 3] = 1;

        point = mat.MultiplyPoint(point);

        return point;
    }

    Vector3 RotateZ(Vector3 point, float angle)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[0, 0] = Mathf.Cos(angle);
        mat[0, 1] = -Mathf.Sin(angle);
        mat[1, 0] = Mathf.Sin(angle);
        mat[1, 1] = Mathf.Cos(angle);

        mat[2, 2] = mat[3, 3] = 1; 

        point = mat.MultiplyPoint(point);

        return point;
    }

    Vector3 Translate(Vector3 point, Vector3 dist)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[0, 0] = mat[1, 1] = mat[2, 2] = mat[3, 3] = 1;
        mat[2, 0] = dist.x;
        mat[2, 1] = dist.y;
        mat[2, 2] = dist.z;

        point = mat.MultiplyPoint(point);

        return point;
    }

    Vector3 Scale(Vector3 point, float scale)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[0, 0] = scale;
        mat[1, 1] = scale;
        mat[2, 2] = scale;
        mat[3, 3] = 1;

        point = mat.MultiplyPoint(point);

        return point;
    }
}
