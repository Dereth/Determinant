using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class f23Inp : MonoBehaviour
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
        if (str == "")
        {
            input.text = "0";
        }
    }

    public void onInputChange(string str)
    {
        if (DetEvents.canModify)
        {
            if (str == "")
            {
                return;
            }
            try
            {
                float parsed = float.Parse(str);
                if (parsed < 0.0f)
                {
                    throw new Exception("coefficient cannot be less than 0");
                }
                DetProps.fCoefs[1, 2] = parsed;
                DetProps.fCoefs[2, 1] = parsed;
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
        return DetProps.fCoefs[1, 2].ToString();
    }
}
