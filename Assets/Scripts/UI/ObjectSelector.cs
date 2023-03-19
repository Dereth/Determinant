using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectSelector : MonoBehaviour
{

    public static ObjectSelector Instance;

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

    public static void refreshOptions()
    {
        List<string> options = new List<string>();
        options.Add("Select Object");
        options.Add("Global");

        foreach (DetObject obj in DetObject.objects)
        {
            options.Add(obj.gameObj.name);
        }

        Instance.dropdown.ClearOptions();
        Instance.dropdown.AddOptions(options);
    }

    public void onObjectSelected(int index)
    {
        if (index != 0)
        {
            dropdown.value = 0;

            if (index == 1)
            {
                DetEvents.setSelected(DetGround.Instance);
                return;
            }

            DetEvents.setSelected(DetObject.objects[index - 2]);
        }
    }

    public void clearSelected()
    {
        DetEvents.setSelected(null);
    }
}
