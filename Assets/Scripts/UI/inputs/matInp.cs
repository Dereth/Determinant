using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class matInp : MonoBehaviour
{
    private TMPro.TMP_Dropdown dropdown;
    private bool updated = false;
    private DetObject prevObj = null;

    void Start()
    {
        dropdown = gameObject.GetComponent<TMPro.TMP_Dropdown>();
    }

    void Update()
    {
        if (!DetEvents.canModify)
        {
            prevObj = DetEvents.selected;
            dropdown.value = DetEvents.selected.props.material;
            updated = false;
        }
        else if (!updated || DetEvents.selected != prevObj)
        {
            prevObj = DetEvents.selected;
            dropdown.value = DetEvents.selected.props.material;
            updated = true;
        }
    }

    public void onInputChange(int index)
    {
        if (DetEvents.canModify)
        {
            try
            {
                DetEvents.selected.modifyMaterial(index);
            }
            catch (Exception)
            {
                dropdown.value = DetEvents.selected.props.material;
                InputInvalid.displayAlert();
            }
        }
    }
}
