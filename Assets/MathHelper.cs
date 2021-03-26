using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MathHelper
{
    public static Vector2 RotatePoint(Vector2 point, float angleInDegrees, Vector2 pivot = default)
    {
        var angle = angleInDegrees * Mathf.Deg2Rad;
        var cos = Mathf.Cos(angle);
        var sin = Mathf.Sin(angle);

        // Translate point back to origin
        point -= pivot;

        // Rotate point
        var rotatedPoint = new Vector2(
            (point.x * cos) - (point.y * sin),
            (point.x * sin) + (point.y * cos)
        );

        // Translate point back
        point = rotatedPoint + pivot;

        return point;
    }

    public static float2 RotatePoint(float2 point, float angleInDegrees, float2 pivot = default)
    {
        var angle = angleInDegrees * Mathf.Deg2Rad;
        var cos = math.cos(angle);
        var sin = math.sin(angle);

        // Translate point back to origin
        point -= pivot;

        // Rotate point
        var rotatedPoint = new float2(
            (point.x * cos) - (point.y * sin),
            (point.x * sin) + (point.y * cos)
        );

        // Translate point back
        point = rotatedPoint + pivot;

        return point;
    }
}
