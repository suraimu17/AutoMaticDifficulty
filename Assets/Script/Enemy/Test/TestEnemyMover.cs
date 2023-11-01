using UnityEngine;
using NavMesh2D;

namespace Enemy.Test
{
    public class TestEnemyMover : MonoBehaviour
    {
        public NavMeshAgent2D agent;
        //�Ăяo�����X�^�[�g���ƃG���[���o��
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