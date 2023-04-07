using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class DetObjectHoldable : DetObjectRendered
{

    public DetHand hand { get; set; }

    public DetObjectHoldable(DetProps props) : base(props)
    {
        hand = null;
    }

    public override bool isUnstoppable()
    {
        if (hand != null)
        {
            return true;
        }
        else
        {
            return base.isUnstoppable();
        }
    }

}