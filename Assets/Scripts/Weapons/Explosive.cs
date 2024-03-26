using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
   //explosive explodes when hp reaches 0
   //explosion grows over time, and flings rigidbodies + decreases HP for all inside
   
   public HealthComponent hp;

   public int explosionDamage;
   
   public float explosionForce;
   public float explosionRadius;
   public float explosionTime = 1f;

   private float currentRadius = 0f;

   public event Action<float> AnnounceRadiusSize;
   public event Action AnnounceExplosionFinished;

   private void OnEnable()
   {
      hp.AnnounceDeath += Explode;
   }

   public void Explode()
   {
      StartCoroutine(ExpandExplosion());
   }

   IEnumerator ExpandExplosion()
   {
      float elapsedTime = 0f;
      while (elapsedTime < explosionTime)
      {
         currentRadius = Mathf.Lerp(0f, explosionRadius, elapsedTime / explosionTime);
         // Get all colliders within the current explosion radius
         AnnounceRadiusSize?.Invoke(currentRadius);
         Collider[] colliders = Physics.OverlapSphere(transform.position, currentRadius);

         foreach (Collider col in colliders)
         {
            // Check if the collider's GameObject is not the same as the ExplosionController GameObject
            if (col.gameObject != gameObject)
            {
               Rigidbody rb = GetRigidbody(col.gameObject);
               if (rb != null)
               {
                  col.isTrigger = false;
                  rb.isKinematic = false;
                  rb.useGravity = true;
                  rb.AddExplosionForce(explosionForce, transform.position, currentRadius);
               }

               HealthComponent health = GetHealthComponent(col.gameObject);
               if (health != null)
               {
                  health.ChangeHealth(explosionDamage);
               }
            }
         }

         elapsedTime += Time.deltaTime;
         yield return null;
      }

      AnnounceExplosionFinished?.Invoke();
   }

   private Rigidbody GetRigidbody(GameObject obj)
   {
      Rigidbody rb = obj.GetComponent<Rigidbody>();
      if (rb == null)
         rb = obj.GetComponentInChildren<Rigidbody>();
      return rb;
   }

   private HealthComponent GetHealthComponent(GameObject obj)
   {
      HealthComponent newHP = obj.GetComponent<HealthComponent>();
      return newHP;
   }

   void OnDisable()
   {
      hp.AnnounceDeath -= Explode;
   }
}
