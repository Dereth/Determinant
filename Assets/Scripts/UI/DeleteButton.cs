using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteButton : MonoBehaviour
{
    public static DeleteButton Instance;

    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            gameObject.SetActive(false);
        }
    }

    public void deleteObject()
    {
        if (DetEvents.isNormalObject())
        {
            if (DetEvents.selected is DetObjectHoldable)
            {
                DetObjectHoldable obj = (DetObjectHoldable) DetEvents.selected;
                DetObject.objects.Remove(obj);
                Destroy(obj.gameObj);
            }
            DetEvents.setSelected(null);
            ObjectSelector.refreshOptions();
        }
    }
}
