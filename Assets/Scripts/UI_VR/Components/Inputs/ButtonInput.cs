using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ButtonInput : MonoBehaviour
{
    public Button input;
    public UnityEvent m_UpdateInput;

    void Update()
    {
        input.interactable = EventController.canEdit;
    }

    /*
     * 
     * 
     * 
     */

}