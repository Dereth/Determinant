using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberInput : MonoBehaviour
{
    public TMPro.TMP_InputField input;

    void Start()
    {
        input = gameObject.GetComponent<TMPro.TMP_InputField>();
    }

    void Update()
    {
        input.interactable = EventController.canEdit;
    }
}