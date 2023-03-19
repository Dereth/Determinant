using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalDisplay : MonoBehaviour
{
    public static List<GameObject> displays = new List<GameObject>();

    void Start()
    {
        displays.Add(gameObject);
        DetEvents.allDisplays.Add(gameObject);
        gameObject.SetActive(false);
    }

}
