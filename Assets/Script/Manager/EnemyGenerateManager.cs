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
        [SerializeField] Transform baseTransform;//Serializeを消すとバグる
        [SerializeField] Transform[] generatePoint;
        [SerializeField] GameObject[] enemy;

        private DifficultyManager difficultyManager;
        private CoinManager coinManager => CoinManager.Instance;
        //private EnemyPattern enemyPattern;
        public bool IsRun = false;
        public bool IsPattern = false;

        private float generateSpan = 5.0f;
        public int generateCount { get; private set; } = MaxGenerateNum;
        public int enemyDeathCount { get; private set; } = MaxGenerateNum;
        private const int MaxGenerateNum= 20;
        private const int MaxWave2GenerateNum= 50;

        public int releasePos=1;
        public int releaseEnemy=1;
        private void Awake()
        {
            for (int i = 0; i < enemy.Length; i++) 
            {
                enemy[i].GetComponent<TestEnemyController>().target = baseTransform;
            }
            //enemyPattern = GetComponent<EnemyPattern>();
            CancellationToken token = this.GetCancellationTokenOnDestroy();
            difficultyManager = FindObjectOfType<DifficultyManager>();

             GenerateObservable();
             //GenerateAsync(token);
        }
        private void RandomGenerateEnemy()
        {
            var spawnNum = Random.Range(0, releasePos);
            var enemyNum = Random.Range(0, releaseEnemy);

            var enemyIns=Instantiate(enemy[enemyNum], generatePoint[spawnNum].position, Quaternion.identity);

            coinManager.canGetCoin += enemyIns.GetComponent<TestEnemyController>().coinNum;
            Debug.Log("maxCoin"+coinManager.canGetCoin);

            enemyIns.OnDestroyAsObservable()
                .Subscribe(_ =>
                {
                    enemyDeathCount--;
                })
                .AddTo(this);
            generateCount--;
        }
        public void GenerateEnemy(int enemyNum,int spawnNum)
        {
            var enemyIns =Instantiate(enemy[enemyNum], generatePoint[spawnNum].position, Quaternion.identity);
            coinManager.canGetCoin += enemyIns.GetComponent<TestEnemyController>().coinNum;

            enemyIns.OnDestroyAsObservable()
                .Subscribe(_ =>
                {
                    enemyDeathCount--;
                    Debug.Log("death"+enemyDeathCount);
                })
                .AddTo(this);
            generateCount--;
        }
        private void GenerateObservable()
        {
            this.UpdateAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(generateSpan))
                .Where(_=>generateCount>0)
                .Where(_=>IsRun==true)
                .Where(_=>IsPattern==false)
                .Subscribe(_ =>
                {
                    //Bossパターン
                    //if (generateCount == 1&&releaseEnemy==5&&difficultyManager.difficulty>0.5f) GenerateEnemy(5, 0);
                    RandomGenerateEnemy();
                    //GenerateEnemy(1, 0);
                })
                .AddTo(this);
        }

        private async UniTaskVoid GenerateAsync(CancellationToken token) 
        {
            
            while (generateCount>0)
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(3.0f),cancellationToken:token);
                IsPattern = true;

                //パターン選定して
                var spawnNum = Random.Range(0, releasePos);
                //OnePattern(1, token);
                TankPattern(0, token);

                IsPattern = false;
                await UniTask.Delay(System.TimeSpan.FromSeconds(3.0f),cancellationToken: token);
                
            }

        }
        //タンクパターン
        public async UniTaskVoid TankPattern(int generateNum,CancellationToken token) 
        {

            GenerateEnemy(1,generateNum);

            for (int i = 0; i <  2; i++)
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(1.5f),cancellationToken:token);
                GenerateEnemy(0, generateNum);
            }

        }
        //全方位パターン 
        public async UniTaskVoid AllPattern(int generateEnemyNum, CancellationToken token)
        {

            for (int i = 0; i < 3; i++)
            {
                GenerateEnemy(generateEnemyNum, i);
            }

        }
        //キャラいっぱい対応 タンクタンク、雑魚、スピード
        public async UniTaskVoid OnePattern(int generateNum, CancellationToken token)
        {

            for (int i = 0; i < 2; i++)
            {
                GenerateEnemy(1, generateNum);
                await UniTask.Delay(System.TimeSpan.FromSeconds(2f), cancellationToken: token);
            }
            GenerateEnemy(0, generateNum);
            await UniTask.Delay(System.TimeSpan.FromSeconds(3f), cancellationToken: token);

            GenerateEnemy(2, generateNum);

        }


        public void SetGenerateSpan(float difficulty)
        {
            if (difficulty > 0.5f) generateSpan -= Mathf.Abs((0.5f - difficulty) * 2);
            else if (difficulty < 0.5f) generateSpan += (0.5f - difficulty) * 2;

        }
        public void SetReleaseEnemy(float difficulty)
        {
            if (difficulty < 0.4f) releaseEnemy = 5;
            else if (difficulty < 0.6f) releaseEnemy = 7;
            else if (difficulty < 0.8f) releaseEnemy = 9;
        }
        public void ResetData() 
        {
            generateCount = MaxWave2GenerateNum;
            enemyDeathCount = MaxWave2GenerateNum;
        
        }
        
    }
}
