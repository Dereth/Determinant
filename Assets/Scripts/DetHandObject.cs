using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetHandObject : DetGameObject
{

    void Update()
    {
        bool rightHand = ((DetHandProps)detObject.props).isRightHand();
        this.transform.position = rightHand ? VRPositioning.rightPos() : VRPositioning.leftPos();
        this.transform.rotation = rightHand ? VRPositioning.rightRot() : VRPositioning.leftRot();
    }

}