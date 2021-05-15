using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformBezierUtils
{
    public static Vector3 RotateX(Vector3 point, float angle)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[1, 1] = Mathf.Cos(angle);
        mat[1, 2] = -Mathf.Sin(angle);
        mat[2, 1] = Mathf.Sin(angle);
        mat[2, 2] = Mathf.Cos(angle);

        mat[0, 0] = mat[0, 2] = mat[3, 3] = 1;

        point = mat.MultiplyPoint(point);

        return point;
    }

    public static Vector3 RotateY(Vector3 point, float angle)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[0, 0] = Mathf.Cos(angle);
        mat[0, 2] = Mathf.Sin(angle);
        mat[2, 0] = -Mathf.Sin(angle);
        mat[2, 2] = Mathf.Cos(angle);

        mat[1, 1] = mat[1, 2] = mat[3, 3] = 1;

        point = mat.MultiplyPoint(point);

        return point;
    }

    public static Vector3 RotateZ(Vector3 point, float angle)
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

    public static Vector3 Translate(Vector3 point, Vector3 dist)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[0, 0] = mat[1, 1] = mat[2, 2] = mat[3, 3] = 1;
        mat[0, 3] = dist.x;
        mat[1, 3] = dist.y;
        mat[2, 3] = dist.z;

        point = mat.MultiplyPoint(point);

        return point;
    }

    public static Vector3 Scale(Vector3 point, float scale)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[0, 0] = scale;
        mat[1, 1] = scale;
        mat[2, 2] = scale;
        mat[3, 3] = 1;

        point = mat.MultiplyPoint(point);

        return point;
    }

    public static Vector3 RotateXInvert(Vector3 point, float angle)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[1, 1] = Mathf.Cos(angle);
        mat[1, 2] = -Mathf.Sin(angle);
        mat[2, 1] = Mathf.Sin(angle);
        mat[2, 2] = Mathf.Cos(angle);

        mat[0,0] = mat[0, 2] = mat[3, 3] = 1;

        point = mat.inverse.MultiplyPoint(point);

        return point;
    }

    public static Vector3 RotateYInvert(Vector3 point, float angle)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[0, 0] = Mathf.Cos(angle);
        mat[0, 2] = Mathf.Sin(angle);
        mat[2, 0] = -Mathf.Sin(angle);
        mat[2, 2] = Mathf.Cos(angle);

        mat[1, 1] = mat[1, 2] = mat[3, 3] = 1;

        point = mat.inverse.MultiplyPoint(point);

        return point;
    }

    public static Vector3 RotateZInvert(Vector3 point, float angle)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[0, 0] = Mathf.Cos(angle);
        mat[0, 1] = -Mathf.Sin(angle);
        mat[1, 0] = Mathf.Sin(angle);
        mat[1, 1] = Mathf.Cos(angle);

        mat[2, 2] = mat[3, 3] = 1;

        point = mat.inverse.MultiplyPoint(point);

        return point;
    }

    public static Vector3 TranslateInvert(Vector3 point, Vector3 dist)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[0, 0] = mat[1, 1] = mat[2, 2] = mat[3, 3] = 1;
        mat[0, 3] = dist.x;
        mat[1, 3] = dist.y;
        mat[2, 3] = dist.z;

        point = mat.inverse.MultiplyPoint(point);

        return point;
    }

    public static Vector3 ScaleInvert(Vector3 point, float scale)
    {
        Matrix4x4 mat = new Matrix4x4();

        mat[0, 0] = scale;
        mat[1, 1] = scale;
        mat[2, 2] = scale;
        mat[3, 3] = 1;

        point = mat.inverse.MultiplyPoint(point);

        return point;
    }

    public static Vector3 Shear(Vector3 point, int matrixCase, float percent)
    {
        Matrix4x4 mat = new Matrix4x4();
        mat[0, 0] = mat[1, 1] = mat[2, 2] = mat[3, 3] = 1;
        if (matrixCase == 0) mat[0, 1] = percent;
        if (matrixCase == 1) mat[0, 2] = percent;
        if (matrixCase == 2) mat[1, 0] = percent;
        if (matrixCase == 3) mat[1, 2] = percent;
        if (matrixCase == 4) mat[2, 0] = percent;
        if (matrixCase == 5) mat[2, 1] = percent;

        point = mat.MultiplyPoint(point);

        return point;
    }

    public static Vector3 ShearInvert(Vector3 point, int matrixCase, float percent)
    {
        Matrix4x4 mat = new Matrix4x4();
        mat[0, 0] = mat[1, 1] = mat[2, 2] = mat[3, 3] = 1;
        if (matrixCase == 0) mat[0, 1] = percent;
        if (matrixCase == 1) mat[0, 2] = percent;
        if (matrixCase == 2) mat[1, 0] = percent;
        if (matrixCase == 3) mat[1, 2] = percent;
        if (matrixCase == 4) mat[2, 0] = percent;
        if (matrixCase == 5) mat[2, 1] = percent;

        point = mat.inverse.MultiplyPoint(point);

        return point;
    }
}
