using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using Enemy;
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

        public List<float> aliveTimeList;

        private WeightedChooser weightedChooser;
        private DifficultyManager difficultyManager;
        private GameManager gameManager => GameManager.Instance;
        private StyleCheck styleCheck;
        private CoinManager coinManager => CoinManager.Instance;
        public bool IsRun = false;
        public bool IsPattern = false;

        private float generateSpan = 7.0f;
        private float patternSpan = 12.0f;
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
            styleCheck = FindObjectOfType<StyleCheck>();
            aliveTimeList= new List<float>();


            GenerateObservable();
            GenerateAsync(token);

            this.ObserveEveryValueChanged(_=>enemyDeathCount)
                .Where(_=>enemyDeathCount<0)
                .Subscribe(_ =>
                {
                    enemyDeathCount = 0;
                })
                .AddTo(this);
        }
        private void RandomGenerateEnemy()
        {
            var spawnNum = LotGeneratePos();
            var enemyNum = LotEnemy();

            var enemyIns=Instantiate(enemy[enemyNum], generatePoint[spawnNum].position, Quaternion.identity);

            coinManager.canGetCoin += enemyIns.GetComponent<TestEnemyController>().coinNum;
            //Debug.Log("maxCoin"+coinManager.canGetCoin);

            enemyIns.OnDestroyAsObservable()
                .Subscribe(_ =>
                {
                    enemyDeathCount--;
                    //if (enemyIns.GetComponent<TestEnemyController>().IsBoss == true) gameManager.HadBoss = true;
                    aliveTimeList.Add(enemyIns.GetComponent<TestEnemyController>().aliveTime);
                    Debug.Log("time" + enemyIns.GetComponent<TestEnemyController>().aliveTime);
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
                    aliveTimeList.Add(enemyIns.GetComponent<TestEnemyController>().aliveTime);
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
                    Debug.Log("生成");
                    if (generateCount == 1&&difficultyManager.difficulty>0.5f&&gameManager.waveNum==2) GenerateEnemy(3, 1);
                    else RandomGenerateEnemy();
                })
                .AddTo(this);
        }
        private async UniTaskVoid GenerateAsync(CancellationToken token) 
        {
            while (true)
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(2.0f), cancellationToken: token);
                if (generateCount < 5||(gameManager.waveNum == 1 && MaxGenerateNum - generateCount<5))continue;
                Debug.Log("pattern生成");
                IsPattern = true;

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
                case 3://量対応キャラを置く人　 スピード、タンクタンク、スピード
                    TankSpeedPattern(generatePos, token);
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
                await UniTask.Delay(System.TimeSpan.FromSeconds(4f),cancellationToken:token);
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
                await UniTask.Delay(System.TimeSpan.FromSeconds(3f), cancellationToken: token);
            }
            GenerateEnemy(0, generateNum);
            await UniTask.Delay(System.TimeSpan.FromSeconds(3f), cancellationToken: token);

            GenerateEnemy(2, generateNum);

        }
        public async UniTaskVoid TankSpeedPattern(int generatePos, CancellationToken token)
        {

            GenerateEnemy(2, generatePos);

            for (int i = 0; i < 2; i++)
            {
                await UniTask.Delay(System.TimeSpan.FromSeconds(3f), cancellationToken: token);
                GenerateEnemy(1, generatePos);
            }

            GenerateEnemy(2, generatePos);

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
            else if (difficulty <= 0.5f) generateSpan += (0.5f - difficulty) * 2;

            patternSpan -=  (5.0f * difficulty);
        }
        public void SetReleaseEnemy(int releaseNum)
        {
            enemyList[releaseNum] = 1;
   
        }
        public void SetReleaseGeneratePos(int releaseNum)
        {
            generatePosList[releaseNum] = 1;
        }
        public void SetEgenerateProbability() 
        {
            var difficulty = difficultyManager.difficulty;
            if (difficulty > 0.6)
            {
                generatePosList[0] *= 1.2f;
                generatePosList[2] /= 1.5f;
                
            }
            else if (difficulty < 0.3f) 
            {
                generatePosList[0] /= 1.5f;
                generatePosList[2] *= 1.2f;
            }
        }
        public void SetPatternProbability() 
        {
            var amountPer = styleCheck.amountStylePer;
            var strongPer = styleCheck.strongStylePer;
            var savePer = styleCheck.saveStylePer;

            var difficulty = difficultyManager.difficulty;

            for (int i = 0; i < patternList.Length ; i++)
            {
                patternList[i] = 1;
            }
            //すべての割合が近かった場合とスタイルの値がどれも低かった場合 確率は同じで解放していく
            if ((amountPer - strongPer == Mathf.Abs(0.1f) && strongPer - savePer == Mathf.Abs(0.1f) && amountPer - savePer == Mathf.Abs(0.1f)) || styleCheck.IsStyle == false)
            {
                if (difficulty < 0.3f)
                {
                    for (int i = 3; i < patternList.Length - 1; i++)
                    {
                        patternList[i] = 0;
                    }
                }
                else if (difficulty < 0.6f)
                {
                    patternList[patternList.Length - 1] = 0;
                }
            }
            //量スタイルが多かった時
            else if (styleCheck.amountStyle == true)
            {
                //難易度高い時は7割弱でスタイルをつくパターンと難しいのが出る予定
                if (difficulty >= 0.6)
                {
                    patternList[1] /= 2;
                    patternList[2] *= 2;
                    patternList[3] *= 2;
                    patternList[4] /= 2;
                    patternList[5] *= 1.5f;
                }
                else if (difficulty <= 0.4)
                {
                    patternList[1] *= 2;
                    patternList[2] /= 2;
                    patternList[3] /= 2;
                    patternList[4] *= 2;
                    patternList[5] /= 1.5f;
                }
                else
                {
                    patternList[2] *= 2;
                    patternList[3] *= 2;
                }
            }
            else if (styleCheck.strongStyle == true)
            {
                if (difficulty >= 0.6)
                {
                    patternList[0] *= 2;
                    patternList[1] /= 3;
                    patternList[2] *= 2;
                    patternList[5] *= 1.5f;
                }
                else if (difficulty <= 0.4)
                {
                    patternList[0] /= 2;
                    patternList[1] *= 3;
                    patternList[2] /= 2;
                    patternList[5] /= 1.5f;
                }
                else
                {
                    patternList[0] *= 2;
                    patternList[2] *= 2;
                }
            }
            else if (styleCheck.saveStyle == true)
            {
                if (difficulty >= 0.6)
                {
                    patternList[0] /= 2;
                    patternList[1] /= 2;
                    patternList[3] *= 2;
                    patternList[4] *= 2;
                    patternList[5] *= 1.5f;
                }
                else if (difficulty <= 0.4)
                {
                    patternList[0] *= 2;
                    patternList[1] *= 2;
                    patternList[3] /= 2;
                    patternList[4] /= 2;
                    patternList[5] /= 1.5f;
                }
                else
                {
                    patternList[3] *= 2;
                    patternList[4] *= 2;
                }
            }
            else Debug.Log("セットできてない");
        }
        public void ResetData()
        {
            generateCount = MaxWave2GenerateNum;
            enemyDeathCount = MaxWave2GenerateNum;
        }

    }
}
