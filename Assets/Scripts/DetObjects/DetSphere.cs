using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DetSphere : DetObjectHoldable
{

    public float r;
    private float momentOfInertia;

    public DetSphere(DetSphereProps props) : base(props)
    {
        r = props.radius;

        setScale();
        gameObj.AddComponent<MeshFilter>().mesh = Determinant.sphereMesh;
    }

    public void modifyRadius(float radius)
    {
        if (radius <= 0)
        {
            throw new Exception("Radius must be greater than 0");
        }
        DetSphereProps sphereProps = (DetSphereProps) props;
        sphereProps.radius = radius;
        r = sphereProps.radius;
        setScale();
        momentOfInertia = props.mass * r * r * 2 / 5;
    }

    public void setScale()
    {
        float scale = r * 2;
        gameObj.transform.localScale = new Vector3(scale, scale, scale);
    }

    public override void resetValues()
    {
        DetSphereProps sphereProps = (DetSphereProps) props;
        r = sphereProps.radius;
        momentOfInertia = props.mass * r * r * 2 / 5;
        base.resetValues();
    }

    public override void preTick()
    {
        base.preTick();
    }

    public override void tick(float dt)
    {
        base.tick(dt);
    }

    public override float getMoI(Vector3 vel)
    {
        return vel.sqrMagnitude > 0 ? momentOfInertia : 0;
    }

    public override float getCollisionRadius()
    {
        return r;
    }

    public override DetCollision getCollision(DetObject obj)
    {
        if (obj is DetRect)
        {
            DetRect rect = (DetRect) obj;
            Vector3 disp = rect.alignVect(pos - rect.pos);

            Vector3 dist = new Vector3(0, 0, 0);
            
            // In case sphere center is inside rectangle
            float bestDepth = float.MaxValue;
            int bestInvert = 0;
            int bestAxis = -1;

            for (int axis = 0; axis < 3; axis++)
            {
                int invert = disp[axis] < 0 ? -1 : 1;
                float d = disp[axis] * invert;
                float edge = rect.getDimension(axis) / 2;

                if (d > edge)
                {
                    dist[axis] = -invert * (d - edge);
                }
                else
                {
                    float curDepth = edge - d + r;
                    if (curDepth < bestDepth)
                    {
                        bestDepth = curDepth;
                        bestInvert = invert;
                        bestAxis = axis;
                    }
                }
            }

            float dMag = dist.magnitude;
            float depth;
            Vector3 direction;
            Vector3 point1;
            Vector3 point2;

            if (dMag == 0)
            {
                depth = bestDepth;
                direction = new Vector3(0, 0, 0);
                direction[bestAxis] = bestInvert;
                direction = rect.disalignVect(direction);
                point1 = new Vector3(0, 0, 0);
                point1[bestAxis] = -r * bestInvert;
                point1 = rect.disalignVect(point1);
                point2 = dist;
                point2[bestAxis] = bestInvert * rect.getDimension(bestAxis) / 2;
                point2 = rect.disalignVect(point2);
            }
            else if (dMag > r)
            {
                return null;
            }
            else
            {
                depth = r - dMag;
                direction = rect.disalignVect(-dist.normalized);
                point1 = -r * direction;
                point2 = rect.disalignVect(disp + dist);

            }

            return new DetCollision(depth, this, rect, point1, point2, direction);
            

        }
        else if (obj is DetSphere)
        {
            DetSphere sphere = (DetSphere) obj;
            Vector3 disp = pos - sphere.pos;
            float diff = disp.magnitude;

            if (diff <= (r + sphere.r))
            {
                float depth;
                Vector3 direction;
                Vector3 point1;
                Vector3 point2;
                
                if (diff == 0)
                {
                    // Default to downward collision
                    depth = (r + sphere.r) / 2;
                    direction = new Vector3(0, -1, 0);
                    point1 = new Vector3(0, -r, 0);
                    point2 = new Vector3(0, sphere.r, 0);
                }
                else
                {
                    depth = r + sphere.r - diff;
                    direction = disp / diff;
                    point1 = -direction * r;
                    point2 = direction * sphere.r;
                }

                return new DetCollision(depth, this, sphere, point1, point2, direction);
            }
        }

        return null;
    }

}

public class DetSphereProps : DetProps
{
    private static int index = 0;
    public float radius;

    public DetSphereProps() : base()
    {
        radius = 1;
    }

    public override DetObject createObject()
    {
        return new DetSphere(this);
    }

    public override string createName()
    {
        string name = "Sphere" + index;
        index++;
        return name;
    }

}