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
        private EnemyGenerateManager enemyGenerateManager;

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
            CancellationToken token = this.GetCancellationTokenOnDestroy();
            StartWave(token);
        }

        private async UniTaskVoid StartWave(CancellationToken token) 
        {

            await UniTask.Delay(System.TimeSpan.FromSeconds(5));
            Debug.Log("start");
            enemyGenerateManager.IsRun = true;
            await UniTask.WaitUntil(() => enemyGenerateManager.enemyDeathCount == 0);
            waveNum++;
            //“ïˆÕ“x’²®”½‰f
            Debug.Log("wave2");
            await UniTask.Delay(System.TimeSpan.FromSeconds(5));
            enemyGenerateManager.ResetData();

            await UniTask.WaitUntil(() => enemyGenerateManager.enemyDeathCount == 0);
            await UniTask.Delay(System.TimeSpan.FromSeconds(2));

            SceneManager.LoadScene("EndScene");
        }  
    }
}