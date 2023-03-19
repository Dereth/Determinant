using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectToggle : MonoBehaviour
{
    public static List<Toggle> toggles = new List<Toggle>();

    void Start()
    {
        Toggle toggle = gameObject.GetComponent<Toggle>();
        toggles.Add(toggle);
        toggle.interactable = DetEvents.canModify;
    }
}
