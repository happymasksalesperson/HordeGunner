using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlackHole : MonoBehaviour
{
    public float growthRate;
    public float rotationSpeed;
    public float maxScale;
    private float originalScale;
    public bool spinRight;
    private Vector3 rotationAxis;
    public List<Transform> objectsInBlackHole = new List<Transform>();
    private Dictionary<Transform, Vector3> contactPoints = new Dictionary<Transform, Vector3>();

    private Coroutine growthCoroutine;

    private Coroutine shrinkCoroutine;
    
    public bool active;

    public void OnEnable()
    {
        Grow();
        originalScale = transform.localScale.x;
        int random = Random.Range(0, 2);
        spinRight = true;

        spinRight = random == 1 ? !spinRight : spinRight;
        rotationAxis = spinRight ? Vector3.up : -Vector3.up;
    }

    private void OnTriggerEnter(Collider other)
    {
        IFallInBlackHoles fallBehavior = other.GetComponent<IFallInBlackHoles>();
        if (fallBehavior != null)
        {
            Vector3 contactPoint = other.ClosestPoint(transform.position);
            contactPoints[other.transform] = contactPoint;

            objectsInBlackHole.Add(other.transform);
            fallBehavior.InBlackHole(true);
        }
    }


    [ContextMenu("Grow")]
    void Grow()
    {
        active = true;
        growthCoroutine = StartCoroutine(GrowOverTime());
    }

    [ContextMenu("Shrink")]
    void Shrink()
    {
        shrinkCoroutine = StartCoroutine(ShrinkOverTime());
    }

    void Update()
    {
        if (active)
        {
            PullObjectsTowardsCenter();
        }
        RotateAroundYAxis();
    }

    private void PullObjectsTowardsCenter()
    {
        foreach (Transform objTransform in objectsInBlackHole)
        {
            Vector3 directionToCenter = (transform.position - objTransform.position).normalized;

            Vector3 contactPoint = contactPoints[objTransform];

            Vector3 directionToCenterFromContact = (transform.position - contactPoint).normalized;

            Vector3 moveDirection = Vector3.Project(directionToCenter, directionToCenterFromContact);

            objTransform.position += moveDirection * Time.deltaTime;
        }
    }

    IEnumerator GrowOverTime()
    {
        while (transform.localScale.x < maxScale)
        {
            transform.localScale += new Vector3(growthRate, growthRate, growthRate);
            yield return null;
        }
    }

    IEnumerator ShrinkOverTime()
    {
        active = false;
        while (transform.localScale.x > originalScale)
        {
            transform.localScale -= new Vector3(growthRate, growthRate, growthRate);
            yield return null;
        }

        foreach (Transform obj in objectsInBlackHole)
        {
            IFallInBlackHoles IFall = obj.GetComponent<IFallInBlackHoles>();
            IFall.InBlackHole(false);
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