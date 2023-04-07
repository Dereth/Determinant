using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class DetObjectRendered : DetObject
{

    // Corresponding GameObject
    public GameObject gameObj { get; private set; }

    public DetObjectRendered(DetProps props) : base(props)
    {
        gameObj = new GameObject(props.createName());
        gameObj.AddComponent<MeshRenderer>().material = props.getRenderMaterial();
        gameObj.AddComponent<DetGameObject>().detObject = this;
    }

}