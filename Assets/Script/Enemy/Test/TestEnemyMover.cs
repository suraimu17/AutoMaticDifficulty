using UnityEngine;
using NavMesh2D;

public class TestEnemyMover : MonoBehaviour
{
    public NavMeshAgent2D agent;
    //[SerializeField] Transform target;
    private void Start()
    {
        agent = GetComponent<NavMeshAgent2D>();
    }
    public void EnemyMove(Transform target)
    {
        //agent.SetDestination(target.position);
        agent.destination = target.position;
    }
}
