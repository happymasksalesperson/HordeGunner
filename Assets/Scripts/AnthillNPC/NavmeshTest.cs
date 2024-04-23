using UnityEngine;
using UnityEngine.AI;

public class NavmeshTest : MonoBehaviour
{
    public Transform target; 
    public NavMeshAgent agent;

    [ContextMenu("Move")]
    public void MoveToTarget()
    {
        if (agent != null)
        {
            agent.SetDestination(target.position); 
        }
    }
}
