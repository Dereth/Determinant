using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class DropdownInput : MonoBehaviour
{
    public TMPro.TMP_Dropdown input;
    public UnityEvent m_UpdateInput;

    void Update()
    {
        input.interactable = EventController.canEdit;
        m_UpdateInput.Invoke();
    }

    /*
     * 
     * 
     * 
     */


    /*
     * Materials
     */

    // Material

    public void setMaterial(int mat)
    {
        try
        {
            EventController.selected.modifyMaterial(mat);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToMaterial()
    {
        input.value = EventController.selected.props.material;
    }

    // Ground Material

    public void setGroundMaterial(int mat)
    {
        try
        {
            Determinant.ground.modifyMaterial(mat);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToGroundMaterial()
    {
        input.value = Determinant.ground.props.material;
    }

    // Right Hand Material

    public void setRightHandMaterial(int mat)
    {
        try
        {
            Determinant.rightHand.modifyMaterial(mat);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToRightHandMaterial()
    {
        input.value = Determinant.rightHand.props.material;
    }

    // Left Hand Material

    public void setLeftHandMaterial(int mat)
    {
        try
        {
            Determinant.leftHand.modifyMaterial(mat);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
        m_UpdateInput.Invoke();
    }

    public void setToLeftHandMaterial()
    {
        input.value = Determinant.leftHand.props.material;
    }

}