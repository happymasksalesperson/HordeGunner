using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damage;

    public bool active = true;

    public event Action AnnounceDamageDealt;
    
    public void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            ITakeDamage victim = other.GetComponent<ITakeDamage>();
            if (victim != null)
            {
                victim.ChangeHealth(damage);
                AnnounceDamageDealt?.Invoke();
            }
        }
    }
}