using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetMath
{
    public const int XAXIS = 0;
    public const int YAXIS = 1;
    public const int ZAXIS = 2;

    public const float DEG_TO_RAD = Mathf.PI / 180;
    public const float RAD_TO_DEG = 180 / Mathf.PI;
    public const float TWO_PI = 2 * Mathf.PI;

    public static Vector3 fixRotation(Vector3 rotation) {
        return new Vector3(
            (rotation.x + TWO_PI) % TWO_PI,
            (rotation.y + TWO_PI) % TWO_PI,
            (rotation.z + TWO_PI) % TWO_PI
        );
    }

    // Combines the mass of two objects to determine the combined mass
    public static float CombineMasses(float mass1, float mass2)
    {
        if (float.IsNaN(mass1))
        {
            if (float.IsNaN(mass2))
            {
                return float.NaN;
            }
            return mass2;
        }
        else if (float.IsNaN(mass2))
        {
            return mass1;
        }
        else
        {
            return (mass1 + mass2) / (mass1 * mass2);
        }
    }

}


public class Line
{
    public Vector3 point1;
    public Vector3 point2;

    public Line(Vector3 point1, Vector3 point2)
    {
        this.point1 = point1;
        this.point2 = point2;
    }
}