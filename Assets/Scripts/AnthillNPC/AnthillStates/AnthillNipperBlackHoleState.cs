using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnthillNipperBlackHoleState :  NipperAnthillStateBase
{
    public override void Enter()
    {
        base.Enter();
        sensor.SetAnimation(NipperAnimationEnum.Flail);
        agent.isStopped = true;
    }

    public override void Exit()
    {
        base.Exit();
        agent.isStopped = false;
    }
}

