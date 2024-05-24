using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTimer : MonoBehaviour
{
   public int countdownTime;

   public float currentTime;

   public event Action<float> AnnounceTime;

   private bool counting = false;
   
   public void StartTimer()
   {
      currentTime = countdownTime;
      counting = true;
      StartCoroutine(CountDownRoutine());
   }

   public void StopTimer()
   {
      counting = false;
      currentTime = 0;
      AnnounceTime?.Invoke(currentTime);
   }

   private IEnumerator CountDownRoutine()
   {
      while (currentTime > 0 && counting)
      {
         AnnounceTime?.Invoke(currentTime);
         yield return null;
         currentTime -= Time.deltaTime;
      }
      AnnounceTime?.Invoke(0);
   }
}
