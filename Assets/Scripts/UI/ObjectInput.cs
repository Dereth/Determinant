using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInput : MonoBehaviour
{
    public static List<TMPro.TMP_InputField> inputs = new List<TMPro.TMP_InputField>();

    void Start()
    {
        TMPro.TMP_InputField input = gameObject.GetComponent<TMPro.TMP_InputField>();
        inputs.Add(input);
        input.interactable = DetEvents.canModify;
    }
}
