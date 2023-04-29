using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetHand : DetObject
{

    public const float RADIUS = 0.12F;
    public static Material holdingMat = Resources.Load("Holding_Mat", typeof(Material)) as Material;
    public static Material notHoldingMat = Resources.Load("Not_Holding_Mat", typeof(Material)) as Material;

    public bool holding { get; private set; }
    public List<DetObjectHoldable> heldObjects;

    // Corresponding GameObject
    public GameObject gameObj { get; private set; }

    public DetHand(DetHandProps props) : base(props)
    {
        heldObjects = new List<DetObjectHoldable>();
        holding = false;

        // Create Object
        gameObj = new GameObject(props.createName());
        gameObj.AddComponent<MeshRenderer>().material = props.getRenderMaterial();
        gameObj.AddComponent<DetHandObject>().detObject = this;
        gameObj.transform.localScale = new Vector3(RADIUS * 2, RADIUS * 2, RADIUS * 2);
        gameObj.AddComponent<MeshFilter>().mesh = Determinant.sphereMesh;
        gameObj.GetComponent<Renderer>().material = notHoldingMat;
    }

    // Updates position and status of hands
    public void updateHolding(float dt)
    {
        bool rightHand = ((DetHandProps) props).isRightHand();
        bool readHolding = rightHand ? VRPositioning.rightHeld() : VRPositioning.leftHeld();
        Vector3 readPos = rightHand ? VRPositioning.rightPos() : VRPositioning.leftPos();
        Quaternion readRot = rightHand ? VRPositioning.rightRot() : VRPositioning.leftRot();

        // Grabs changes in position;
        Vector3 newVel = (readPos - pos) / dt;
        Quaternion deltaRot = (readRot * Quaternion.Inverse(rot));

        // Calculates angular momentum
        float magnitude = 0.0F;
        Vector3 axis = Vector3.zero;
        deltaRot.ToAngleAxis(out magnitude, out axis);
        magnitude /= dt;
        Vector3 newAng = axis.normalized * magnitude * DetMath.DEG_TO_RAD;

        // Sets values
        setVel(newVel);
        setAng(newAng);

        if (readHolding)
        {
            if (!holding)
            {
                gameObj.GetComponent<Renderer>().material = holdingMat;

                foreach (DetObject obj in DetObject.objects)
                {
                    if (obj is DetObjectHoldable && getCollision(obj) != null)
                    {
                        DetObjectHoldable heldObj = (DetObjectHoldable) obj;
                        heldObjects.Add(heldObj);
                        if (heldObj.hand != null)
                        {
                            heldObj.hand.heldObjects.Remove(heldObj);
                        }
                        heldObj.hand = this;
                    }
                }
                holding = true;
            }

            // Calculates and sets values for held objects
            foreach (DetObjectHoldable obj in heldObjects)
            {
                Vector3 dist = obj.pos - pos;
                Vector3 newPos = readPos + (deltaRot * dist);
                obj.setVel((newPos - obj.pos) / dt);
                obj.setAng(newAng);
            }

        }
        else if (holding)
        {
            clearHolding();
        }

        pos = readPos;
        rot = readRot;

    }

    public void updatePositioning()
    {
        bool rightHand = ((DetHandProps)props).isRightHand();
        pos = rightHand ? VRPositioning.rightPos() : VRPositioning.leftPos();
        rot = rightHand ? VRPositioning.rightRot() : VRPositioning.leftRot();
        clearHolding();
    }

    public void clearHolding()
    {
        // Sets material to not holding
        gameObj.GetComponent<Renderer>().material = notHoldingMat;

        // Clears Held Objects
        foreach (DetObjectHoldable obj in heldObjects)
        {
            obj.hand = null;
        }
        heldObjects.Clear();
        holding = false;
    }

    public override float getMoI(Vector3 vel)
    {
        return 0;
    }

    // Collision Logic
    public override float getCollisionRadius()
    {
        return RADIUS;
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
                    float curDepth = edge - d + RADIUS;
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
                point1[bestAxis] = -RADIUS * bestInvert;
                point1 = rect.disalignVect(point1);
                point2 = dist;
                point2[bestAxis] = bestInvert * rect.getDimension(bestAxis) / 2;
                point2 = rect.disalignVect(point2);
            }
            else if (dMag > RADIUS)
            {
                return null;
            }
            else
            {
                depth = RADIUS - dMag;
                direction = rect.disalignVect(-dist.normalized);
                point1 = -RADIUS * direction;
                point2 = rect.disalignVect(disp + dist);

            }

            return new DetCollision(depth, this, rect, point1, point2, direction);


        }
        else if (obj is DetSphere)
        {
            DetSphere sphere = (DetSphere)obj;
            Vector3 disp = pos - sphere.pos;
            float diff = disp.magnitude;

            if (diff <= (RADIUS + sphere.r))
            {
                float depth;
                Vector3 direction;
                Vector3 point1;
                Vector3 point2;

                if (diff == 0)
                {
                    // Default to downward collision
                    depth = (RADIUS + sphere.r) / 2;
                    direction = new Vector3(0, -1, 0);
                    point1 = new Vector3(0, -RADIUS, 0);
                    point2 = new Vector3(0, sphere.r, 0);
                }
                else
                {
                    depth = RADIUS + sphere.r - diff;
                    direction = disp / diff;
                    point1 = -direction * RADIUS;
                    point2 = direction * sphere.r;
                }

                return new DetCollision(depth, this, sphere, point1, point2, direction);
            }
        }

        return null;
    }
}

public abstract class DetHandProps : DetProps
{

    public DetHandProps() : base()
    {
        unstoppable = true;
    }

    public override DetObject createObject()
    {
        return new DetHand(this);
    }

    public abstract bool isRightHand();

}

public class DetRightHandProps : DetHandProps
{

    public DetRightHandProps() : base()
    {
    }

    public override bool isRightHand()
    {
        return true;
    }

    public override string createName()
    {
        return "RightHand";
    }

}

public class DetLeftHandProps : DetHandProps
{

    public DetLeftHandProps() : base()
    {
    }

    public override bool isRightHand()
    {
        return false;
    }

    public override string createName()
    {
        return "LeftHand";
    }

}