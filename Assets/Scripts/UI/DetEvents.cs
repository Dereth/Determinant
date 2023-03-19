using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetEvents : MonoBehaviour
{

    public static DetObject selected = null;
    public static List<GameObject> allDisplays = new List<GameObject>();
    public static bool canModify = true;

    void Start()
    {
    }

    void Update()
    {
    }

    public static bool isNormalObject()
    {
        return selected != null && (
            selected is DetRect ||
            selected is DetSphere
        );
    }

    public static void setSelected(DetObject detObj)
    {
        selected = detObj;

        foreach (GameObject obj in allDisplays)
        {
            obj.SetActive(false);
        }
        DeleteButton.Instance.gameObject.SetActive(false);

        if (selected != null)
        {
            foreach (GameObject obj in DefaultDisplay.displays)
            {
                obj.SetActive(true);
            }

            if (selected is DetGround)
            {
                foreach (GameObject obj in GlobalDisplay.displays)
                {
                    obj.SetActive(true);
                }
            }
            else if (selected is DetRect)
            {
                foreach (GameObject obj in NormalDisplay.displays)
                {
                    obj.SetActive(true);
                }
                foreach (GameObject obj in RectDisplay.displays)
                {
                    obj.SetActive(true);
                }
                if (canModify)
                {
                    DeleteButton.Instance.gameObject.SetActive(true);
                }
            }
            else if (selected is DetSphere)
            {
                foreach (GameObject obj in NormalDisplay.displays)
                {
                    obj.SetActive(true);
                }
                foreach (GameObject obj in SphereDisplay.displays)
                {
                    obj.SetActive(true);
                }
                if (canModify)
                {
                    DeleteButton.Instance.gameObject.SetActive(true);
                }
            }
        }
    } 

    public static void setCanModify(bool modify)
    {
        canModify = modify;
        if (modify)
        {
            AddObject.Instance.gameObject.SetActive(true);
            if (isNormalObject())
            {
                DeleteButton.Instance.gameObject.SetActive(true);
            }
            foreach (TMPro.TMP_InputField input in ObjectInput.inputs)
            {
                input.interactable = true;
            }
            foreach (Toggle toggle in ObjectToggle.toggles)
            {
                toggle.interactable = true;
            }
        }
        else
        {
            AddObject.Instance.gameObject.SetActive(false);
            DeleteButton.Instance.gameObject.SetActive(false);
            foreach (TMPro.TMP_InputField input in ObjectInput.inputs)
            {
                input.interactable = false;
            }
            foreach (Toggle toggle in ObjectToggle.toggles)
            {
                toggle.interactable = false;
            }
        }
    }
}
