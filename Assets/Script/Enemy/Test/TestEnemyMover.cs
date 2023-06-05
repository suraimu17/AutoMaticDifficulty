using UnityEngine;
using NavMesh2D;

namespace Enemy.Test
{
    public class TestEnemyMover : MonoBehaviour
    {
        public NavMeshAgent2D agent;
        private void Start()
        {
            agent = GetComponent<NavMeshAgent2D>();
        }
        public void EnemyMove(Transform target)
        {
            agent.destination = target.position;
        }
    }
}