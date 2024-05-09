using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeGunKnife : MonoBehaviour, IFallInBlackHoles
{
    public Rigidbody rb;

    public DamageDealer dd;

    void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        dd = GetComponent<DamageDealer>();
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
