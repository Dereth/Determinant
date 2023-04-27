using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectAdder : MonoBehaviour
{

    private TMPro.TMP_Dropdown dropdown;

    void Start()
    {
        dropdown = gameObject.GetComponent<TMPro.TMP_Dropdown>();

        List<string> options = new List<string>();
        options.Add("Add Object");
        options.Add("Sphere");
        options.Add("Rectangular Prism");
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
    }

    void Update()
    {
        dropdown.interactable = EventController.canEdit;
    }

    public void addObject(int index)
    {
        if (index != 0)
        {
            dropdown.value = 0;

            DetProps props = null;

            switch (index)
            {
                case 1:
                    props = new DetSphereProps();
                    props.position = new Vector3(0, 0.5F, 0);
                    break;
                case 2:
                    props = new DetRectProps();
                    props.position = new Vector3(0, 0.5F, 0);
                    break;
                default:
                    return;
            }

            (props.createObject()).resetValues();
            EventController.Instance.objectSelector.refreshOptions();
        }
    }
}