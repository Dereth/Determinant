using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class rZinp : MonoBehaviour
{
    private TMPro.TMP_InputField input;
    private bool updated = false;
    private DetObject prevObj = null;

    void Start()
    {
        input = gameObject.GetComponent<TMPro.TMP_InputField>();
    }

    void Update()
    {
        if (!DetEvents.canModify)
        {
            prevObj = DetEvents.selected;
            input.text = getText();
            updated = false;
        }
        else if (!updated || DetEvents.selected != prevObj)
        {
            prevObj = DetEvents.selected;
            input.text = getText();
            updated = true;
        }
    }

    public void onEndEdit(string str)
    {
        if (str == "" || str == "-")
        {
            input.text = "0";
        }
    }

    public void onInputChange(string str)
    {
        if (DetEvents.canModify)
        {
            if (str == "" || str == "-")
            {
                return;
            }
            try
            {
                float parsed = float.Parse(str);
                Vector3 newVal = DetEvents.selected.props.rotation;
                newVal.z = parsed;
                DetEvents.selected.modifyRotation(newVal);
            }
            catch (Exception)
            {
                input.text = getText();
                InputInvalid.displayAlert();
            }
        }
    }

    private static string getText()
    {
        return (DetEvents.selected.rot.eulerAngles.z % 360).ToString();
    }
}
