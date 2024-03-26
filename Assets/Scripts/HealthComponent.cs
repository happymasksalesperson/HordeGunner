using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour, ITakeDamage
{
    public int maxHP;

    public int HP;

    public bool isAlive;

    public event Action<int> AnnounceChangeHealth;

    public event Action AnnounceDeath;

    public int testHPAmount;

    void OnEnable()
    {
        ChangeHealth(maxHP);
    }

    public void TestChangeHealth()
    {
        ChangeHealth(testHPAmount);
    }

    public void Resurrect()
    {
        ChangeHealth(maxHP);
    }

    public void Kill()
    {
        ChangeHealth(-maxHP);
    }

    public void ChangeHealth(int input)
    {
        int newHP = HP += input;

        if (newHP <= 0)
        {
            HP = 0;
            isAlive = false;
            AnnounceDeath?.Invoke();
        }

        else if (newHP >= maxHP)
            HP = maxHP;

        else
        {
            HP = newHP;
        }

        if (HP > 0)
            isAlive = true;

        AnnounceChangeHealth?.Invoke(HP);
    }
}