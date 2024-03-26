using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
   public PlayerControls controls;

   public WeaponScriptableObject equippedWeapon;
   
   public List<WeaponScriptableObject> allWeapons = new List<WeaponScriptableObject>();

   public List<WeaponScriptableObject> equippedWeapons = new List<WeaponScriptableObject>();

   public int equipIndex;

   public void Start()
   {
      controls.AnnounceMouseScroll += Scroll;

      foreach (WeaponScriptableObject wep in allWeapons)
      {
         UnlockWeapon(wep);
      }
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
   }

   public void EquipWeapon(WeaponScriptableObject newWep)
   {
      foreach (WeaponScriptableObject wep in equippedWeapons)
      {
         if(wep!=newWep)
            wep.ChangeEquip(false);
      }
      newWep.ChangeEquip(true);
   }
   
   void OnDisable()
   {
      controls.AnnounceMouseScroll -= Scroll;
   }
}
