using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumpadController : MonoBehaviour
{
    public GameObject numpad;
    private float time = 0F;

    void Update()
    {
        if (NumpadButton.previous != null && NumberInput.selected == null && !NumpadButton.typing)
        {

            float dt = Time.deltaTime;
            time -= dt;
            if (time < 0)
            {
                NumpadButton.previous = null;
            }
        }
        else
        {
            time = 0.2F;
        }
    }
}
