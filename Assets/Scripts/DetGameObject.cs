using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetGameObject : MonoBehaviour
{
    public DetObject detObject { get; set; }

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position = detObject.pos;// + detObject.alignVect(new Vector3(0, 0, 0));
        this.transform.eulerAngles = detObject.rot.eulerAngles;
    }
}
