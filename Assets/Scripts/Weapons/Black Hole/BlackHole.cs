using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlackHole : MonoBehaviour
{
    public float growthRate;
    public float rotationSpeed;
    public float maxScale;
    public float pullStrength;
    private float originalScale;
    public bool spinRight;
    public float lifeSpan;
    private Vector3 rotationAxis;
    public List<Rigidbody> objectsInBlackHole = new List<Rigidbody>();
    private Dictionary<Transform, Vector3> contactPoints = new Dictionary<Transform, Vector3>();

    private Coroutine growthCoroutine;

    private Coroutine shrinkCoroutine;

    public bool active;

    public void OnEnable()
    {
        originalScale = transform.localScale.x;
        int random = Random.Range(0, 2);
        spinRight = true;

        spinRight = random == 1 ? !spinRight : spinRight;
        rotationAxis = spinRight ? Vector3.up : -Vector3.up;
        Grow();
    }

    private IEnumerator BlackHoleLife()
    {
        yield return new WaitForSeconds(lifeSpan);
        Shrink();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (active)
        {
            IFallInBlackHoles fallBehavior = other.GetComponent<IFallInBlackHoles>();
            if (fallBehavior != null)
            {
                Vector3 contactPoint = other.ClosestPoint(transform.position);
                contactPoints[other.transform] = contactPoint;

                objectsInBlackHole.Add(other.transform.GetComponent<Rigidbody>());
                fallBehavior.InBlackHole(true);
                other.transform.SetParent(transform);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        Rigidbody obj = other.GetComponent<Rigidbody>();

        if (obj != null && objectsInBlackHole.Contains(obj))
        {
            objectsInBlackHole.Remove(obj);
            contactPoints.Remove(other.transform);

            other.transform.SetParent(null);

            IFallInBlackHoles IFall = obj.GetComponent<IFallInBlackHoles>();
            if (IFall != null)
            {
                IFall.InBlackHole(false);
            }
        }
    }


    [ContextMenu("Grow")]
    void Grow()
    {
        growthCoroutine = StartCoroutine(GrowOverTime());
    }

    [ContextMenu("Shrink")]
    void Shrink()
    {
        shrinkCoroutine = StartCoroutine(ShrinkOverTime());
    }

    void FixedUpdate()
    {
        if (active)
        {
            PullObjectsTowardsCenter();
        }

        RotateAroundYAxis();
    }

    private void PullObjectsTowardsCenter()
    {
        foreach (Rigidbody objRigidbody in objectsInBlackHole)
        {
            if (objRigidbody != null)
            {
                Vector3 pointOfAttraction = transform.position;
                Vector3 objPosition = objRigidbody.position;

                // Calculate direction towards the center from the object's position
                Vector3 pullDirection = (pointOfAttraction - objPosition).normalized;

                // Apply the force towards the center
                objRigidbody.AddForce(pullDirection * pullStrength * objRigidbody.mass);

                // Calculate the new rotation looking away from the center
                Quaternion toRotation = Quaternion.LookRotation(-pullDirection, Vector3.up);

                // Smoothly rotate the object to face away from the center
                objRigidbody.MoveRotation(Quaternion.Lerp(objRigidbody.rotation, toRotation, Time.fixedDeltaTime * pullStrength));

                // Debug line showing the direction of force applied
                Debug.DrawLine(objPosition, pointOfAttraction, Color.red);
            }
        }
    }

    IEnumerator GrowOverTime()
    {
        while (transform.localScale.x < maxScale)
        {
            transform.localScale += new Vector3(growthRate, growthRate, growthRate);
            yield return null;
        }

        Rigidbody rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        StartCoroutine(BlackHoleLife());
        active = true;
    }

    IEnumerator ShrinkOverTime()
    {
        active = false;
        foreach (Rigidbody obj in objectsInBlackHole)
        {
            obj.useGravity = true;
            obj.transform.SetParent(null);
            IFallInBlackHoles IFall = obj.GetComponent<IFallInBlackHoles>();
            IFall.InBlackHole(false);
        }

        while (transform.localScale.x > originalScale)
        {
            transform.localScale -= new Vector3(growthRate, growthRate, growthRate);
            yield return null;
        }
        
        Destroy(gameObject);
    }

    void RotateAroundYAxis()
    {
        transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
    }

    void OnDisable()
    {
        objectsInBlackHole.Clear();
    }
}