using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIHelper
{

    /*
     * Base getters and setters
     */

    // Mass

    public static string getMass(DetObject obj)
    {
        return obj.props.mass.ToString();
    }

    public static void setMass(DetObject obj, string mass)
    {
        float parsed = float.Parse(mass);
        obj.modifyMass(parsed);
    }

    // Unstoppable

    public static bool getUnstoppable(DetObject obj)
    {
        return obj.props.unstoppable;
    }

    public static void setUnstoppable(DetObject obj, bool unstoppable)
    {
        obj.modifyUnstoppable(unstoppable);
    }

    // Material

    public static int getMaterial(DetObject obj)
    {
        return obj.props.material;
    }

    public static void setMaterial(DetObject obj, int material)
    {
        obj.modifyMaterial(material);
    }

    /*
     * Position getters and setters
     */

    // X Position

    public static string getXPosition(DetObject obj)
    {
        return obj.pos.x.ToString();
    }

    public static void setXPosition(DetObject obj, string x)
    {
        float parsed = float.Parse(x);
        Vector3 pos = new Vector3(parsed, obj.props.position.y, obj.props.position.z);
        obj.modifyPosition(pos);
    }

    // Y Position

    public static string getYPosition(DetObject obj)
    {
        return obj.pos.y.ToString();
    }

    public static void setYPosition(DetObject obj, string y)
    {
        float parsed = float.Parse(y);
        Vector3 pos = new Vector3(obj.props.position.x, parsed, obj.props.position.z);
        obj.modifyPosition(pos);
    }

    // Z Position

    public static string getZPosition(DetObject obj)
    {
        return obj.pos.z.ToString();
    }

    public static void setZPosition(DetObject obj, string z)
    {
        float parsed = float.Parse(z);
        Vector3 pos = new Vector3(obj.props.position.x, obj.props.position.y, parsed);
        obj.modifyPosition(pos);
    }


    /*
     * Velocity getters and setters
     */

    // X Velocity

    public static string getXVelocity(DetObject obj)
    {
        return obj.vel.x.ToString();
    }

    public static void setXVelocity(DetObject obj, string x)
    {
        float parsed = float.Parse(x);
        Vector3 vel = new Vector3(parsed, obj.props.velocity.y, obj.props.velocity.z);
        obj.modifyVelocity(vel);
    }

    // Y Velocity

    public static string getYVelocity(DetObject obj)
    {
        return obj.vel.y.ToString();
    }

    public static void setYVelocity(DetObject obj, string y)
    {
        float parsed = float.Parse(y);
        Vector3 vel = new Vector3(obj.props.velocity.x, parsed, obj.props.velocity.z);
        obj.modifyVelocity(vel);
    }

    // Z Velocity

    public static string getZVelocity(DetObject obj)
    {
        return obj.vel.z.ToString();
    }

    public static void setZVelocity(DetObject obj, string z)
    {
        float parsed = float.Parse(z);
        Vector3 vel = new Vector3(obj.props.velocity.x, obj.props.velocity.y, parsed);
        obj.modifyVelocity(vel);
    }


    /*
     * Rotation getters and setters
     */

    // X Rotation

    public static string getXRotation(DetObject obj)
    {
        return (obj.rot.eulerAngles.x % 360).ToString();
    }

    public static void setXRotation(DetObject obj, string x)
    {
        float parsed = float.Parse(x);
        Vector3 rot = new Vector3(parsed, obj.props.rotation.y, obj.props.rotation.z);
        obj.modifyRotation(rot);
    }

    // Y Rotation

    public static string getYRotation(DetObject obj)
    {
        return (obj.rot.eulerAngles.y % 360).ToString();
    }

    public static void setYRotation(DetObject obj, string y)
    {
        float parsed = float.Parse(y);
        Vector3 rot = new Vector3(obj.props.rotation.x, parsed, obj.props.rotation.z);
        obj.modifyRotation(rot);
    }

    // Z Rotation

    public static string getZRotation(DetObject obj)
    {
        return (obj.rot.eulerAngles.z % 360).ToString();
    }

    public static void setZRotation(DetObject obj, string z)
    {
        float parsed = float.Parse(z);
        Vector3 rot = new Vector3(obj.props.rotation.x, obj.props.rotation.y, parsed);
        obj.modifyRotation(rot);
    }


    /*
     * Angular Velocity getters and setters
     */

    // X Angular Velocity

    public static string getXAngularVelocity(DetObject obj)
    {
        return obj.ang.x.ToString();
    }

    public static void setXAngularVelocity(DetObject obj, string x)
    {
        float parsed = float.Parse(x);
        Vector3 ang = new Vector3(parsed, obj.props.angleVel.y, obj.props.angleVel.z);
        obj.modifyAngleVel(ang);
    }

    // Y Angular Velocity

    public static string getYAngularVelocity(DetObject obj)
    {
        return obj.ang.y.ToString();
    }

    public static void setYAngularVelocity(DetObject obj, string y)
    {
        float parsed = float.Parse(y);
        Vector3 ang = new Vector3(obj.props.angleVel.x, parsed, obj.props.angleVel.z);
        obj.modifyAngleVel(ang);
    }

    // Z Angular Velocity

    public static string getZAngularVelocity(DetObject obj)
    {
        return obj.ang.z.ToString();
    }

    public static void setZAngularVelocity(DetObject obj, string z)
    {
        float parsed = float.Parse(z);
        Vector3 ang = new Vector3(obj.props.angleVel.x, obj.props.angleVel.y, parsed);
        obj.modifyAngleVel(ang);
    }


    /*
     * Sphere getters and setters
     */

    // Radius

    public static string getRadius(DetObject obj)
    {
        if (obj is DetSphere)
        {
            DetSphere sphere = (DetSphere)obj;
            return sphere.r.ToString();
        }
        else
        {
            throw new Exception("Object is not a sphere!");
        }
    }

    public static void setRadius(DetObject obj, string radius)
    {
        if (obj is DetSphere)
        {
            DetSphere sphere = (DetSphere)obj;
            float parsed = float.Parse(radius);
            sphere.modifyRadius(parsed);
        }
        else
        {
            throw new Exception("Object is not a sphere!");
        }
    }


    /*
     * Rectangular Prism getters and setters
     */

    // Length

    public static string getLength(DetObject obj)
    {
        if (obj is DetRect)
        {
            DetRect rect = (DetRect)obj;
            return ((DetRectProps)rect.props).length.ToString();
        }
        else
        {
            throw new Exception("Object is not a Rectangular Prism!");
        }
    }

    public static void setLength(DetObject obj, string length)
    {
        if (obj is DetRect)
        {
            DetRect rect = (DetRect)obj;
            float parsed = float.Parse(length);
            rect.modifyLength(parsed);
        }
        else
        {
            throw new Exception("Object is not a Rectangular Prism!");
        }
    }

    // Width

    public static string getWidth(DetObject obj)
    {
        if (obj is DetRect)
        {
            DetRect rect = (DetRect)obj;
            return ((DetRectProps)rect.props).width.ToString();
        }
        else
        {
            throw new Exception("Object is not a Rectangular Prism!");
        }
    }

    public static void setWidth(DetObject obj, string width)
    {
        if (obj is DetRect)
        {
            DetRect rect = (DetRect)obj;
            float parsed = float.Parse(width);
            rect.modifyWidth(parsed);
        }
        else
        {
            throw new Exception("Object is not a Rectangular Prism!");
        }
    }

    // Height

    public static string getHeight(DetObject obj)
    {
        if (obj is DetRect)
        {
            DetRect rect = (DetRect)obj;
            return ((DetRectProps)rect.props).height.ToString();
        }
        else
        {
            throw new Exception("Object is not a Rectangular Prism!");
        }
    }

    public static void setHeight(DetObject obj, string height)
    {
        if (obj is DetRect)
        {
            DetRect rect = (DetRect)obj;
            float parsed = float.Parse(height);
            rect.modifyHeight(parsed);
        }
        else
        {
            throw new Exception("Object is not a Rectangular Prism!");
        }
    }


    /*
     * Static getters and setters
     */

    // Gravity

    public static string getGravity()
    {
        return Determinant.gravity.ToString();
    }

    public static void setGravity(string gravity)
    {
        float parsed = float.Parse(gravity);
        if (parsed < 0)
        {
            throw new Exception("Gravity cannot be less than 0!");
        }

        Determinant.gravity = parsed;
    }

    // Ground Material

    public static int getGroundMaterial()
    {
        return getMaterial(Determinant.ground);
    }

    public static void setGroundMaterial(int material)
    {
        setMaterial(Determinant.ground, material);
    }

    // Right Hand Material

    public static int getRightHandMaterial()
    {
        return getMaterial(Determinant.rightHand);
    }

    public static void setRightHandMaterial(int material)
    {
        setMaterial(Determinant.rightHand, material);
    }

    // Left Hand Material

    public static int getLeftHandMaterial()
    {
        return getMaterial(Determinant.leftHand);
    }

    public static void setLefthandMaterial(int material)
    {
        setMaterial(Determinant.leftHand, material);
    }


    /*
     * Coefficient getters and setters
     */

    // 1 on 1 Friction

    public static string get1on1Friction()
    {
        return DetProps.fCoefs[1, 1].ToString();
    }

    public static void set1on1Friction(string friction)
    {
        float parsed = float.Parse(friction);
        if (parsed < 0)
        {
            throw new Exception("Friction cannot be less than 0!");
        }

        DetProps.fCoefs[1, 1] = parsed;
    }

    // 1 on 2 Friction

    public static string get1on2Friction()
    {
        return DetProps.fCoefs[1, 2].ToString();
    }

    public static void set1on2Friction(string friction)
    {
        float parsed = float.Parse(friction);
        if (parsed < 0)
        {
            throw new Exception("Friction cannot be less than 0!");
        }

        DetProps.fCoefs[1, 2] = parsed;
        DetProps.fCoefs[2, 1] = parsed;
    }

    // 1 on 3 Friction

    public static string get1on3Friction()
    {
        return DetProps.fCoefs[1, 3].ToString();
    }

    public static void set1on3Friction(string friction)
    {
        float parsed = float.Parse(friction);
        if (parsed < 0)
        {
            throw new Exception("Friction cannot be less than 0!");
        }

        DetProps.fCoefs[1, 3] = parsed;
        DetProps.fCoefs[3, 1] = parsed;
    }

    // 2 on 2 Friction

    public static string get2on2Friction()
    {
        return DetProps.fCoefs[2, 2].ToString();
    }

    public static void set2on2Friction(string friction)
    {
        float parsed = float.Parse(friction);
        if (parsed < 0)
        {
            throw new Exception("Friction cannot be less than 0!");
        }

        DetProps.fCoefs[2, 2] = parsed;
    }

    // 2 on 3 Friction

    public static string get2on3Friction()
    {
        return DetProps.fCoefs[1, 2].ToString();
    }

    public static void set2on3Friction(string friction)
    {
        float parsed = float.Parse(friction);
        if (parsed < 0)
        {
            throw new Exception("Friction cannot be less than 0!");
        }

        DetProps.fCoefs[2, 3] = parsed;
        DetProps.fCoefs[3, 2] = parsed;
    }

    // 3 on 3 Friction

    public static string get3on3Friction()
    {
        return DetProps.fCoefs[3, 3].ToString();
    }

    public static void set3on3Friction(string friction)
    {
        float parsed = float.Parse(friction);
        if (parsed < 0)
        {
            throw new Exception("Friction cannot be less than 0!");
        }

        DetProps.fCoefs[3, 3] = parsed;
    }

    // 1 on 1 Restitution

    public static string get1on1Restitution()
    {
        return DetProps.rCoefs[1, 1].ToString();
    }

    public static void set1on1Restitution(string restitution)
    {
        float parsed = float.Parse(restitution);
        if (parsed < 0)
        {
            throw new Exception("Restitution cannot be less than 0!");
        }
        else if (parsed > 1)
        {
            throw new Exception("Restitution cannot be more than 1!");
        }

        DetProps.rCoefs[1, 1] = parsed;
    }

    // 1 on 2 Restitution

    public static string get1on2Restitution()
    {
        return DetProps.rCoefs[1, 2].ToString();
    }

    public static void set1on2Restitution(string restitution)
    {
        float parsed = float.Parse(restitution);
        if (parsed < 0)
        {
            throw new Exception("Restitution cannot be less than 0!");
        }
        else if (parsed > 1)
        {
            throw new Exception("Restitution cannot be more than 1!");
        }

        DetProps.rCoefs[1, 2] = parsed;
        DetProps.rCoefs[2, 1] = parsed;
    }

    // 1 on 3 Restitution

    public static string get1on3Restitution()
    {
        return DetProps.rCoefs[1, 3].ToString();
    }

    public static void set1on3Restitution(string restitution)
    {
        float parsed = float.Parse(restitution);
        if (parsed < 0)
        {
            throw new Exception("Restitution cannot be less than 0!");
        }
        else if (parsed > 1)
        {
            throw new Exception("Restitution cannot be more than 1!");
        }

        DetProps.rCoefs[1, 3] = parsed;
        DetProps.rCoefs[3, 1] = parsed;
    }

    // 2 on 2 Restitution

    public static string get2on2Restitution()
    {
        return DetProps.rCoefs[2, 2].ToString();
    }

    public static void set2on2Restitution(string restitution)
    {
        float parsed = float.Parse(restitution);
        if (parsed < 0)
        {
            throw new Exception("Restitution cannot be less than 0!");
        }
        else if (parsed > 1)
        {
            throw new Exception("Restitution cannot be more than 1!");
        }

        DetProps.rCoefs[2, 2] = parsed;
    }

    // 2 on 3 Restitution

    public static string get2on3Restitution()
    {
        return DetProps.rCoefs[1, 2].ToString();
    }

    public static void set2on3Restitution(string restitution)
    {
        float parsed = float.Parse(restitution);
        if (parsed < 0)
        {
            throw new Exception("Restitution cannot be less than 0!");
        }
        else if (parsed > 1)
        {
            throw new Exception("Restitution cannot be more than 1!");
        }

        DetProps.rCoefs[2, 3] = parsed;
        DetProps.rCoefs[3, 2] = parsed;
    }

    // 3 on 3 Restitution

    public static string get3on3Restitution()
    {
        return DetProps.rCoefs[3, 3].ToString();
    }

    public static void set3on3Restitution(string restitution)
    {
        float parsed = float.Parse(restitution);
        if (parsed < 0)
        {
            throw new Exception("Restitution cannot be less than 0!");
        }
        else if (parsed > 1)
        {
            throw new Exception("Restitution cannot be more than 1!");
        }

        DetProps.rCoefs[3, 3] = parsed;
    }

}