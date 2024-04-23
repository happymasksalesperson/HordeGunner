using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnthillNipperRunState : NipperAnthillStateBase
{
    public float distance;

    public float minDist;
    
    public override void Enter()
    {
        base.Enter();
        sensor.SetAnimation(NipperAnimationEnum.Run);
    }
}
