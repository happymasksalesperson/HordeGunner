using System;
using System.Collections;
using System.Collections.Generic;
using Anthill.AI;
using UnityEngine;

public class NipperSensor : MonoBehaviour, ISense, IFallInBlackHoles
{
    public Transform target;
    
    public bool spawned;
    public bool isAlive;
    public bool inRange;
    public bool attacking;
    public bool fallingInBlackHole;

    public event Action<NipperAnimationEnum> AnnounceAnimation;

    public HealthComponent HP;

    public GameObject ragdoll;
    private GameObject deathDoll;

    public void OnEnable()
    {
        deathDoll = Instantiate(ragdoll);
        deathDoll.SetActive(false);
        HP.AnnounceChangeHealth += Alive;
        HP.AnnounceDeath += Die;
    }

    private void Alive(int hp)
    {
        if (hp > 0)
        {
            isAlive = true;
        }
    }

    private void Die()
    {
        isAlive = false;
        spawned = false;
        
        //HACK
       // deathDoll.transform.position = new Vector3(transform.position.x, transform.position.y +1, transform.position.z);

        deathDoll.transform.position = transform.position;
        deathDoll.transform.rotation = transform.rotation;
        deathDoll.SetActive(true);
        gameObject.SetActive(false);
    }

    public void CollectConditions(AntAIAgent aAgent, AntAICondition aWorldState)
    {
        aWorldState.BeginUpdate(aAgent.planner);
        {
            aWorldState.Set(NipperScenarioEnum.Spawned, spawned);
            aWorldState.Set(NipperScenarioEnum.Alive, isAlive);
            aWorldState.Set(NipperScenarioEnum.InRange, inRange);
            aWorldState.Set(NipperScenarioEnum.Attacking, attacking);
            aWorldState.Set(NipperScenarioEnum.BlackHole, fallingInBlackHole);
        }
        aWorldState.EndUpdate();
    }

    public void SetAnimation(NipperAnimationEnum newAnim)
    {
        AnnounceAnimation?.Invoke(newAnim);
    }

    void OnDisable()
    {
        HP.AnnounceDeath -= Die;
    }

    public void InBlackHole(bool input)
    {
        fallingInBlackHole = input;
    }
}
