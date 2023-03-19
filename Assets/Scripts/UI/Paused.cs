using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paused : MonoBehaviour
{

    public static GameObject Instance;

    void Start()
    {
        if (Instance != null && Instance != gameObject)
        {
            Destroy(this);
        }
        else
        {
            Instance = gameObject;
        }
    }
}
