using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using Enemy.Test;
using Cysharp.Threading.Tasks;
using System.Threading;
using Util;

namespace Manager
{
    public class EnemyGenerateManager : MonoBehaviour
    {
        [SerializeField] Transform baseTransform;//Serializeを消すとバグる
        [SerializeField] Transform[] generatePoint;
        [SerializeField] GameObject[] enemy;

        [field:SerializeField]private float[] patternList;
        [field:SerializeField]private float[] generatePosList;
        [field:SerializeField]private float[] enemyList;

        private WeightedChooser weightedChooser;
        private DifficultyManager difficultyManager;
        private GameManager gameManager;
        private CoinManager coinManager => CoinManager.Instance;
        public bool IsRun = false;
        public bool IsPattern = false;

        private float generateSpan = 5.0f;
        private float patternSpan = 10.0f;
        public int generateCount { get; private set; } = MaxGenerateNum;
        public int enemyDeathCount { get; private set; } = MaxGenerateNum;
        private const int MaxGenerateNum= 20;
        private const int MaxWave2GenerateNum= 50;

        private void Awake()
        {
            for (int i = 0; i < enemy.Length; i++) 
            {
                enemy[i].GetComponent<TestEnemyController>().target = baseTransform;
            }

            CancellationToken token = this.GetCancellationTokenOnDestroy();
            difficultyManager = FindObjectOfType<DifficultyManager>();
            gameManager = FindObjectOfType<GameManager>();

            GenerateObservable();
            GenerateAsync(token);
        }
        private void RandomGenerateEnemy()
        {
            var spawnNum = Random.Range(0, LotGeneratePos());
            var enemyNum = Random.Range(0, LotEnemy());

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
                })
                .AddTo(this);
            generateCount--;
        }
        //Wave1
        private void GenerateObservable()
        {
            this.UpdateAsObservable()
                .ThrottleFirst(System.TimeSpan.FromSeconds(generateSpan))
                .Where(_=>generateCount>0)
                .Where(_=>IsRun==true)
                .Where(_=>IsPattern==false)
                .Subscribe(_ =>
                {
                    Debug.Log("生成");
                    //Bossパターン
                    //if (generateCount == 1&&releaseEnemy==5&&difficultyManager.difficulty>0.5f) GenerateEnemy(5, 0);
                    RandomGenerateEnemy();
                })
                .AddTo(this);
        }
        //Wave2
        private async UniTaskVoid GenerateAsync(CancellationToken token) 
        {
            while (true)
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(2.0f), cancellationToken: token);
                if (generateCount < 5||(gameManager.waveNum == 1 && MaxGenerateNum - generateCount<5))continue;

                IsPattern = true;
                //パターン選定して

                GetPattern(LotPattern(),token,LotGeneratePos());
                await UniTask.Delay(System.TimeSpan.FromSeconds(2.0f), cancellationToken: token);
                IsPattern = false;
                await UniTask.Delay(System.TimeSpan.FromSeconds(patternSpan),cancellationToken: token);
                
            }

        }

        private void GetPattern(int Pattern,CancellationToken token,int generatePos)
        {
            switch (Pattern)
            {
                case 0://要所対応　　Tankを先頭
                      TankPattern(generatePos,token);
                    break;
                case 1://全方位雑魚
                      AllPattern(0);
                    break;
                case 2://要所と量対応 Tank,Tank,雑魚 Speed
                      OnePattern(generatePos, token);
                    break;
                case 3://量対応キャラを置く人　
                      AllStrongPattern(token);
                    break;
                case 4://様子見対応 スピード全方位
                    AllPattern(2);
                    break;
                case 5://難易度強い時　
                    AllStrongPattern(token);
                    break;

                default:
                    Debug.Log("Default");
                    break;

            }

        }
        //タンクパターン
        public async UniTaskVoid TankPattern(int generatePos,CancellationToken token) 
        {

            GenerateEnemy(1,generatePos);

            for (int i = 0; i <  2; i++)
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(2f),cancellationToken:token);
                GenerateEnemy(0, generatePos);
            }

        }
        //全方位パターン 
        public async UniTaskVoid AllPattern(int generateEnemyNum)
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
        //全方位にいっぱい置く
        public async UniTaskVoid AllStrongPattern(CancellationToken token)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int k = 0; k < 3; k++)
                {
                    GenerateEnemy(i, k);
                }
                await UniTask.Delay(System.TimeSpan.FromSeconds(4f), cancellationToken: token);
            }

        }

        //重み付き確率の抽選
        private int LotPattern() 
        {
            //パターンの確率を代入
            weightedChooser = new WeightedChooser(patternList);//　前に書くかも

            return weightedChooser.Choose();
        
        }
        private int LotGeneratePos()
        { 
            var weightedChooser = new WeightedChooser(generatePosList);

            return weightedChooser.Choose();
        }
        private int LotEnemy()
        {
            var weightedChooser = new WeightedChooser(enemyList);

            return weightedChooser.Choose();
        }
        public void SetGenerateSpan(float difficulty)
        {
            if (difficulty > 0.5f) generateSpan -= Mathf.Abs((0.5f - difficulty) * 2);
            else if (difficulty < 0.5f) generateSpan += (0.5f - difficulty) * 2;

            //パターンのスパンも変える
        }
        public void SetReleaseEnemy(int releaseNum)
        {
            generatePosList[releaseNum] = 1;
        }
        public void SetReleaseGeneratePos(int releaseNum)
        {
            enemyList[releaseNum] = 1;
        }
        public void ResetData() 
        {
            generateCount = MaxWave2GenerateNum;
            enemyDeathCount = MaxWave2GenerateNum;
        }
        
    }
}
