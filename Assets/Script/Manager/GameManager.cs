using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using Data;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public int waveNum { get; private set; } = 1;
        private const int MaxWaveNum = 2;
        private EnemyGenerateManager enemyGenerateManager;
        private DifficultyManager difficultyManager;
        private StyleCheck styleCheck;
        private UserData userData;

        public static GameManager Instance = null;

        //‰½”Ô–Ú‚ÌƒvƒŒƒC‚©
        public int playerNum { private set; get; } = 1;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);


            DontDestroyOnLoad(gameObject);
        }
        private void Start()
        {
            enemyGenerateManager = FindObjectOfType<EnemyGenerateManager>();
            difficultyManager = FindObjectOfType<DifficultyManager>();
            userData = FindObjectOfType<UserData>();

            styleCheck = GetComponent<StyleCheck>();

            var playerDataWrapper = userData.load();
            if (playerDataWrapper.DataList.Count != 0)
            {
                playerNum = playerDataWrapper.DataList[playerDataWrapper.DataList.Count - 1].playerNum + 1;
            }

            CancellationToken token = this.GetCancellationTokenOnDestroy();
            StartWave(token);
        }

        private async UniTaskVoid StartWave(CancellationToken token) 
        {

            await UniTask.Delay(System.TimeSpan.FromSeconds(5));
            Debug.Log("start");
            enemyGenerateManager.IsRun = true;

            await UniTask.WaitUntil(() => enemyGenerateManager.generateCount <=15, cancellationToken: token);
            enemyGenerateManager.SetReleaseGeneratePos(0);
            Debug.Log("‰ð•ú");
            enemyGenerateManager.SetReleaseEnemy(1);
            await UniTask.WaitUntil(() => enemyGenerateManager.generateCount <= 8, cancellationToken: token);
            styleCheck.checkCoinPer();
            enemyGenerateManager.SetReleaseGeneratePos(2);
            enemyGenerateManager.SetReleaseEnemy(2);

            await UniTask.WaitUntil(() => enemyGenerateManager.enemyDeathCount <= 0,cancellationToken:token);
            //Wave2‚É“ü‚é

            waveNum++;
            enemyGenerateManager.SetEnemyObj();
            //“ïˆÕ“x’²®”½‰f
            difficultyManager.reflectDifficulty();
            styleCheck.CheckStyle();
            userData.Save(styleCheck.amountStylePer,
                          styleCheck.strongStylePer,
                          styleCheck.saveStylePer,
                          styleCheck.GetStyle(),
                          styleCheck.IsStyle);
            enemyGenerateManager.SetPatternProbability();

            await UniTask.Delay(System.TimeSpan.FromSeconds(5));
            enemyGenerateManager.ResetData();
 

            await UniTask.WaitUntil(() => enemyGenerateManager.enemyDeathCount <= 0, cancellationToken: token);
            await UniTask.Delay(System.TimeSpan.FromSeconds(2));

            SceneManager.LoadScene("EndScene");
        }  
    }
}