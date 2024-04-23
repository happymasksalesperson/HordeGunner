using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnthillNipperAttackState : NipperAnthillStateBase
{
    public override void Enter()
    {
        base.Enter();
        sensor.SetAnimation(NipperAnimationEnum.Attack01);
    }
}
