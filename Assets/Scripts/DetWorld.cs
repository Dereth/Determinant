using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetWorld : MonoBehaviour
{

    public static Mesh sphereMesh;

    private DetObject[] objects;

    void Start()
    {
        objects = DetObject.objects.ToArray();
    }

    void FixedUpdate()
    {

        float dt = Time.deltaTime;

        foreach(DetObject obj in objects)
        {
            obj.preTick();
            if (!obj.props.unstoppable)
            {
                obj.applyHalfGravity(dt);
            }
        }

        for (int i = 0; i < objects.Length; i++)
        {
            DetObject obj = objects[i];

            for (int j = i + 1; j < objects.Length; j++)
            {
                DetObject other = objects[j];
                if (obj.shouldCollide(other))
                {
                    DetCollision collision = obj.getCollision(other);
                    if (collision != null)
                    {
                        DetObject.resolveCollision(collision, dt);
                    }
                }
            }
        }

        foreach(DetObject obj in objects)
        {
            if (!obj.props.unstoppable)
            {
                obj.applyHalfGravity(dt);
                DetCollision collision = DetGround.Instance.getCollision(obj);
                if (collision != null)
                {
                    DetObject.resolveCollision(collision, dt);
                }
            }
            obj.tick(dt);
        }
    }
}
