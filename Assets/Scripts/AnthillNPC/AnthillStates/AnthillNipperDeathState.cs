using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnthillNipperDeathState : NipperAnthillStateBase
{
    public override void Enter()
    {
        base.Enter();
        sensor.SetAnimation(NipperAnimationEnum.Flail);
    }
}
