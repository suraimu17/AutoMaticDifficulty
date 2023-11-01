using UnityEngine;
using NavMesh2D;

namespace Enemy.Test
{
    public class TestEnemyMover : MonoBehaviour
    {
        public NavMeshAgent2D agent;
        //呼び出しがスタートだとエラーを出す
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent2D>();
        }
        public void EnemyMove(Transform target)
        {
            Debug.Log("nullCheck"+agent);
            agent.destination = target.position;
        }
    }
}