using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage;

    public void OnTriggerEnter(Collider other)
    {
        ITakeDamage victim = other.GetComponent<ITakeDamage>();
        if (victim != null)
        {
            victim.ChangeHealth(damage);
        }
    }
}