using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    public PlayerControls controls;

    public PlayerInventory inventory;

    public WeaponScriptableObject equippedWep;

    public void Start()
    {
        inventory.AnnounceEquippedWeapon += EquipWeapon;
        controls.AnnounceLeftClick += Shoot;  
    }
    
    private void EquipWeapon(WeaponScriptableObject newWep)
    {
        equippedWep = newWep;
        equippedWep.ChangeReadyToFire(true);
    }

    private void Shoot(InputAction.CallbackContext obj)
    {
        if (!equippedWep.readyToFire)
            return;
            
        StartCoroutine(Cooldown(equippedWep));
    }

    IEnumerator Cooldown(WeaponScriptableObject wep)
    {
        //shoot
        wep.ChangeReadyToFire(false);

        yield return new WaitForSeconds(wep.firingRate);
        
        wep.ChangeReadyToFire(true);
    }

    void OnDisable()
    {
        inventory.AnnounceEquippedWeapon -= EquipWeapon;
        controls.AnnounceLeftClick -= Shoot;
    }
}