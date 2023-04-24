using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvalidInputMessage : MonoBehaviour
{

    private float time = 0F;

    void Update()
    {
        float dt = Time.deltaTime;
        time -= dt;
        if (time < 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void activate(float duration)
    {
        time = duration;
        gameObject.SetActive(true);
    }
}
