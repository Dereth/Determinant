using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectorButton : MonoBehaviour
{

    public DetObject obj;

    void Start()
    {
        obj = null;
    }

    void Update()
    {
    }

    public void press()
    {
        EventController.Instance.select(obj);
    }
}