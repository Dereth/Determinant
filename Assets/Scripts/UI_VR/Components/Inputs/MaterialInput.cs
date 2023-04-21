using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaterialInput : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdown;

    void Start()
    {
        dropdown = gameObject.GetComponent<TMPro.TMP_Dropdown>();
    }

    void Update()
    {
        dropdown.interactable = EventController.canEdit;
    }
}