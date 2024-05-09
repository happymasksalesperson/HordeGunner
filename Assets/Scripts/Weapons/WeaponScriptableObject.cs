using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Create New Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public string name;

    public float projectileSpeed;

    public float firingRate;
    public float rechargeTimer;

    public int damage;
    
    public GameObject projectile;

    public bool equipped = false;
    public bool unlocked = false;
    public bool readyToFire = true;

    void OnEnable()
    {
        readyToFire = true;
        rechargeTimer = 0f;
    }

    public void StartCooldown()
    {
        if (!readyToFire) return;    

        readyToFire = false;
        rechargeTimer = firingRate;
        CoroutineRunner.Instance.StartCoroutine(Cooldown());  
    }

    IEnumerator Cooldown()
    {
        while (rechargeTimer > 0)
        {
            rechargeTimer -= Time.deltaTime;
            yield return null;
        }
        readyToFire = true;
    }

    public void ChangeEquip(bool input)
    {
        equipped = input;
    }

    public void ChangeUnlock(bool input)
    {
        unlocked = input;
    }

    public void ChangeReadyToFire(bool input)
    {
        readyToFire = input;
    }
}