using System.Collections.Generic;
using UnityEngine;

public class Whirlpool : MonoBehaviour
{
    public List<Transform> trappedTransforms = new List<Transform>();

    private void OnTriggerEnter(Collider other)
    {
        // Add entered Transform to the list
        trappedTransforms.Add(other.transform);

        // Fire raycast from this whirlpool's position to the contact point
        if (Physics.Raycast(transform.position, other.transform.position - transform.position, out RaycastHit hit))
        {
            // Freeze the position where the object makes contact with the whirlpool
            other.transform.position = hit.point;
            IFallInBlackHoles victim = other.GetComponent<IFallInBlackHoles>();
            if(victim!=null)
                victim.InBlackHole(true);
            // Set the transform's parent to this whirlpool to create a transformation hierarchy
            other.transform.SetParent(transform);
            
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;  // Freezes the Rigidbody in place
            }
        }
    }
}