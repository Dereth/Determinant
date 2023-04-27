using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetGameObject : MonoBehaviour
{

    public DetObject detObject { get; set; }

    void Update()
    {
        this.transform.position = detObject.pos;
        this.transform.eulerAngles = detObject.rot.eulerAngles;
    }
}
