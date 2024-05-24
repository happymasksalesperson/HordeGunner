using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeGunKnife : MonoBehaviour, IFallInBlackHoles
{
    public Rigidbody rb;

    public DamageDealer dd;

    public DecayOverTime decay;
    
    [SerializeField] private float threshold = 0.1f;

    public bool active;

    void OnEnable()
    {
        active = true;
    }
    
    //TODO: figure out knife sticking into ragdoll
    
    /*
    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody otherRb = collision.gameObject.GetComponent<Rigidbody>();

        if (otherRb != null)
        {
            ITakeDamage takeDamage = collision.gameObject.GetComponent<ITakeDamage>();

            if (takeDamage != null)
            {
                FixedJoint fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.connectedBody = otherRb;
                fixedJoint.breakForce = Mathf.Infinity;
                fixedJoint.breakTorque = Mathf.Infinity;
                SetOff();
            }
        }
    }*/

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < threshold)
        {
            SetOff();
        }
    }

    private void SetOff()
    {
        active = false;
        dd.active = false;
        decay.Decay();
    }

    public void InBlackHole(bool input)
    {
        if (input)
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            dd.active = false;
        }
        else
        {
            rb.useGravity = true;
        }
    }
}
