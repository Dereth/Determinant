using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticView : MonoBehaviour
{
    void Update()
    {
        gameObject.transform.position = EventController.Instance.VRMenu.transform.position;
        gameObject.transform.rotation = EventController.Instance.VRMenu.transform.rotation;
    }
}