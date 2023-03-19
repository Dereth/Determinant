using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetCollision
{

    public float depth;
    public DetObject obj1;
    public DetObject obj2;
    public Vector3 point1;
    public Vector3 point2;
    public Vector3 direction;

    public DetCollision(float depth, DetObject obj1, DetObject obj2, Vector3 point1, Vector3 point2, Vector3 direction)
    {
        this.depth = depth;
        this.obj1 = obj1;
        this.obj2 = obj2;
        this.point1 = point1;
        this.point2 = point2;
        this.direction = direction;
    }

}