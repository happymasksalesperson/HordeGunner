using System;
using UnityEngine;
using System.Collections;

public class BlackHoleProjectile : MonoBehaviour
{
    public float speed;
    public float waitTime;
    
    public bool active = false;
    
    public BlackHole blackHole;
    public Rigidbody rb;

    public void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
        Spawn();
    }

    private void FixedUpdate()
    {
        if(!active)
            rb.velocity = transform.forward * speed;
    }

    public void Spawn()
    {
        StartCoroutine(ActivateAfterTime());
    }

    private IEnumerator ActivateAfterTime()
    {
        yield return new WaitForSeconds(waitTime);
        active = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            TriggerEffect(other);
        }
    }

    private void TriggerEffect(Collider other)
    {
        rb.velocity = Vector3.zero;
        blackHole.OnSpawn();
    }
}