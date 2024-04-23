using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class NipperAnthillStateBase : AntAIState
{
    public NipperSensor sensor;

    public override void Enter()
    {
        base.Enter();
        sensor = GetComponentInParent<NipperSensor>();
    }
}
