using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddObject : MonoBehaviour
{

    public static AddObject Instance;

    private TMPro.TMP_Dropdown dropdown;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            dropdown = gameObject.GetComponent<TMPro.TMP_Dropdown>();
        }
    }

    public void onObjectAdded(int index)
    {
        if (index != 0)
        {
            dropdown.value = 0;

            DetProps props = null;

            switch (index)
            {
                case 1:
                    props = new DetSphereProps();
                    props.position = new Vector3(0, 1F, 0);
                    break;
                case 2:
                    props = new DetRectProps();
                    props.position = new Vector3(0, 0.5F, 0);
                    break;
                default:
                    return;
            }

            (props.createObject()).resetValues();
            ObjectSelector.refreshOptions();
        }
    }
}
