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
    private Collider[] hitColliders = new Collider[100]; // Adjustable based on expected max objects
    private SphereCollider sphereCollider;

    void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider == null)
        {
            Debug.LogError("SphereCollider is missing from the BlackHole object.");
        }
    }

    public void OnSpawn()
    {
        originalScale = transform.localScale.x;
        int random = Random.Range(0, 2);
        spinRight = random == 1 ? !spinRight : spinRight;
        rotationAxis = spinRight ? Vector3.up : -Vector3.up;
        Grow();
        active = true;
    }

    private IEnumerator BlackHoleLife()
    {
        yield return new WaitForSeconds(lifeSpan);
        Shrink();
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
            DetectAndHandleObjects();
            PullObjectsTowardsCenter();
        }
        RotateAroundYAxis();
    }

    private void DetectAndHandleObjects()
    {
        int numColliders = Physics.OverlapSphereNonAlloc(transform.position, sphereCollider.radius * transform.localScale.x, hitColliders);
        HashSet<Rigidbody> detectedRigidbodies = new HashSet<Rigidbody>();

        for (int i = 0; i < numColliders; i++)
        {
            Collider col = hitColliders[i];
            IFallInBlackHoles fallBehavior = col.GetComponent<IFallInBlackHoles>();
            if (fallBehavior != null)
            {
                var rb = col.attachedRigidbody;
                if (rb != null && !objectsInBlackHole.Contains(rb))
                {
                    objectsInBlackHole.Add(rb);
                    Vector3 contactPoint = col.ClosestPoint(transform.position);
                    contactPoints[col.transform] = contactPoint;
                    fallBehavior.InBlackHole(true);
                }
                detectedRigidbodies.Add(rb);
            }
        }

        // Remove objects that are no longer detected within the radius
        for (int i = objectsInBlackHole.Count - 1; i >= 0; i--)
        {
            var rb = objectsInBlackHole[i];
            if (!detectedRigidbodies.Contains(rb))
            {
                objectsInBlackHole.Remove(rb);
                contactPoints.Remove(rb.transform);
                IFallInBlackHoles fallBehavior = rb.GetComponent<IFallInBlackHoles>();
                if (fallBehavior != null)
                {
                    fallBehavior.InBlackHole(false);
                }
            }
        }
    }

    private void PullObjectsTowardsCenter()
    {
        foreach (Rigidbody objRigidbody in objectsInBlackHole)
        {
            if (objRigidbody != null)
            {
                Vector3 pointOfAttraction = transform.position;
                Vector3 objPosition = objRigidbody.position;
                Vector3 pullDirection = (pointOfAttraction - objPosition).normalized;
                objRigidbody.AddForce(pullDirection * pullStrength * objRigidbody.mass);

                Quaternion toRotation = Quaternion.LookRotation(-pullDirection, Vector3.up);
                objRigidbody.MoveRotation(Quaternion.Lerp(objRigidbody.rotation, toRotation, Time.fixedDeltaTime * pullStrength));

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
    }

    IEnumerator ShrinkOverTime()
    {
        active = false;
        foreach (Rigidbody obj in objectsInBlackHole)
        {
            obj.useGravity = true; 
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