using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerInventory : MonoBehaviour
{
   public Transform firePoint;

   public GameObject model;

   public WeaponScriptableObject equippedWeapon;
   
   public List<WeaponScriptableObject> allWeapons = new List<WeaponScriptableObject>();

   public List<WeaponScriptableObject> equippedWeapons = new List<WeaponScriptableObject>();

   public int equipIndex;

   public bool initialised = false;
   
   public event Action<WeaponScriptableObject> AnnounceEquippedWeapon;

   public void Start()
   {
      foreach (WeaponScriptableObject wep in allWeapons)
      {
         UnlockWeapon(wep);
      }
      Scroll(1);
   }

   private void Update()
   {
      // Scroll up/down input
      float scrollInput = Input.GetAxis("Mouse ScrollWheel");
      if (Mathf.Abs(scrollInput) > 0)
      {
         Scroll(scrollInput);
      }

      // Left click to shoot
      if (Input.GetButtonDown("Fire1"))
      {
         Shoot();
      }
   }

   void Shoot()
   {
      GameObject proj = Instantiate(equippedWeapon.projectile, firePoint.transform.position, firePoint.transform.rotation);
      Rigidbody rb = proj.GetComponent<Rigidbody>();
      rb.velocity = firePoint.forward * equippedWeapon.projectileSpeed;
   }

   private void Scroll(float input)
   {
      int newIndex = equipIndex;
      
      if (input > 0)
      {
         newIndex++;
      }
      else if (input < 0)
      {
         newIndex--;
      }

      if (newIndex < 0)
         newIndex = equippedWeapons.Count - 1;
      else if (newIndex >= equippedWeapons.Count)
         newIndex = 0;

      equipIndex = newIndex;
      equippedWeapon = equippedWeapons[equipIndex];
      EquipWeapon(equippedWeapon);
   }

   public void UnlockWeapon(WeaponScriptableObject newWep)
   {
      if (allWeapons.Contains(newWep))
      {
         newWep.ChangeUnlock(true);
         equippedWeapons.Add(newWep);
      }

      if (!initialised)
      {
         initialised = true;
      }
   }

   public void EquipWeapon(WeaponScriptableObject newWep)
   {
      foreach (WeaponScriptableObject wep in equippedWeapons)
      {
         if(wep!=newWep)
            wep.ChangeEquip(false);
      }
      newWep.ChangeEquip(true);
      AnnounceEquippedWeapon?.Invoke(newWep);
   }
   
   void OnDisable()
   {
      equippedWeapons.Clear();
   }
}
