using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnthillNipperSpawnState : NipperAnthillStateBase
{
    public override void Enter()
    {
        base.Enter();
        sensor.SetAnimation(NipperAnimationEnum.Idle);
        sensor.spawned = true;
        sensor.isAlive = true;
    }
}
