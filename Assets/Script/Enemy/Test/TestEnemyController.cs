using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Base;
using Manager;

namespace Enemy.Test
{
    public class TestEnemyController : MonoBehaviour, IEnemyController
    {
        protected TestEnemyMover enemyMover;
        private IEnemyHp testEnemyHp;
        private BaseHP baseHP;

        [field:SerializeField] public bool IsBoss { private set; get; }
        public Transform target;
        public int coinNum;

        GameManager gameManager => GameManager.Instance;
        public float aliveTime { private set; get; } = 0;

        private void Start()
        {
            enemyMover = GetComponent<TestEnemyMover>();
            testEnemyHp = GetComponent<IEnemyHp>();

        }

        private void Update()
        {
            enemyMover.EnemyMove(target);
            aliveTime += Time.deltaTime;

            if (IsBoss == true && testEnemyHp.enemyHp <= 0) gameManager.HadBoss = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Base")
            {
                baseHP = collision.GetComponent<BaseHP>();
                if (baseHP == null) return;

                if (IsBoss == true) baseHP.DecreaseBossHp();
                else baseHP.DecreaseHp();
                Destroy(gameObject);
            }
        }

    }
}