using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class unstinp : MonoBehaviour
{
    private Toggle toggle;
    private bool updated = false;
    private DetObject prevObj = null;

    void Start()
    {
        toggle = gameObject.GetComponent<Toggle>();
    }

    void Update()
    {
        if (!DetEvents.canModify)
        {
            prevObj = DetEvents.selected;
            toggle.isOn = DetEvents.selected.props.unstoppable;
            updated = false;
        }
        else if (!updated || DetEvents.selected != prevObj)
        {
            prevObj = DetEvents.selected;
            toggle.isOn = DetEvents.selected.props.unstoppable;
            updated = true;
        }
    }

    public void onToggle(bool b)
    {
        if (DetEvents.canModify)
        {
            try
            {
                DetEvents.selected.modifyUnstoppable(b);
            }
            catch (Exception)
            {
                toggle.isOn = DetEvents.selected.props.unstoppable;
                InputInvalid.displayAlert();
            }
        }
    }
}
