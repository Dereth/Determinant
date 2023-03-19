using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInvalid : MonoBehaviour
{

    public static GameObject Instance;
    private static float time = 2F;

    void Start()
    {
        if (Instance != null && Instance != gameObject)
        {
            Destroy(this);
        }
        else
        {
            Instance = gameObject;
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        float dt = Time.deltaTime;
        time -= dt;
        if (time < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public static void displayAlert()
    {
        Instance.SetActive(true);
        time = 1.5F;
    }
}
