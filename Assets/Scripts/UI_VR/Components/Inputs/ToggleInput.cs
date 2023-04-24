using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ToggleInput : MonoBehaviour
{
    public Toggle input;
    public UnityEvent m_UpdateInput;

    private DetObject prevObj = null;
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
     * 
     */

    // Unstoppable

    public void setUnstoppable(bool u)
    {
        try
        {
            EventController.selected.modifyUnstoppable(u);
        }
        catch (Exception)
        {
            EventController.Instance.displayError();
        }
    }

    public void setToUnstoppable()
    {
        input.isOn = EventController.selected.props.unstoppable;
    }

}