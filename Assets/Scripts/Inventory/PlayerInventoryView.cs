using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryView : MonoBehaviour
{
    public PlayerInventory pI;

    public Slider fireRateTimer;

    public WeaponScriptableObject currentWep;
    
    private void OnEnable()
    {
        pI = GetComponentInParent<PlayerInventory>();
        pI.AnnounceEquippedWeapon += SwitchWeapons;
        pI.AnnounceRechargeTimer += SetTimer;
    }

    private void SwitchWeapons(WeaponScriptableObject obj)
    {
        
    }


    private void SetTimer(float obj)
    {
        float clampedValue = Mathf.Clamp(obj, 0f, 1f);
        fireRateTimer.value = clampedValue;
    }

    void OnDisable()
    {
        pI.AnnounceRechargeTimer -= SetTimer;
        pI.AnnounceEquippedWeapon -= SwitchWeapons;
    }
}
