using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using Enemy.Test;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace Manager
{
    public class EnemyGenerateManager : MonoBehaviour
    {
        [SerializeField] Transform baseTransform;//SerializeÇè¡Ç∑Ç∆ÉoÉOÇÈ
        [SerializeField] Transform[] generatePoint;
        [SerializeField] GameObject[] enemy;

        private EnemyPattern enemyPattern;

        //public bool IsRun = false;

        private float generateSpan = 5.0f;
        private void Start()
        {
            for (int i = 0; i < enemy.Length; i++) 
            {
                enemy[i].GetComponent<TestEnemyController>().target = baseTransform;
            }
            enemyPattern = GetComponent<EnemyPattern>();
            CancellationToken token = this.GetCancellationTokenOnDestroy();

             GenerateObservable();
            // GenerateAsync(token);
        }
        private void RandomGenerateEnemy()
        {
            var spawnNum = Random.Range(0, generatePoint.Length);
            var enemyNum = Random.Range(0, enemy.Length);

            Instantiate(enemy[enemyNum], generatePoint[spawnNum].position, Quaternion.identity);

        }
        public void GenerateEnemy(int enemyNum,int spawnNum)
        {
            Instantiate(enemy[enemyNum], generatePoint[spawnNum].position, Quaternion.identity);
        }
        private void GenerateObservable()
        {
            this.UpdateAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(generateSpan))
                .Subscribe(_ =>
                {
                    RandomGenerateEnemy();
                })
                .AddTo(this);
        }

        private async UniTaskVoid GenerateAsync(CancellationToken token) 
        {
            while (true)
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(4.0f));
                var spawnNum = Random.Range(0, generatePoint.Length);
                enemyPattern.TankPattern(enemy[1], enemy[0], 3, generatePoint[spawnNum].position);
                await UniTask.Delay(System.TimeSpan.FromSeconds(3.0f));
            }

        }
    }
}
