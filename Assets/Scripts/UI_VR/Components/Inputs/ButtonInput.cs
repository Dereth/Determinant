using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ButtonInput : MonoBehaviour
{
    public TMPro.TMP_Dropdown input;
    public UnityEvent m_UpdateInput;

    void Update()
    {
        if (EventController.canEdit)
        {
            input.interactable = true;
        }
        else
        {
            m_UpdateInput.Invoke();
            input.interactable = false;
        }
    }

    /*
     * 
     * 
     * 
     */

}