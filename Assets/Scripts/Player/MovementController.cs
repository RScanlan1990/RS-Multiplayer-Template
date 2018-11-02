using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

public class MovementController : NetworkBehaviour {

    public float PlayerSpeed;
    private NavMeshAgent navMeshAgent;

    public void Start()
    {
        navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    [Command]
    public void CmdRequestMove(Vector3 clickPosition)
    {
        RpcDoMove(clickPosition);
    }

    [ClientRpc]
    private void RpcDoMove(Vector3 clickPosition)
    {
        navMeshAgent.SetDestination(clickPosition);
    }
}
