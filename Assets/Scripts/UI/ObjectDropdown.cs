using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDropdown : MonoBehaviour
{
    public static List<TMPro.TMP_Dropdown> dropdowns = new List<TMPro.TMP_Dropdown>();

    void Start()
    {
        TMPro.TMP_Dropdown dropdown = gameObject.GetComponent<TMPro.TMP_Dropdown>();
        dropdowns.Add(dropdown);
        dropdown.interactable = DetEvents.canModify;
    }
}
