using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;
using UnityEngine.AI;

public class NipperAnthillStateBase : AntAIState
{
    public NipperSensor sensor;

    public NavMeshAgent agent;

    public override void Enter()
    {
        base.Enter();
        agent = GetComponentInParent<NavMeshAgent>();
        sensor = GetComponentInParent<NipperSensor>();
    }
}
