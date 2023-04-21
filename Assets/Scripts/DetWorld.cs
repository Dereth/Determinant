using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetWorld : MonoBehaviour
{

    public DetObjectHoldable[] objects;

    void Start()
    {
        objects = DetObject.objects.ToArray();
    }

    void FixedUpdate()
    {

        DetCollision collision;
        float dt = Time.deltaTime;

        foreach(DetObject obj in objects)
        {
            obj.preTick();
            if (!obj.isUnstoppable())
            {
                obj.applyHalfGravity(dt);
            }
        }

        Determinant.rightHand.updateHolding(dt);
        Determinant.leftHand.updateHolding(dt);

        for (int i = 0; i < objects.Length; i++)
        {
            DetObject obj = objects[i];

            for (int j = i + 1; j < objects.Length; j++)
            {
                DetObject other = objects[j];
                if (obj.shouldCollide(other))
                {
                    collision = obj.getCollision(other);
                    if (collision != null)
                    {
                        DetObject.resolveCollision(collision, dt);
                    }
                }
            }
        }

        Determinant.leftHand.tick(dt);
        Determinant.rightHand.tick(dt);
        foreach (DetObjectHoldable obj in objects)
        {
            if (!obj.isUnstoppable())
            {
                // Right Hand Collision
                if (Determinant.rightHand.holding)
                {
                    collision = Determinant.rightHand.getCollision(obj);
                    if (collision != null)
                    {
                        DetObject.resolveCollision(collision, dt);
                    }
                }

                // Left Hand Collision
                if (Determinant.leftHand.holding)
                {
                    collision = Determinant.leftHand.getCollision(obj);
                    if (collision != null)
                    {
                        DetObject.resolveCollision(collision, dt);
                    }
                }

                // Final Gravity Application
                obj.applyHalfGravity(dt);

                // Ground Collision
                collision = Determinant.ground.getCollision(obj);
                if (collision != null)
                {
                    DetObject.resolveCollision(collision, dt);
                }
            }
            obj.tick(dt);
        }
    }
}
