using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;

namespace Manager
{
    public class GameManager : MonoBehaviour
    {
        public int waveNum { get; private set; } = 1;
        private const int MaxWaveNum = 2;
        private EnemyGenerateManager enemyGenerateManager;
        private DifficultyManager difficultyManager;
        private StyleCheck styleCheck;

        public static GameManager Instance = null;
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
            styleCheck = GetComponent<StyleCheck>();

            CancellationToken token = this.GetCancellationTokenOnDestroy();
            StartWave(token);
        }

        private async UniTaskVoid StartWave(CancellationToken token) 
        {

            await UniTask.Delay(System.TimeSpan.FromSeconds(5));
            Debug.Log("start");
            enemyGenerateManager.IsRun = true;

            await UniTask.WaitUntil(() => enemyGenerateManager.generateCount ==15, cancellationToken: token);
            enemyGenerateManager.releasePos++;
            enemyGenerateManager.releaseEnemy++;
            await UniTask.WaitUntil(() => enemyGenerateManager.generateCount == 8, cancellationToken: token);
            enemyGenerateManager.releasePos++;
            enemyGenerateManager.releaseEnemy++;

            await UniTask.WaitUntil(() => enemyGenerateManager.enemyDeathCount == 0,cancellationToken:token);
            waveNum++;
            //“ïˆÕ“x’²®”½‰f
            difficultyManager.reflectDifficulty();
            styleCheck.CheckStyle();

            Debug.Log("wave2");
            await UniTask.Delay(System.TimeSpan.FromSeconds(5));
            enemyGenerateManager.ResetData();
 

            await UniTask.WaitUntil(() => enemyGenerateManager.enemyDeathCount == 0, cancellationToken: token);
            await UniTask.Delay(System.TimeSpan.FromSeconds(2));

            SceneManager.LoadScene("EndScene");
        }  
    }
}