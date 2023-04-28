using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class NumberInput : MonoBehaviour
{

    public static TMPro.TMP_InputField selected = null;
    private static TouchScreenKeyboard keyboard;

    public TMPro.TMP_InputField input;
    public UnityEvent m_UpdateInput;

    void Update()
    {
        input.interactable = EventController.canEdit;
        if (selected != input)
        {
            m_UpdateInput.Invoke();
        }
    }

    public void select()
    {
        deselect();
        selected = input;
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        Debug.Log(keyboard.active);
    }

    public static void deselect()
    {
        if (selected != null)
        {
            selected.DeactivateInputField();
            selected = null;
        }
    }

    /*
     * 
     * 
     * 
     */


    /*
     * Position
     */

    // X Position

    public void setXPosition(string x)
    {
        try
        {
            float parsed = float.Parse(x);
            Vector3 pos = new Vector3(
                parsed,
                EventController.selected.props.position.y,
                EventController.selected.props.position.z
            );
            EventController.selected.modifyPosition(pos);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToXPosition()
    {
        input.text = EventController.selected.pos.x.ToString();
    }

    // Y Position

    public void setYPosition(string y)
    {
        try
        {
            float parsed = float.Parse(y);
            Vector3 pos = new Vector3(
                EventController.selected.props.position.x,
                parsed,
                EventController.selected.props.position.z
            );
            EventController.selected.modifyPosition(pos);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToYPosition()
    {
        input.text = EventController.selected.pos.y.ToString();
    }

    // Z Position

    public void setZPosition(string z)
    {
        try
        {
            float parsed = float.Parse(z);
            Vector3 pos = new Vector3(
                EventController.selected.props.position.x,
                EventController.selected.props.position.y,
                parsed
            );
            EventController.selected.modifyPosition(pos);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToZPosition()
    {
        input.text = EventController.selected.pos.z.ToString();
    }


    /*
     * Velocity
     */

    // X Velocity

    public void setXVelocity(string x)
    {
        try
        {
            float parsed = float.Parse(x);
            Vector3 vel = new Vector3(
                parsed,
                EventController.selected.props.velocity.y,
                EventController.selected.props.velocity.z
            );
            EventController.selected.modifyVelocity(vel);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToXVelocity()
    {
        input.text = EventController.selected.vel.x.ToString();
    }

    // Y Velocity

    public void setYVelocity(string y)
    {
        try
        {
            float parsed = float.Parse(y);
            Vector3 vel = new Vector3(
                EventController.selected.props.velocity.x,
                parsed,
                EventController.selected.props.velocity.z
            );
            EventController.selected.modifyVelocity(vel);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToYVelocity()
    {
        input.text = EventController.selected.vel.y.ToString();
    }

    // Z Velocity

    public void setZVelocity(string z)
    {
        try
        {
            float parsed = float.Parse(z);
            Vector3 vel = new Vector3(
                EventController.selected.props.velocity.x,
                EventController.selected.props.velocity.y,
                parsed
            );
            EventController.selected.modifyVelocity(vel);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToZVelocity()
    {
        input.text = EventController.selected.vel.z.ToString();
    }


    /*
     * Rotation
     */

    // X Rotation

    public void setXRotation(string x)
    {
        try
        {
            float parsed = float.Parse(x);
            Vector3 rot = new Vector3(
                parsed,
                EventController.selected.props.rotation.y,
                EventController.selected.props.rotation.z
            );
            EventController.selected.modifyRotation(rot);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToXRotation()
    {
        input.text = (EventController.selected.rot.eulerAngles.x % 360).ToString();
    }

    // Y Rotation

    public void setYRotation(string y)
    {
        try
        {
            float parsed = float.Parse(y);
            Vector3 rot = new Vector3(
                EventController.selected.props.rotation.x,
                parsed,
                EventController.selected.props.rotation.z
            );
            EventController.selected.modifyRotation(rot);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToYRotation()
    {
        input.text = (EventController.selected.rot.eulerAngles.y % 360).ToString();
    }

    // Z Rotation

    public void setZRotation(string z)
    {
        try
        {
            float parsed = float.Parse(z);
            Vector3 rot = new Vector3(
                EventController.selected.props.rotation.x,
                EventController.selected.props.rotation.y,
                parsed
            );
            EventController.selected.modifyRotation(rot);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToZRotation()
    {
        input.text = (EventController.selected.rot.eulerAngles.z % 360).ToString();
    }


    /*
     * Angular Velocity
     */

    // X Angular Velocity

    public void setXAngularVelocity(string x)
    {
        try
        {
            float parsed = float.Parse(x);
            Vector3 ang = new Vector3(
                parsed,
                EventController.selected.props.angleVel.y,
                EventController.selected.props.angleVel.z
            );
            EventController.selected.modifyAngleVel(ang);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToXAngularVelocity()
    {
        input.text = EventController.selected.ang.x.ToString();
    }

    // Y Angular Velocity

    public void setYAngularVelocity(string y)
    {
        try
        {
            float parsed = float.Parse(y);
            Vector3 ang = new Vector3(
                EventController.selected.props.angleVel.x,
                parsed,
                EventController.selected.props.angleVel.z
            );
            EventController.selected.modifyAngleVel(ang);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToYAngularVelocity()
    {
        input.text = EventController.selected.ang.y.ToString();
    }

    // Z Angular Velocity

    public void setZAngularVelocity(string z)
    {
        try
        {
            float parsed = float.Parse(z);
            Vector3 ang = new Vector3(
                EventController.selected.props.angleVel.x,
                EventController.selected.props.angleVel.y,
                parsed
            );
            EventController.selected.modifyAngleVel(ang);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToZAngularVelocity()
    {
        input.text = EventController.selected.ang.z.ToString();
    }


    /*
     * Other Values
     */

    // Mass

    public void setMass(string mass)
    {
        try
        {
            float parsed = float.Parse(mass);
            EventController.selected.modifyMass(parsed);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToMass()
    {
        input.text = EventController.selected.props.mass.ToString();
    }


    /*
     * Sphere
     */

    // Radius

    public void setRadius(string r)
    {
        if (EventController.selected is DetSphere)
        {
            DetSphere sphere = (DetSphere)EventController.selected;
            try
            {
                float parsed = float.Parse(r);
                sphere.modifyRadius(parsed);
            }
            catch (Exception)
            {
                EventController.Instance.displayError();
            }
        }
        m_UpdateInput.Invoke();
    }

    public void setToRadius()
    {
        if (EventController.selected is DetSphere)
        {
            DetSphere sphere = (DetSphere)EventController.selected;
            input.text = sphere.r.ToString();
        }
    }


    /*
     * Rectangular Prism
     */

    // Length

    public void setLength(string l)
    {
        if (EventController.selected is DetRect)
        {
            DetRect rect = (DetRect)EventController.selected;
            try
            {
                float parsed = float.Parse(l);
                rect.modifyLength(parsed);
            }
            catch (Exception)
            {
                EventController.Instance.displayError();
            }
        }
        m_UpdateInput.Invoke();
    }

    public void setToLength()
    {
        if (EventController.selected is DetRect)
        {
            DetRect rect = (DetRect)EventController.selected;
            input.text = ((DetRectProps)rect.props).length.ToString();
        }
    }

    // Width

    public void setWidth(string w)
    {
        if (EventController.selected is DetRect)
        {
            DetRect rect = (DetRect)EventController.selected;
            try
            {
                float parsed = float.Parse(w);
                rect.modifyWidth(parsed);
            }
            catch (Exception)
            {
                EventController.Instance.displayError();
            }
        }
        m_UpdateInput.Invoke();
    }

    public void setToWidth()
    {
        if (EventController.selected is DetRect)
        {
            DetRect rect = (DetRect)EventController.selected;
            input.text = ((DetRectProps)rect.props).width.ToString();
        }
    }

    // Height

    public void setHeight(string h)
    {
        if (EventController.selected is DetRect)
        {
            DetRect rect = (DetRect)EventController.selected;
            try
            {
                float parsed = float.Parse(h);
                rect.modifyHeight(parsed);
            }
            catch (Exception)
            {
                EventController.Instance.displayError();
            }
        }
        m_UpdateInput.Invoke();
    }

    public void setToHeight()
    {
        if (EventController.selected is DetRect)
        {
            DetRect rect = (DetRect)EventController.selected;
            input.text = ((DetRectProps)rect.props).height.ToString();
        }
    }


    /*
     * Static Values
     */

    // Gravity

    public void setGravity(string g)
    {
        try
        {
            float parsed = float.Parse(g);
            if (parsed < 0)
            {
                EventController.Instance.displayError();
            }
            else
            {
                Determinant.gravity = parsed;
            }
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToGravity()
    {
        input.text = Determinant.gravity.ToString();
    }

    // 1 on 1 Friction

    public void set1on1Friction(string f)
    {
        try
        {
            float parsed = float.Parse(f);
            if (parsed < 0)
            {
                EventController.Instance.displayError();
            }
            else
            {
                DetProps.fCoefs[0, 0] = parsed;
            }
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setTo1on1Friction()
    {
        input.text = DetProps.fCoefs[0, 0].ToString();
    }

    // 1 on 2 Friction

    public void set1on2Friction(string f)
    {
        try
        {
            float parsed = float.Parse(f);
            if (parsed < 0)
            {
                EventController.Instance.displayError();
            }
            else
            {
                DetProps.fCoefs[0, 1] = parsed;
                DetProps.fCoefs[1, 0] = parsed;
            }
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setTo1on2Friction()
    {
        input.text = DetProps.fCoefs[0, 1].ToString();
    }

    // 1 on 3 Friction

    public void set1on3Friction(string f)
    {
        try
        {
            float parsed = float.Parse(f);
            if (parsed < 0)
            {
                EventController.Instance.displayError();
            }
            else
            {
                DetProps.fCoefs[0, 2] = parsed;
                DetProps.fCoefs[2, 0] = parsed;
            }
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setTo1on3Friction()
    {
        input.text = DetProps.fCoefs[0, 2].ToString();
    }

    // 2 on 2 Friction

    public void set2on2Friction(string f)
    {
        try
        {
            float parsed = float.Parse(f);
            if (parsed < 0)
            {
                EventController.Instance.displayError();
            }
            else
            {
                DetProps.fCoefs[1, 1] = parsed;
            }
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setTo2on2Friction()
    {
        input.text = DetProps.fCoefs[1, 1].ToString();
    }

    // 2 on 3 Friction

    public void set2on3Friction(string f)
    {
        try
        {
            float parsed = float.Parse(f);
            if (parsed < 0)
            {
                EventController.Instance.displayError();
            }
            else
            {
                DetProps.fCoefs[1, 2] = parsed;
                DetProps.fCoefs[2, 1] = parsed;
            }
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setTo2on3Friction()
    {
        input.text = DetProps.fCoefs[1, 2].ToString();
    }

    // 3 on 3 Friction

    public void set3on3Friction(string f)
    {
        try
        {
            float parsed = float.Parse(f);
            if (parsed < 0)
            {
                EventController.Instance.displayError();
            }
            else
            {
                DetProps.fCoefs[2, 2] = parsed;
            }
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setTo3on3Friction()
    {
        input.text = DetProps.fCoefs[2, 2].ToString();
    }

    // 1 on 1 Restitution

    public void set1on1Restitution(string r)
    {
        try
        {
            float parsed = float.Parse(r);
            if (parsed < 0)
            {
                EventController.Instance.displayError();
            }
            else
            {
                DetProps.rCoefs[0, 0] = parsed;
            }
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setTo1on1Restitution()
    {
        input.text = DetProps.rCoefs[0, 0].ToString();
    }

    // 1 on 2 Restitution

    public void set1on2Restitution(string r)
    {
        try
        {
            float parsed = float.Parse(r);
            if (parsed < 0)
            {
                EventController.Instance.displayError();
            }
            else
            {
                DetProps.rCoefs[0, 1] = parsed;
                DetProps.rCoefs[1, 0] = parsed;
            }
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setTo1on2Restitution()
    {
        input.text = DetProps.rCoefs[0, 1].ToString();
    }

    // 1 on 3 Restitution

    public void set1on3Restitution(string r)
    {
        try
        {
            float parsed = float.Parse(r);
            if (parsed < 0)
            {
                EventController.Instance.displayError();
            }
            else
            {
                DetProps.rCoefs[0, 2] = parsed;
                DetProps.rCoefs[2, 0] = parsed;
            }
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setTo1on3Restitution()
    {
        input.text = DetProps.rCoefs[0, 2].ToString();
    }

    // 2 on 2 Restitution

    public void set2on2Restitution(string r)
    {
        try
        {
            float parsed = float.Parse(r);
            if (parsed < 0)
            {
                EventController.Instance.displayError();
            }
            else
            {
                DetProps.rCoefs[1, 1] = parsed;
            }
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setTo2on2Restitution()
    {
        input.text = DetProps.rCoefs[1, 1].ToString();
    }

    // 2 on 3 Restitution

    public void set2on3Restitution(string r)
    {
        try
        {
            float parsed = float.Parse(r);
            if (parsed < 0)
            {
                EventController.Instance.displayError();
            }
            else
            {
                DetProps.rCoefs[1, 2] = parsed;
                DetProps.rCoefs[2, 1] = parsed;
            }
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setTo2on3Restitution()
    {
        input.text = DetProps.rCoefs[1, 2].ToString();
    }

    // 3 on 3 Restitution

    public void set3on3Restitution(string r)
    {
        try
        {
            float parsed = float.Parse(r);
            if (parsed < 0)
            {
                EventController.Instance.displayError();
            }
            else
            {
                DetProps.rCoefs[2, 2] = parsed;
            }
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setTo3on3Restitution()
    {
        input.text = DetProps.rCoefs[2, 2].ToString();
    }
}