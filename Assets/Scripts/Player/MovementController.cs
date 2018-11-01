using UnityEngine;
using UnityEngine.AI;

public class MovementController : MonoBehaviour {

    public float PlayerSpeed;
    private NavMeshAgent navMeshAgent;

    public void Init()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void CalculatePathAndMove(Vector3 clickPosition)
    {
        var path = CalculatePath(clickPosition);
        Move(path);
    }

    private NavMeshPath CalculatePath(Vector3 clickPostion)
    {
        NavMeshPath path = new NavMeshPath();
        NavMeshHit hit;
        NavMesh.SamplePosition(clickPostion, out hit, Mathf.Infinity, NavMesh.AllAreas);
        navMeshAgent.CalculatePath(hit.position, path);
        return path;
    }

    private void Move(NavMeshPath path)
    {
        navMeshAgent.SetPath(path);
    }
}
