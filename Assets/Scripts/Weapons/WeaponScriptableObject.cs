using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Create New Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    public string name;

    public float firingRate;

    public int damage;

    public GameObject projectile;

    public bool equipped = false;

    public void ChangeEquip(bool input)
    {
        equipped = input;
    }

    public bool unlocked = false;

    public void ChangeUnlock(bool input)
    {
        unlocked = input;
    }
}