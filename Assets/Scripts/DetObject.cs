using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class DetObject
{

    // Constants
    public const int MAX_OBJECTS = 10;
    public const int jitterMax = 5;

    // Static List
    public static List<DetObjectHoldable> objects = new List<DetObjectHoldable>();
    public static int numObjects = 0;

    // Properties
    public DetProps props { get; private set; }

    // Runtime Values
    public float moi { get; set; }
    public Vector3 vel { get; set; }
    public Vector3 pos { get; set; }
	public Vector3 ang { get; set; }
    public Quaternion rot { get; set; }
    public Vector3 momentum { get; set; }
    public Vector3 angleMom { get; set; }

    // Kinematic Approximations
    public Vector3 prevVel;
    public float maxHeight;
    public float kE;
    public float rE;
    public float pE;

    // Displace object at end of each tick
    public Vector3 collDisp { get; private set; }

    // Anti-jittering
    public int jitterCount = 0;

    // Trig Optimizations
    public Quaternion inverseRot { get; private set; }
    private float xsin;
    private float ysin;
    private float zsin;
    private float xcos;
    private float ycos;
    private float zcos;

    // Controls adding objects to list
    public static void addObject(DetObjectHoldable obj)
    {
        if (numObjects < MAX_OBJECTS)
        {
            numObjects++;
            objects.Add(obj);
        }
    }

    public static void removeObject(DetObject obj)
    {
        if (objects.Contains(obj))
        {
            numObjects--;
            objects.Remove(obj);
        }
    }

    // Create object with properties
    public DetObject(DetProps props)
    {
        this.props = props;
        if (this is DetObjectHoldable)
        {
            addObject((DetObjectHoldable) this);
        }
    }

    /*
     * Modification Functions:
     * 
     * Changes properties of object. DO NOT USE WHILE RUNNING!
     */

    public void modifyMaterial(int material)
    {
        props.material = material;
        if (this is DetObjectRendered)
        {
            ((DetObjectRendered) this).gameObj.GetComponent<Renderer>().material = props.getRenderMaterial();
        }
    }

    public void modifyMass(float mass)
    {
        if (mass <= 0)
        {
            throw new Exception("mass must be greater than 0");
        }
        props.mass = mass;
        momentum = mass * this.vel;
        moi = getMoI(ang);
        angleMom = moi * ang;
    }

    public void modifyUnstoppable(bool unstoppable)
    {
        props.unstoppable = unstoppable;
    }

    public void modifyPosition(Vector3 position)
    {
        props.position = position;
        pos = position;
        updateEnergies();
    }

    public void modifyVelocity(Vector3 velocity)
    {
        props.velocity = velocity;
        vel = props.velocity;
        momentum = props.mass * props.velocity;
        updateEnergies();
    }

    public void modifyRotation(Vector3 rotation)
    {
        props.rotation = rotation;
        rot = Quaternion.Euler(rotation);
        cacheTrig();
        moi = getMoI(ang);
        angleMom = moi * ang;
    }

    public void modifyAngleVel(Vector3 angleVel)
    {
        props.angleVel = angleVel * DetMath.DEG_TO_RAD;
        ang = props.angleVel;
        moi = getMoI(ang);
        angleMom = moi * ang;
    }

    /*
     * Setter Functions:
     * 
     * Can be used while running. Does not affect properties
     */

    public void setVel(Vector3 velocity)
    {
        vel = velocity;
        momentum = props.mass * velocity;
        updateEnergies();
    }

    public void setAng(Vector3 angleVel)
    {
        ang = angleVel;
        cacheTrig();
        moi = getMoI(ang);
        angleMom = moi * ang;
    }

    /*
     * Trig Functions
     */

    public void cacheTrig()
    {
        inverseRot = Quaternion.Inverse(rot);
    }

    public Vector3 alignVect(Vector3 vect)
    {
        return inverseRot * vect;
    }

    public Vector3 disalignVect(Vector3 vect)
    {
        return rot * vect;
    }

    public virtual void resetValues()
    {
        jitterCount = 0;
        vel = props.velocity;
        pos = props.position;
        ang = props.angleVel;
        rot = Quaternion.Euler(props.rotation);
        cacheTrig();
        moi = getMoI(ang);
        momentum = props.velocity * props.mass;
        angleMom = props.angleVel * moi;
        updateEnergies();
    }

    public virtual void preTick()
    {
        prevVel = vel;
        collDisp = new Vector3(0, 0, 0);
        cacheTrig();
    }

    public virtual void tick(float dt)
    {
        pos += vel * dt + collDisp;

        float angle = ang.magnitude * dt;
        if (angle > 0)
        {
            rot = Quaternion.AngleAxis(angle * DetMath.RAD_TO_DEG, ang.normalized) * rot;
        }

        float curKE = getKEnergy();
        float curPE = getPEnergy();
        if (!isUnstoppable() && curKE + curPE > kE + pE)
        {
            Vector3 newPos = pos;
            newPos.y = Mathf.Min(pos.y, maxHeight);
            pos = newPos;
            curPE = getPEnergy();
            float eDiff = pE + kE - curPE - curKE;
            vel = new Vector3(vel.x, Mathf.Sqrt(Mathf.Max(0, vel.y * vel.y + 2 * eDiff / props.mass)), vel.z);
            momentum = vel * props.mass;
            curKE = getKEnergy();
            kE = curKE;
            pE = curPE;
        }
        else
        {
            kE = curKE;
            pE = curPE;
        }
    }

    public virtual bool isUnstoppable()
    {
        return props.unstoppable;
    }

    public float getKEnergy()
    {
        return 0.5F * props.mass * vel.sqrMagnitude;
    }

    public float getREnergy()
    {
        return 0.5F * moi * ang.sqrMagnitude;
    }

    public float getPEnergy()
    {
        return props.mass * pos.y * Determinant.gravity;
    }

    public float getMaxHeight()
    {
        if (Determinant.gravity == 0)
        {
            return float.MaxValue;
        }
        else
        {
            return (pE + kE) / (props.mass * Determinant.gravity);
        }
    }

    public Vector3 getVelAtPoint(Vector3 point)
    {
        return vel + Vector3.Cross(ang, point);
    }

    public void applyHalfGravity(float dt)
    {
        Vector3 force = new Vector3(0, -Determinant.gravity * dt * props.mass / 2, 0);
        momentum += force;
        vel = momentum / props.mass;
    }

    public void updateEnergies()
    {
        kE = getKEnergy();
        rE = getREnergy();
        pE = getPEnergy();
        maxHeight = getMaxHeight();
    }

    public bool canClick()
    {
        return jitterCount >= jitterMax;
    }

    public virtual bool tryClick(Vector3 dir, float dt)
    {
        return false;
    }

    public virtual void addForce(Vector3 force, Vector3 location, float dt, bool couldClick)
    {
        if (!props.unstoppable)
        {
            momentum += force;
            vel = momentum / props.mass;
            Vector3 angleMomDiff = Vector3.Cross(location, force);
            angleMom += angleMomDiff;
            moi = getMoI(angleMom);
            Vector3 oldAng = ang;
            ang = moi == 0 ? new Vector3(0, 0, 0) : angleMom / moi;

            float torqueMoi = getMoI(angleMomDiff);
            float angAcc = torqueMoi == 0 ? 0 : angleMomDiff.magnitude / torqueMoi;
            if (couldClick)
            {
                if (angAcc < 0.5F && ang.magnitude < 0.5F)
                {
                    if (!canClick())
                    {
                        jitterCount++;
                    }
                }
                else
                {
                    jitterCount = 0;
                }
            }

            updateEnergies();
        }
    }

    public static void resolveCollision(DetCollision coll, float dt)
    {
        DetObject mainObj = null;
        DetObject otherObj = null;
        Vector3 mainPoint = Vector3.zero;
        Vector3 mainDir = Vector3.zero;
        float mCoef; // Mass coefficient
        float rCoef; // Restitution coefficient
        float fCoef; // Friction coefficient;

        // Stores the "main" object (the one that is not unstoppable)
        // or keeps as null if both are not unstoppable
        if (coll.obj1.isUnstoppable())
        {
            mainObj = coll.obj2;
            mainPoint = coll.point2;
            mainDir = -coll.direction;
            otherObj = coll.obj1;
        }
        else if (coll.obj2.isUnstoppable())
        {
            mainObj = coll.obj1;
            mainPoint = coll.point1;
            mainDir = coll.direction;
            otherObj = coll.obj2;
        }

        // Get collision coefficients
        rCoef = coll.obj1.props.getRcoef(coll.obj2.props);
        fCoef = coll.obj1.props.getFcoef(coll.obj2.props);

        // Calculate relative velocity of the two points
        Vector3 RelVel = coll.obj1.getVelAtPoint(coll.point1)
            - coll.obj2.getVelAtPoint(coll.point2);

        // Get the value of the initail velocity (Will be negative if objects are moving towards each other)
        float iSpd = Vector3.Dot(RelVel, coll.direction);

        if (iSpd > 0)
        {
            iSpd = 0;
        }

        // Calculate final relative speed.
        float fSpd = -rCoef * iSpd;

        if (fSpd * dt < coll.depth)
        {
            // Separate objects based on mass
            if (mainObj != null)
            {
                mainObj.collDisp += coll.depth * mainDir;
            }
            else
            {
                float totalMass = coll.obj1.props.mass + coll.obj2.props.mass;
                coll.obj1.collDisp += coll.depth * (coll.obj2.props.mass / totalMass) * coll.direction;
                coll.obj2.collDisp -= coll.depth * (coll.obj1.props.mass / totalMass) * coll.direction;
            }

        }

        if (mainObj != null)
        {
            mCoef = mainObj.getCollMass(mainPoint, mainDir);
        }
        else
        {
            float m1 = coll.obj1.getCollMass(coll.point1, coll.direction);
            float m2 = coll.obj2.getCollMass(coll.point2, coll.direction);
            mCoef = (m1 * m2) / (m1 + m2);
        }

        float f = (fSpd - iSpd) * mCoef;

        Vector3 force = coll.direction * f;

        coll.obj1.addForce(force, coll.point1, dt, true);
        coll.obj2.addForce(-force, coll.point2, dt, true);

        RelVel = coll.obj1.getVelAtPoint(coll.point1)
            - coll.obj2.getVelAtPoint(coll.point2);

        //RelVel = RelVel - (coll.direction * Vector3.Dot(RelVel, coll.direction));
        Vector3 fricDirection = -(RelVel - (coll.direction * Vector3.Dot(RelVel, coll.direction))).normalized;
        iSpd = Vector3.Dot(RelVel, fricDirection);

        if (mainObj != null)
        {
            mCoef = mainObj.getCollMass(mainPoint, fricDirection);
        }
        else
        {
            float m1 = coll.obj1.getCollMass(coll.point1, fricDirection);
            float m2 = coll.obj2.getCollMass(coll.point2, fricDirection);
            mCoef = (m1 * m2) / (m1 + m2);
        }

        Debug.Log("iSpd " + iSpd);

        float maxFric = -iSpd * mCoef;
        f = Mathf.Min(f * fCoef, maxFric);

        force = fricDirection * f;

        Debug.Log("Force: " + force);

        coll.obj1.addForce(force, coll.point1, dt, false);
        coll.obj2.addForce(-force, coll.point2, dt, false);

        RelVel = coll.obj1.getVelAtPoint(coll.point1)
            - coll.obj2.getVelAtPoint(coll.point2);

        if (-iSpd < 0.1F || fCoef == 0)
        {
            bool clicked1 = coll.obj1.tryClick(coll.direction, dt);
            bool clicked2 = coll.obj2.tryClick(-coll.direction, dt);
            if (clicked1 || clicked2)
            {
                if (fCoef != 0)
                {
                    if (mainObj != null)
                    {
                        mainObj.vel = otherObj.vel;
                        mainObj.momentum = mainObj.vel * mainObj.props.mass;
                    }
                    else
                    {
                        coll.obj1.vel = coll.obj2.vel;
                        coll.obj1.momentum = coll.obj1.vel * coll.obj1.props.mass;
                    }
                }

            }
        }

    }

    public float getCollMass(Vector3 point, Vector3 direction)
    {
        float kMass = props.mass;
        float r = Vector3.Cross(point, direction).sqrMagnitude;

        if (r != 0)
        {
            float moi = getMoI(Vector3.Cross(point, direction));
            if (moi > 0)
            {
                float rMass = moi / r;
                return (kMass * rMass) / (kMass + rMass);
            }
        }
        return kMass;
    }

    public bool shouldCollide(DetObject obj)
    {
        if (isUnstoppable() && obj.isUnstoppable())
        {
            return false;
        }
        else if ((pos - obj.pos).sqrMagnitude > Mathf.Pow(getCollisionRadius() + obj.getCollisionRadius(), 2F))
        {
            return false;
        }

        return true;
    }

    public abstract float getMoI(Vector3 vel);

    public abstract float getCollisionRadius();

    public abstract DetCollision getCollision(DetObject obj);

}