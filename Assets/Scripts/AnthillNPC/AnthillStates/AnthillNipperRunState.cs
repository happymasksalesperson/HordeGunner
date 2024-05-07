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
        agent.SetDestination(sensor.target.position);
    }

    public override void Execute(float aDeltaTime, float aTimeScale)
    {
        base.Execute(aDeltaTime, aTimeScale);
        distance = Vector3.Distance(sensor.transform.position, sensor.target.position);
        if (distance <= minDist)
            sensor.inRange = true;
    }
}
