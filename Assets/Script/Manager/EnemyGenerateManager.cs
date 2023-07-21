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
        public bool IsRun = false;

        private float generateSpan = 5.0f;
        public int generateCount { get; private set; } = MaxGenerateNum;
        public int enemyDeathCount { get; private set; } = MaxGenerateNum;
        private const int MaxGenerateNum= 15;
        private const int MaxWave2GenerateNum= 50;
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

            var enemyIns=Instantiate(enemy[enemyNum], generatePoint[spawnNum].position, Quaternion.identity);
            enemyIns.OnDestroyAsObservable()
                .Subscribe(_ =>
                {
                    enemyDeathCount--;
                    Debug.Log("death"+enemyDeathCount);
                })
                .AddTo(this);
            generateCount--;
        }
        public void GenerateEnemy(int enemyNum,int spawnNum)
        {
            Instantiate(enemy[enemyNum], generatePoint[spawnNum].position, Quaternion.identity);
        }
        private void GenerateObservable()
        {
            this.UpdateAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(generateSpan))
                .Where(_=>generateCount>0)
                .Where(_=>IsRun==true)
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

        public void ResetData() 
        {
            generateCount = MaxWave2GenerateNum;
            enemyDeathCount = MaxWave2GenerateNum;
        
        }
    }
}
